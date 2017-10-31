using System.Windows;
using System.Windows.Controls;

namespace Group
{
    /// <summary>
    /// Interaction logic for PvcPage.xaml
    /// </summary>
    public partial class PvcPage : Page
    {
        public PvcPage()
        {
            InitializeComponent();
            Loaded += PvcPage_Loaded;
        }
        private void PvcPage_Loaded(object sender, RoutedEventArgs e)
        {
            playerOneCb.ItemsSource = MainWindow.playerNames;
            pOneFirstBtn.IsChecked = true;
            p1Xpvc.IsChecked = true;
        }
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            int? aiDifficulty = null;
            bool playerOneFirst = false;
            if ((bool)pOneFirstBtn.IsChecked)
            {
                playerOneFirst = true;
            }
            else
            {
                playerOneFirst = false;
            }

            if ((bool)easyBtn.IsChecked)
            {
                aiDifficulty = 1;
            }
            else if ((bool)mediumBtn.IsChecked)
            {
                aiDifficulty = 2;
            }
           // else if ((bool)hardBtn.IsChecked)
            {
               // aiDifficulty = 3;
            }
            Application.Current.MainWindow.Content
                = new GameBoardPage(playerOneFirst, playerOneCb.SelectedItem.ToString(), ((ComboBoxItem)aiCb.SelectedItem).Content.ToString(), (bool)p1Xpvc.IsChecked, aiDifficulty);
        }
        private void playerOneCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (playerOneCb.SelectedItem != null)
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

        private void p1Xpvc_Checked(object sender, RoutedEventArgs e)
        {

            p2Opvc.IsChecked = true;
        }

        private void p1Opvc_Checked(object sender, RoutedEventArgs e)
        {
            p2Xpvc.IsChecked = true;
        }

        private void p2Xpvc_Checked(object sender, RoutedEventArgs e)
        {
            p1Opvc.IsChecked = true;
        }

        private void p2Opvc_Checked(object sender, RoutedEventArgs e)
        {
            p1Xpvc.IsChecked = true;
        }
    }
}
