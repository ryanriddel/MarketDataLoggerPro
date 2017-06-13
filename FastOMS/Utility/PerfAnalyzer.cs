using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace FastOMS
{
    public static class PerfAnalyzer
    {
        static ConcurrentDictionary<string, ActionAnalyzer> ActionAnalyzerDictionary = new ConcurrentDictionary<string, ActionAnalyzer>();
        static List<ActionAnalyzer> AnalyzerList = new List<ActionAnalyzer>();

        static public bool DEBUG_MODE_ENABLED = false;

        public static void InitializeActionAnalyzer(string actionName)
        {
            ActionAnalyzerDictionary[actionName] = new ActionAnalyzer(actionName);
            lock(AnalyzerList)
                AnalyzerList.Add(ActionAnalyzerDictionary[actionName]);
        }

        public static string GetPerformanceReport()
        {
            string str = "";
            foreach(ActionAnalyzer act in AnalyzerList)
            {
                str += act.ToString() + Environment.NewLine;
            }
            return str;
        }
        
        //I don't want to check for existence in the dictionary in these two functions because 
        //im afraid of the overhead
        public static void startTime(string actionName)
        {
            if(DEBUG_MODE_ENABLED)
                ActionAnalyzerDictionary[actionName].startTime();
        }

        public static void endTime(string actionName)
        {
            if (DEBUG_MODE_ENABLED)
                ActionAnalyzerDictionary[actionName].endTime();
        }

        public static void addTime(long timeInTicks, string actionName)
        {
            if (DEBUG_MODE_ENABLED)
                ActionAnalyzerDictionary[actionName].addTime(timeInTicks);
        }

        public static ActionAnalyzer getActionAnalyzer(string actionName)
        {
            if (ActionAnalyzerDictionary.ContainsKey(actionName))
                return ActionAnalyzerDictionary[actionName];
            else
                return new ActionAnalyzer(actionName);
        
        }
    }

    public class ActionAnalyzer
    {
        public static List<ActionAnalyzer> ActionAnalyzers = new List<ActionAnalyzer>(5);
        public static long nanosecondsPerTick = 0;
        public static double millisecondsPerTick = 0;
        bool allowNewAdditions = true;

        static ActionAnalyzer()
        {
            nanosecondsPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            millisecondsPerTick = (1000L) / Stopwatch.Frequency;
        }

        string _actionName;
        List<long> _times;
        bool TimeIsInNanoseconds = false;
        Stopwatch watch = new Stopwatch();
        
        public ActionAnalyzer(string actionName)
        {
            _actionName = actionName;
            _times = new List<long>(1000);
            ActionAnalyzers.Add(this);

            if (Stopwatch.IsHighResolution == false)
                Console.WriteLine("WARNING: Stopwatch is not high resolution.");
        }

        public void startTime()
        {
            
            if(!watch.IsRunning)
                watch.Start();
        }

        public void endTime()
        {
            if(watch.ElapsedTicks != 0)
                if (allowNewAdditions)
                    _times.Add(watch.ElapsedTicks);
            watch.Reset();
        }
        public void addTime(long time)
        {
            if (allowNewAdditions)
                _times.Add(time);
        }

        public void convertAllTimesToNanoSeconds()
        {
            if (allowNewAdditions)
                if (!TimeIsInNanoseconds)
                {
                    for (int i = 0; i < _times.Count; i++)
                    {
                        _times[i] = _times[i] * nanosecondsPerTick;
                    }
                    TimeIsInNanoseconds = true;
                }
        }

        public double AverageTime
        {
            
            get { return _times.Average(); }
        }

        public double StandardDeviation
        {
            get {
                allowNewAdditions = false;
                double std= Utilities.StandardDeviation(_times);
                allowNewAdditions = true;
                return std;
                }
        }

        public long MaximumTime
        {
            get { return _times.Max(); }
        }

        public long MinimumTime
        {
            get { return _times.Min(); }
        }

        public override string ToString()
        {
            string str;
            if(!TimeIsInNanoseconds)
               str = _actionName + "(mS): Avg=" + Math.Round(AverageTime* millisecondsPerTick, 2) + ",  StdDev=" + Math.Round(StandardDeviation * millisecondsPerTick,2) + ",  Max=" + Math.Round(MaximumTime * millisecondsPerTick,2) + ",  Min=" + Math.Round(MinimumTime * millisecondsPerTick,2);
            else
               str= _actionName + "(mS): Avg=" + AverageTime  + ",  StdDev=" + StandardDeviation  + ",  Max=" + MaximumTime  + ",  Min=" + MinimumTime ;
            return str;
        }


        
    }
    
}
