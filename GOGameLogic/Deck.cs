using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOContracts;

namespace GOGameLogic
{
    public class Deck
    {
        private Random _rand;
        private IList<ICard> _cards; 

        public Deck()
        {
            _rand = new Random();

            _cards = new List<ICard>();

            foreach (var suitId in Enum.GetValues(typeof(Card.SuitID)))
            {
                foreach (var rankId in Enum.GetValues(typeof(Card.RankID)))
                {
                    _cards.Add(new Card((Card.SuitID)suitId, (Card.RankID)rankId));
                }
            }

            Shuffle();
        }

        /// <summary>
        /// Returns number of cards left in Deck
        /// </summary>
        public int NumCards => _cards.Count;

        /// <summary>
        /// Returns Cards within deck
        /// </summary>
        public IList<ICard> Cards => _cards;

        /// <summary>
        /// Draws One Card and Removes it from the deck
        /// </summary>
        /// <returns></returns>
        public ICard Draw()
        {
            if (_cards.Count > 0)
            {
                var card = _cards.Take(1).SingleOrDefault();
                _cards.Remove(card);
                return card;
            }
            return null;
        }

        /// <summary>
        /// Draws multiple cards at once determined by num param
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IEnumerable<ICard> Draw(int num)
        {
            var cards = _cards.Take(num);

            var cardArray = cards as ICard[] ?? cards.ToArray();
            foreach (var card in cardArray)
            {
                _cards.Remove(card);
            }
            return cardArray;
        }

        /// <summary>
        /// Shuffles cards within deckObject
        /// </summary>
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
