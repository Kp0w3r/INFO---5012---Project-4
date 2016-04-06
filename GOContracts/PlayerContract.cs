using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GOContracts
{
    public interface IPlayer
    {
        /// <summary>
        /// Gets the player's display name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Guid identity of the player.
        /// </summary>
        Guid Id { get; }
    }

    [DataContract]
    public class PlayerState : IPlayer, INotifyPropertyChanged
    {
        private int _numHand = 0;
        private int _numPairs = 0;
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public Guid Id { get; private set; }
        [DataMember]
        public int NumHand {
            get { return _numHand; }
            set
            {
                if (_numHand != value)
                {
                    _numHand = value;
                    PlayerPropertyChanged("NumHand");
                }
            }
        }
        [DataMember]
        public int NumPairs
        {
            get { return _numPairs; }
            set
            {
                if (_numPairs != value)
                {
                    _numPairs = value;
                    PlayerPropertyChanged("NumPairs");
                }
            }
        }

        public override string ToString()
        {
            return Name + ", Hand: " + NumHand + ", Pairs: " + NumPairs;
        }

        public PlayerState(string name, Guid id, int numHand, int numPairs)
        {
            this.Name = name;
            this.Id = id;
            this._numHand = numHand;
            this._numPairs = numPairs;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void PlayerPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
