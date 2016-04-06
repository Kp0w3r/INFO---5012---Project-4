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

        /// <summary>
        /// Gets an <see cref="IList{ICard}"/> of the cards in the player's hand.
        /// </summary>
        IList<ICard> Hand { get; }
    }

    [DataContract]
    public class Player : IPlayer
    {

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public IList<ICard> Hand { get; set; }

        public Player(string name, Guid id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}
