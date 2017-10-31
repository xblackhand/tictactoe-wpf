using System.Windows;
using System.Windows.Controls;

namespace Group
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : Page
    {
        public StartUp()
        {
            InitializeComponent();
        }
        public void pvcBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new PvcPage();
        }
        public void pvpBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new PvpPage();
        }
        private void teamBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = MainWindow.tPage;
        }
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = new RegisterPage();
        }
        private void fameBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.playerHistoryList.Clear();
            try
            {
                foreach (PlayerStats player in MainWindow.xmlHandler.GetPlayerScoreList())
                {
                    MainWindow.playerHistoryList.Add(player);
                }
            }
            catch
            {
                MessageBox.Show("Error 0x0004: Failed loading player history data."
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);
                return;
            }
            Application.Current.MainWindow.Content = new PlayerHistoryPage();
        }
    }
}
