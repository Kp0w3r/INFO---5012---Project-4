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
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IPlayer
    {
        /// <summary>
        /// Gets the player's display name.
        /// </summary>
        string Name { [OperationContract] get; }

        /// <summary>
        /// Gets the Guid identity of the player.
        /// </summary>
        Guid Id { [OperationContract] get; }

        /// <summary>
        /// Gets an <see cref="IList{ICard}"/> of the cards in the player's hand.
        /// </summary>
        IList<ICard> Hand { [OperationContract] get; }

        /// <summary>
        /// Determines whether a player has a specified card.
        /// </summary>
        /// <param name="card">The card to look for.</param>
        /// <returns>True if the card exists and false if it does not.</returns>
        [OperationContract]
        bool HasCard(ICard card);
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

        public bool HasCard(ICard card)
        {
            throw new NotImplementedException();
        }
        public Player(string name, Guid id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}
