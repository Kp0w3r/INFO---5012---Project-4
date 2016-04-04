using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IGame
    {
        Guid GameId { [OperationContract] get; }
        IList<IPlayer> Players { [OperationContract] get; }
        [OperationContract]
        IPlayer CreatePlayer(string name);
        [OperationContract]
        bool RemovePlayer(IPlayer player);
    }
}
