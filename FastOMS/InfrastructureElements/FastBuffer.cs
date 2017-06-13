using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Disruptor;
using Disruptor.Dsl;
using System.Collections.Concurrent;

namespace FastOMS
{
    public delegate void BufferEventHandlerFunction<T>(T data);
    
    public class FastBuffer<T,A> where T : class, IBufferItem<T, A>, new()
    {
        RingBuffer<T> _ringBuffer;
        Disruptor<T> _disruptor;
        List<BufferEventHandler<T>> _eventHandlers = new List<BufferEventHandler<T>>();
        EventPublisher<T> _publisher;

        bool _disruptorHasStarted = false;
        int _numExtraHandlersAdded = 0;
        int _bufferSize;

        ConcurrentQueue<IMarketDataConsumer<T>> stragglerHandlers = new ConcurrentQueue<IMarketDataConsumer<T>>();

        public void stragglerEventHandler(T data)
        {
            foreach(IMarketDataConsumer<T> cons in stragglerHandlers)
            {
                cons.NewDataHandler(data);
            }
        }
        ConcurrentQueue<IMarketDataConsumer<T>> stragglerHandlers2 = new ConcurrentQueue<IMarketDataConsumer<T>>();

        public void stragglerEventHandler2(T data)
        {
            foreach (IMarketDataConsumer<T> cons in stragglerHandlers2)
            {
                cons.NewDataHandler(data);
            }
        }



        public FastBuffer(int bufferSize = 1024)
        {
            _bufferSize = bufferSize;

            _disruptor = new Disruptor<T>(() => { return new T(); }, _bufferSize, TaskScheduler.Default, ProducerType.Single, new BlockingWaitStrategy());


            //Experiment: test to see if spreading the load between two straggler handlers improves performance
            BufferEventHandler<T> stragglerHandler = new BufferEventHandler<T>();
            stragglerHandler.ThrowEvent += stragglerEventHandler;
            _eventHandlers.Add(stragglerHandler);

            BufferEventHandler<T> stragglerHandler2 = new BufferEventHandler<T>();
            stragglerHandler.ThrowEvent += stragglerEventHandler2;
            _eventHandlers.Add(stragglerHandler2);

            Begin();
        }

        public void ConsumerSubscribe(IMarketDataConsumer<T> cons)
        {
            //Experiment: test to see if spreading the load between two straggler handlers improves performance
            /*if (!_disruptorHasStarted)
            {
                BufferEventHandler<T> newHandler = new BufferEventHandler<T>();
                newHandler.ThrowEvent += cons.NewDataHandler;
                _eventHandlers.Add(newHandler);
            }
            else
            {*/

            if (_numExtraHandlersAdded % 2 == 0)
                    stragglerHandlers.Enqueue(cons);
                else
                    stragglerHandlers2.Enqueue(cons);
            _numExtraHandlersAdded++;

            //}
        }

        public void ConsumerUnsubscribe(IMarketDataConsumer<T> cons)
        {
            if(stragglerHandlers.Contains<IMarketDataConsumer<T>>(cons))
            {
                lock(stragglerHandlers)
                {
                    //WILL HAVE TO WRITE THIS EVENTUALLY
                }
            }
        }

        public void Add(ref A data)
        {
            long sequenceNum = -1;
            
            try
            {
                sequenceNum = _disruptor.RingBuffer.Next();
                T temp = _disruptor.RingBuffer[sequenceNum];
                
                temp.Update(ref data);
                _disruptor.RingBuffer.Publish(sequenceNum);
            }
            catch(Exception e)
            {
                Console.WriteLine("Fast buffer error: " + e.Message);
                Console.WriteLine(data.ToString());
            }

        }


        public void Begin()
        {

            _disruptor.HandleEventsWith(_eventHandlers.ToArray<IEventHandler<T>>());
            _ringBuffer = _disruptor.Start();
            _publisher = new EventPublisher<T>(_ringBuffer);
            _disruptorHasStarted = true;

        }

        void publishEvent(T arg, long sequence, T arg2)
        {

        }


        class BufferEventHandler<B> : IEventHandler<B>
        {
            public static int _numConsumers = 0;
            private readonly int _ordinal;


            public event BufferEventHandlerFunction<B> ThrowEvent = delegate { };


            public BufferEventHandler()
            {
                this._ordinal = _numConsumers++;
            }

            public void OnNext(B data, long sequence, bool endOfBatch)
            {
                ThrowEvent(data);
            }

            public void OnEvent(B data, long sequence, bool endOfBatch)
            {
                ThrowEvent(data);
            }
        }

    }

    class EventTranslator<T> : IEventTranslator<T>
    {
        T _data;
        public EventTranslator(T data)
        {
            _data = data;
        }
        public void TranslateTo(T eventData, long sequence)
        {
            eventData = _data;
        }
    }

    //T should be the class, A should be a struct which is a clone of the class
    //Done for speed (fewer allocations)
    public interface IBufferItem<T, A>
    {
        void Update(ref A newItem);
        
    }
}
