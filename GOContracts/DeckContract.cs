using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace GOContracts
{
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IDeck
    {
        [OperationContract]
        ICard Draw();
        [OperationContract]
        IEnumerable<ICard> Draw(int num);
        [OperationContract(IsOneWay = true)]
        void Shuffle();
        int NumCards { [OperationContract] get; }
    }

}
