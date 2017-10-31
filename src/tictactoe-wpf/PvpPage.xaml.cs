using System.Windows;
using System.Windows.Controls;

namespace Group
{
    /// <summary>
    /// Interaction logic for pvpPage.xaml
    /// </summary>
    public partial class PvpPage : Page
    {
        public PvpPage()
        {
            InitializeComponent();
            Loaded += PvpPage_Loaded;
        }

        private void PvpPage_Loaded(object sender, RoutedEventArgs e)
        {
            playerOneCb.ItemsSource = MainWindow.playerNames;
            playerTwoCb.ItemsSource = MainWindow.playerNames;
            pOneFirstBtn.IsChecked = true;
            p1Xpvp.IsChecked = true;
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            bool playerOneFirst = false;
            if ((bool)pOneFirstBtn.IsChecked)
            {
                playerOneFirst = true;
            }
            else
            {
                playerOneFirst = false;
            }
            Application.Current.MainWindow.Content 
                = new GameBoardPage(playerOneFirst, playerOneCb.SelectedItem.ToString(), playerTwoCb.SelectedItem.ToString(), (bool)p1Xpvp.IsChecked, null);
        }
        private void playerOneCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (playerOneCb.SelectedItem != null && playerTwoCb.SelectedItem != null && playerOneCb.SelectedItem != playerTwoCb.SelectedItem)
            {
                startBtn.IsEnabled = true;
            }
            else
            {
                startBtn.IsEnabled = false;
            }
        }
        private void playerTwoCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (playerOneCb.SelectedItem != null && playerTwoCb.SelectedItem != null && playerOneCb.SelectedItem != playerTwoCb.SelectedItem)
            {
                startBtn.IsEnabled = true;
            }
            else
            {
                startBtn.IsEnabled = false;
            }
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = MainWindow.sPage;
        }

        private void p1Xpvp_Checked(object sender, RoutedEventArgs e)
        {
            p2Opvp.IsChecked = true;
        }

        private void p2Xpvp_Checked(object sender, RoutedEventArgs e)
        {
            p1Opvp.IsChecked = true;
        }

        private void p1Opvp_Checked(object sender, RoutedEventArgs e)
        {
            p2Xpvp.IsChecked = true;
        }

        private void p2Opvp_Checked(object sender, RoutedEventArgs e)
        {
            p1Xpvp.IsChecked = true;
        }
    }
}