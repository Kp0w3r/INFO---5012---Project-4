using System;
using System.Collections.Generic;
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
        public IPlayer PlayerData = null;
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
                    _game.RemovePlayer(PlayerData as Player);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public void UpdateGameState(GoCallback callback)
        {
            throw new NotImplementedException();
        }
    }
}
