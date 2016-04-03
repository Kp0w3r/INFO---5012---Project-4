using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOGameLogic
{
    public abstract class CardGame
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
        public IEnumerable<IPlayer> Players { get; protected set; }
    }
}
