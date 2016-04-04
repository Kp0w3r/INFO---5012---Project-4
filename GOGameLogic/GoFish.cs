using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GOContracts;

namespace GOGameLogic
{
    public sealed class GoFish : CardGame
    {
        private const int CardCount2Players = 7, CardCountMorePlayers = 5;

        public GoFish()
        {
            Players = new List<IPlayer>();
        }

        public override int MinPlayers { get; } = 2;

        public override int MaxPlayers { get; } = 6;
        
        public void AskPlayer(IPlayer self, IPlayer target, ICard card)
        {
            var cards = target.Hand.Where(c => c.Rank.Equals(card.Rank));
            foreach (var playerCard in cards)
            {
                self.Hand.Add(playerCard);
            }
        }
        
        public override IPlayer CreatePlayer(string name)
        {
            var player = new PlayerState(name);

            Players.Add(player);

            return player;
        }

        public override bool RemovePlayer(IPlayer player)
        {
            return Players.Remove(player);
        }

        protected override void DealCards()
        {
            int cardCount = (Players.Count == 2) ? CardCount2Players : CardCountMorePlayers;

            var deck = Decks.Single();

            foreach (var player in Players)
            {
                player.Hand.Clear();

                foreach (var card in deck.Draw(cardCount))
                {
                    player.Hand.Add(card);
                }
            }
        }
    }
}
