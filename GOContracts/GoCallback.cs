using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    [ServiceContract]
    public interface ICallback
    {
        [OperationContract]
        void UpdateGameState(GoCallback callback);
    }
    [DataContract]
    public class GoCallback { 
        [DataMember]
        public List<PlayerState> Players { get; set; }
        [DataMember]
        public int CardsInDeck { get; private set; }

        public GoCallback(int cardsInDeck, List<PlayerState> players)
        {
            this.CardsInDeck = cardsInDeck;
            this.Players = players;
        }
    }
}