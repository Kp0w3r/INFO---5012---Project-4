using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOContracts;

namespace GOGameLogic
{
    /// <summary>
    /// Service Host Representation of the Player
    /// </summary>
    public class Player : IPlayer
    {

        public Player(string name)
        {
            this.Name = name;
            Id = Guid.NewGuid();

            Hand = new List<Card>();
        }

        public string Name { get; }

        public Guid Id { get; }

        public List<Card> Hand { get; }

        public bool HasCard(ICard card)
        {
            return Hand.Any(c => c.Equals(card));
        }
    }
}
