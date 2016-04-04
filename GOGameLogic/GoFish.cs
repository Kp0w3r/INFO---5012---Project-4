using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOContracts;

namespace GOGameLogic
{
    public sealed class GoFish : CardGame
    {
        public GoFish()
        {
            Players = new List<IPlayer>();
        }

        public override int MinPlayers { get; } = 2;

        public override int MaxPlayers { get; } = 6;

        public override IPlayer CreatePlayer(string name)
        {
            var player = new PlayerState(name);

            Players.Add(player);

            return player;
        }

        public override bool RemovePlayer(Guid player)
        {
            var selectedPlayer = Players.SingleOrDefault(p => p.Id == player);

            return Players.Remove(selectedPlayer);
        }

        protected override void DealCards()
        {
            int cardCount = (Players.Count > 2) ? 7 : 5;

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
