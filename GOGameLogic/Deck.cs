using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOContracts;

namespace GOGameLogic
{
    public class Deck : IDeck
    {
        private Random _rand;

        public Deck()
        {
            _rand = new Random();

            Cards = new List<ICard>();

            foreach (var suitId in Enum.GetValues(typeof(Card.SuitID)))
            {
                foreach (var rankId in Enum.GetValues(typeof(Card.RankID)))
                {
                    Cards.Add(new Card((Card.SuitID)suitId, (Card.RankID)rankId));
                }
            }

            Shuffle();
        }

        public int NumCards => Cards.Count;

        public IList<ICard> Cards { get; }

        public ICard Draw()
        {
            var card = Cards.Take(1).SingleOrDefault();
            Cards.Remove(card);
            return card;
        }

        public IEnumerable<ICard> Draw(int num)
        {
            var cards = Cards.Take(num);

            var cardArray = cards as ICard[] ?? cards.ToArray();
            foreach (var card in cardArray)
            {
                Cards.Remove(card);
            }
            return cardArray;
        }

        public void Shuffle()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                // Add one to make upper bound inclusive
                int limit = i + 1;
                int swap = _rand.Next(0, limit);

                if (swap != i)
                {
                    ICard card = Cards[swap];
                    Cards[swap] = Cards[i];
                    Cards[i] = card;
                }
            }
        }

    }
}
