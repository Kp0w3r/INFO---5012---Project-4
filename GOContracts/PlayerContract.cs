using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    interface IPlayer
    {
        string Name { get; }
        Guid Id { get; }
        bool hasCard { get; }
    }

    public class PlayerState : IPlayer
    {
        private List<ICard> _handCards;
        private string _name;
        private Guid _id;
         
        public string Name { get; }
        public Guid Id { get; }
        public bool hasCard { get; }

        PlayerState(string Name)
        {
            
        }
    }
}
