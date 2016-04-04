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
    public partial class MainWindow : Window
    {
        private IGame _game = null;
        public IPlayer playerData = null;
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
            this.Closed += OnClosed;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            DuplexChannelFactory<IGame> deckFactory = new DuplexChannelFactory<IGame>(this, "GameService");
            _game = deckFactory.CreateChannel();

            playerData = _game.CreatePlayer("TonyGeorge");
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            if (_game != null && playerData != null)
            {
                try
                {
                    _game.RemovePlayer(playerData.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }



        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
        }
    }
}
