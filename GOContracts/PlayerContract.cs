using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    public interface IPlayer
    {
        /// <summary>
        /// Gets the player's display name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Guid identity of the player.
        /// </summary>
        Guid Id { get; }
    }

    [DataContract]
    public class PlayerState : IPlayer
    {

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public int NumHand { get; set; }
        [DataMember]
        public int NumPairs { get; set; }

        public override string ToString()
        {
            return Name + ", Hand: " + NumHand + ", Pairs: " + NumPairs;
        }

        public PlayerState(string name, Guid id, int numHand, int numPairs)
        {
            this.Name = name;
            this.Id = id;
            this.NumHand = numHand;
            this.NumPairs = numPairs;
        }
    }
}
