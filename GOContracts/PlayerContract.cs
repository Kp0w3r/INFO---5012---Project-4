using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    public interface IPlayer
    {
        string Name { get; }
        Guid Id { get; }
        bool HasCard(ICard card);
    }
    [DataContract]
    public class PlayerState : IPlayer
    {
        [DataMember]
        private List<ICard> _handCards;
        [DataMember]
        private string _name;
        [DataMember]
        private Guid _id;
         
        [DataMember]
        public string Name { get { return _name; } }
        [DataMember]
        public Guid Id { get { return _id; } }
        public bool HasCard(ICard card)
        {
            return _handCards.Any(c => c.Rank == card.Rank);
        }


        PlayerState(string Name, List<ICard> hand)
        {
            _name = Name;
            _id = Guid.NewGuid();
            _handCards = hand;
        }
    }
}
