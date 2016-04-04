using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOContracts;

namespace GOGameLogic
{
    public abstract class CardGame:IGame
    {

        protected CardGame()
        {
            GameId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets a <see cref="Guid"/> uniquely identifying the game instance.
        /// </summary>
        public Guid GameId { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IPlayer}"/> containing player information for all players in the game.
        /// </summary>
        public IList<IPlayer> Players { get; protected set; }

        /// <summary>
        /// Creates a new player and returns its instance.
        /// </summary>
        /// <param name="name">The display name of the player.</param>
        /// <returns>A reference to an <see cref="IPlayer"/> instance of the player created.</returns>
        public abstract IPlayer CreatePlayer(string name);

        /// <summary>
        /// Removes a specified player from the game.
        /// </summary>
        /// <param name="player">The guid identity of the player.</param>
        /// <returns>A bool value indicating whether the player was successfully removed.</returns>
        public abstract bool RemovePlayer(Guid player);

    }
}
