using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GOContracts
{
    [ServiceContract]
    public interface ICallback
    {
        //[OperationContract(IsOneWay = true)]
        //void UpdateGui(GoCallback info);
    }
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IDeck
    {
        [OperationContract]
        Card Draw();
        [OperationContract(IsOneWay = true)]
        void Shuffle();
        int NumCards { [OperationContract] get; }
        [OperationContract]
        Guid RegisterForCallbacks();
        [OperationContract(IsOneWay = true)]
        void UnregisterForCallbacks(Guid callbackId);
    }
}
