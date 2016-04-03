using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOContracts;

namespace GOGameLogic
{
    public class GoFish : CardGame
    {
        public GoFish()
        {
            Players = new List<IPlayer>();
        }


        public override IPlayer CreatePlayer(string name)
        {
            var player = new PlayerState(name);

            Players.Add(player);

            return player;
        }

        public override bool RemovePlayer(Guid player)
        {
            throw new NotImplementedException();
        }
    }
}
