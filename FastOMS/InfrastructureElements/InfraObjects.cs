using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastOMS
{
    public class InfraConnector<D> : IInfraConnector<D> where D : IInfraData
    {
        IInfraReceiver<D> infraReceiver;
        IInfraSender<D> infraSender;
        

        public void AddInfraReceiver(IInfraReceiver<D> receiver)
        {
            throw new NotImplementedException();
        }

        public void SendDataToReceiver(D data)
        {
            infraReceiver.InfraDataReceiveHandler(data);
        }

        public void SendDataToReceiver(D data, IInfraReceiver<D> infraReceiver)
        {
            throw new NotImplementedException();
        }

        public void SendDataToReceiver(D data, Guid receiverGuid)
        {
            throw new NotImplementedException();
        }

        void IInfraConnector<D>.InfraConnector(IInfraSender<D> sender, IInfraReceiver<D> receiver)
        {
            
        }

        public InfraConnector(IInfraSender<D> sender, IInfraReceiver<D> receiver)
        {
            infraSender = sender;
            infraReceiver = receiver;
            infraSender.infraConnector = this;
        }
    }


}
