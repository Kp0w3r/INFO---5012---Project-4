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
        }

        public int NumCards => Cards.Count;

        public IList<ICard> Cards { get; }

        public ICard Draw()
        {
            return Cards.Take(1).SingleOrDefault();
        }

        public IEnumerable<ICard> Draw(int num)
        {
            return Cards.Take(num);
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
