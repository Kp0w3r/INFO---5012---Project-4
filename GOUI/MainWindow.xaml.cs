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
                _game = deckFactory.CreateChannel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void ConntectPlayer(string name)
        {
            if (PlayerData != null)
            {
                MessageBox.Show("Already Conntected!!");
                return;
            }
            PlayerData = _game.CreatePlayer(name);
            var players = _game.PlayerStates;
            UpdatePlayers(players);
            UpdateHand();
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

        private void AskPlayer()
        {
            if (CardList.SelectedItem != null && PlayerList.SelectedItem != null)
            {
                try
                {
                    var hasCard = _game.AskPlayer(PlayerData.Id, ((PlayerState) PlayerList.SelectedItem).Id,
                        (CardList.SelectedItem as Card));
                    if (!hasCard)
                    {
                        MessageBox.Show("FISH !!!");
                    }
                    else
                    {
                        PlayerData.NumPairs++;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Please Select a Player and a Card");
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "AskButton":
                    AskPlayer();
                    break;
                case "ConnectButton":
                    ConntectPlayer(NameBox.Text);
                    break;
            }
        }

        private void UpdateHand()
        {
            PlayerHand = new ObservableCollection<Card>(_game.GetHand(PlayerData.Id));
            PlayerData.NumHand = PlayerHand.Count();
            CardList.DataContext = PlayerHand;

        }

        private void UpdatePlayers(List<PlayerState> players)
        {
            Players = new ObservableCollection<PlayerState>(players.Where(p => !p.Id.Equals(PlayerData.Id)));
            PlayerList.DataContext = Players;
        }

        private delegate void ClientUpdateDelegate(GoCallback callback);
        public void UpdateGameState(GoCallback callback)
        {

            if (System.Threading.Thread.CurrentThread == this.Dispatcher.Thread)
            {
                if (PlayerData != null)
                {
                    var currentPlayer = callback.Players.Find(p => p.Id.Equals(PlayerData.Id));
                    if (currentPlayer.NumHand != PlayerData.NumHand || currentPlayer.NumPairs != PlayerData.NumPairs)
                    {
                        UpdateHand();
                    }
                    this.DataContext = PlayerData;
                    DeckBlock.Text = callback.CardsInDeck.ToString();
                    UpdatePlayers(callback.Players);
                    if (callback.IsGameOver)
                    {
                        var end = "GameOver: ";
                        if (callback.Winner == PlayerData.Id)
                        {
                            end += "You Win!!";

                        }
                        else
                        {
                            end += "You LOOSE";
                        }

                        MessageBox.Show(end);
                    }
                };

            }
            else
            {
                this.Dispatcher.BeginInvoke(new ClientUpdateDelegate(UpdateGameState), callback);
            }
        }
    }
}
