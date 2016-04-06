using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    /// <summary>
    /// Service Contract for game Instance so clients can access the Servicehosts game interface
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IGame
    {
        Guid GameId { [OperationContract] get; }
        List<PlayerState> PlayerStates { [OperationContract] get; }
        [OperationContract]
        PlayerState CreatePlayer(string name);
        [OperationContract]
        bool RemovePlayer(PlayerState player);
        [OperationContract]
        List<Card> GetHand(Guid playerId);
        [OperationContract]
        bool AskPlayer(Guid self, Guid target, Card card);
    }
}
