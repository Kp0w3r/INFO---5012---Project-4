using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
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
            Players = new List<Player>();
            ClientCallbacks = new Dictionary<Guid, ICallback>();
            Deck = new Deck();
        }

        public override GoCallback CallBack => new GoCallback(this.Deck.NumCards, PlayerStates);

        public override int MinPlayers { get; } = 2;

        public override int MaxPlayers { get; } = 6;

        public void AskPlayer(Guid self, Guid target, Card card)
        {
            var player = Players.Find(c => c.Id.Equals(self));
            var targetPlayer = Players.Find(c => c.Id.Equals(target));
            var cards = targetPlayer.Hand.Where(c => c.Rank.Equals(card.Rank));
            foreach (var playerCard in cards)
            {
                player.Hand.Add(playerCard);
                targetPlayer.Hand.Remove(playerCard);
            }

            if (!player.Hand.Any())
            {
                PerformCall();
            }
        }

        public override PlayerState CreatePlayer(string name)
        {
            var player = new Player(name);
            Players.Add(player);

            ICallback cb = OperationContext.Current.GetCallbackChannel<ICallback>();

            ClientCallbacks.Add(player.Id, cb);


            Console.WriteLine("Player " + player.Name + "(" + player.Id + ") has joined the game.");
            Console.WriteLine("Players Left: " + Players.Count);

            PerformCall();

            return new PlayerState(player.Name, player.Id, player.Hand.Count(), 0);
        }

        public override bool RemovePlayer(PlayerState playerState)
        {
            var player = Players.Find(p => p.Id == playerState.Id);

            var isRemoved = Players.Remove(player);
            if (isRemoved)
            {
                Console.WriteLine("Player " + player.Name + "(" + player.Id + ") has left the game.");
                Console.WriteLine("Players Left: " + Players.Count);
            }

            ClientCallbacks.Remove(player.Id);

            PerformCall();

            return isRemoved;
        }

        protected override void DealCards()
        {
            int cardCount = (Players.Count == 2) ? CardCount2Players : CardCountMorePlayers;

            foreach (var player in Players)
            {
                player.Hand.Clear();

                foreach (var card in Deck.Draw(cardCount))
                {
                    player.Hand.Add(card as Card);
                }
            }
        }

        private void PerformCall()
        {
            foreach (var clientCallback in ClientCallbacks.Values)
            {
                clientCallback.UpdateGameState(CallBack);
            }
        }
    }
}
