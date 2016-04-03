using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace GOContracts
{
    [ServiceContract]
    public interface ICallback
    {
    }

    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IDeck
    {
        [OperationContract]
        ICard Draw();
        [OperationContract(IsOneWay = true)]
        void Shuffle();
        int NumCards { [OperationContract] get; }
    }

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
            var lastCard = Cards.Last();

            Cards.Remove(lastCard);

            return lastCard;
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
