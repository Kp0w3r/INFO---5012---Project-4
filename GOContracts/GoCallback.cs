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
        public List<Player> Players { get; set; }
        [DataMember]
        public int NumCards { get; private set; }
        [DataMember]
        public bool EmptyHand { get; private set; }

        public GoCallback(int c, bool e, List<Player> p )
        {
            NumCards = c;
            EmptyHand = e;
        }
}

    }