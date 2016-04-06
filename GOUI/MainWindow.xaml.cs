using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GOContracts;

namespace GOUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [CallbackBehavior(ConcurrencyMode=ConcurrencyMode.Reentrant, UseSynchronizationContext=false)]
    public partial class MainWindow : Window, ICallback
    {
        private IGame _game = null;
        public PlayerState PlayerData = null;
        public ObservableCollection<PlayerState> Players = new ObservableCollection<PlayerState>();
        public ObservableCollection<Card> PlayerHand = new ObservableCollection<Card>();

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
            this.Closed += OnClosed;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                DuplexChannelFactory<IGame> deckFactory = new DuplexChannelFactory<IGame>(this, "GameService");
                this._game = deckFactory.CreateChannel();
                var game = this._game;
                if (game != null)
                {
                    PlayerData = _game.CreatePlayer("TonyGeorge");
                    UpdatePlayers(_game.PlayerStates);
                    UpdateHand();
                    this.DataContext = PlayerData;
                }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            if (_game != null && PlayerData != null)
            {
                try
                {
                    _game.RemovePlayer(PlayerData as PlayerState);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void UpdateHand()
        {
            PlayerHand = new ObservableCollection<Card>(_game.GetHand(PlayerData.Id));
            PlayerData.NumHand = PlayerHand.Count();
            if (CardList.DataContext != PlayerHand)
            {
                CardList.DataContext = PlayerHand;
            }
        }

        private void UpdatePlayers(List<PlayerState> players)
        {
            Players = new ObservableCollection<PlayerState>(players.Where(p => !p.Id.Equals(PlayerData.Id)));
            if (PlayerList.DataContext != Players)
            {
                PlayerList.DataContext = Players;
            }
        }

        public void UpdateGameState(GoCallback callback)
        {
            var currentPlayer = callback.Players.Find(p => p.Id.Equals(PlayerData.Id));
            if (currentPlayer.NumHand != PlayerData.NumHand || currentPlayer.NumPairs != PlayerData.NumPairs)
            {
                UpdateHand();
            }
            UpdatePlayers(callback.Players);
        }
    }
}
