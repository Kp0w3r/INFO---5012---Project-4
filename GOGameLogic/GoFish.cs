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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public sealed class GoFish : CardGame
    {
        /// <summary>
        /// Amount of cards each player is dealed
        /// </summary>
        private const int CardCount2Players = 7, CardCountMorePlayers = 5;

        public GoFish()
        {
            Players = new List<Player>();
            ClientCallbacks = new Dictionary<Guid, ICallback>();
            Deck = new Deck();
        }

        /// <summary>
        /// Gets GoCallback DataContracts of current state of the game
        /// </summary>
        public override GoCallback CallBack => new GoCallback(this.Deck.NumCards, PlayerStates);

        /// <summary>
        /// Represents Minimum amount of players in game (Currently Not Implemented
        /// </summary>
        public override int MinPlayers { get; } = 2;

        /// <summary>
        /// Represents Max amount of players in game (Currently Not Implemented
        /// </summary>
        public override int MaxPlayers { get; } = 6;
        
        /// <summary>
        /// See CardGame
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool AskPlayer(Guid self, Guid target, Card card)
        {
            var player = Players.Find(c => c.Id.Equals(self));
            var targetPlayer = Players.Find(c => c.Id.Equals(target));
            var cards = targetPlayer.Hand.Where(c => c.Rank.Equals(card.Rank)).ToList();
            var hasCard = (cards.Any());
            if (hasCard == false)
            {
                player.Hand.Add(Deck.Draw() as Card);
            }
            foreach (var playerCard in cards)
            {
                player.Hand.Add(playerCard);
                targetPlayer.Hand.Remove(playerCard);
            }
            PerformCall();

            if (Deck.NumCards == 0)
            {
                IsGameOver = true;
                return false;
            }

            return hasCard;
        }

        /// <summary>
        /// Registers Player to game and callbacks
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override PlayerState CreatePlayer(string name)
        {
            var player = new Player(name);
            Players.Add(player);

            ICallback cb = OperationContext.Current.GetCallbackChannel<ICallback>();

            ClientCallbacks.Add(player.Id, cb);


            Console.WriteLine("Player " + player.Name + "(" + player.Id + ") has joined the game.");
            Console.WriteLine("Players Left: " + Players.Count);


            DealCards();
            PerformCall();

            return new PlayerState(player.Name, player.Id, player.Hand.Count(), 0);
        }

        /// <summary>
        /// Deregisters player from game and callbacks
        /// </summary>
        /// <param name="playerState"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deals Cards to all Players
        /// </summary>
        protected override void DealCards()
        {
            int cardCount = (Players.Count <= 2) ? CardCount2Players : CardCountMorePlayers;

            foreach (var player in Players)
            {
                int draw = cardCount - player.Hand.Count;
                foreach (var card in Deck.Draw(draw))
                {
                    player.Hand.Add(card as Card);
                }
            }
        }

        /// <summary>
        /// Calls All Callbacks registered to object
        /// </summary>
        private void PerformCall()
        {
            GoCallback cb;
            var finalStates = PlayerStates;
            finalStates.Sort((p1, p2) => p1.NumHand.CompareTo(p2.NumHand));
            cb = IsGameOver ? new GoCallback(Deck.NumCards, finalStates) { IsGameOver = true, Winner = finalStates.First().Id} : CallBack;
            if (ClientCallbacks.Count > 0)
            {
                foreach (ICallback clientCallback in ClientCallbacks.Values)
                {
                    clientCallback.UpdateGameState(cb);
                }
            }

        }
    }
}
