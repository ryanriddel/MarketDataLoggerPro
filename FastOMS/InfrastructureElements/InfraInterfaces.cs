using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace FastOMS
{
    public interface IInfraData
    {
        
    }

    public interface IInfraParticipant
    {
        
    }

    public interface IInfraConnector<D> where D : IInfraData
    {
        

        void InfraConnector(IInfraSender<D> sender, IInfraReceiver<D> receiver);
        void AddInfraReceiver(IInfraReceiver<D> receiver);
        
        void SendDataToReceiver(D data);
        void SendDataToReceiver(D data, Guid receiverGuid);
        void SendDataToReceiver(D data, IInfraReceiver<D> infraReceiver);
    }

    public interface IInfraSender<D> where D : IInfraData
    {
        IInfraConnector<D> infraConnector
        {
            get;
            set;
        }
        void SendInfraData(D infraData);
    }

    public interface IInfraReceiver<D> where D : IInfraData
    {
        void InfraDataReceiveHandler(D receivedData);
    }

    public interface IUIForm
    {
        Guid getGuid();
    }

    

    
    
}
