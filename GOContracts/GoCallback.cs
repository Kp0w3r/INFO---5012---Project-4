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
    public class GoCallback
    {
        [DataMember]
        public List<PlayerState> Players { get; set; }
        [DataMember]
        public DeckState DeckState { get; private set; }

        public GoCallback(DeckState d, List<PlayerState> p )
        {
            this.DeckState = d;
            this.Players = p;
        }
    }

    [DataContract]
    public class DeckState
    {
        [DataMember]
        public int NumCards { get; private set; }
        [DataMember]
        public bool IsDeckEmpty { get; private set; }

        public DeckState(int c, bool e)
        {
            NumCards = c;
            IsDeckEmpty = e;
        }
    }
}