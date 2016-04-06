using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    public interface ICard
    {
        Card.SuitID Suit { get; }
        Card.RankID Rank { get; }
    }

    /// <summary>
    /// Data Contract for card objects
    /// Implemented from Class Examples
    /// </summary>
    [DataContract]
    public class Card : ICard
    {
        public enum SuitID
        {
            Clubs, Diamonds, Hearts, Spades
        };

        public enum RankID
        {
            Ace, King, Queen, Jack, Ten, Nine, Eight, Seven, Six, Five, Four, Three, Two
        };

        [DataMember]
        public SuitID Suit { get; private set; }

        [DataMember]
        public RankID Rank { get; private set; }

        public Card(SuitID s, RankID r)
        {
            Suit = s;
            Rank = r;
        }

        public override string ToString()
        {
            return Rank.ToString() + " of " + Suit.ToString();

        }
    }
}
