using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    interface IPlayer
    {
        string Name { get; }
        Guid Id { get; }
        bool HasCard(ICard card);
    }

    public class PlayerState : IPlayer
    {
        private List<ICard> _handCards;
        private string _name;
        private Guid _id;
         
        public string Name { get { return _name; } }
        public Guid Id { get { return _id; } }
        public bool HasCard(ICard card)
        {
            return _handCards.Any(c => c.Equals(card));
        }


        PlayerState(string Name, List<ICard> hand)
        {
            _name = Name;
            _id = new Guid(Name);
            _handCards = hand;
        }
    }
}
