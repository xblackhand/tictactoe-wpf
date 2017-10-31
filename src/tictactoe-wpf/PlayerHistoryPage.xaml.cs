using System.Windows;
using System.Windows.Controls;

namespace Group
{
    /// <summary>
    /// Interaction logic for HallOfFamePage.xaml
    /// </summary>
    public partial class PlayerHistoryPage : Page
    {
        public PlayerHistoryPage()
        {
            InitializeComponent();
            Loaded += HallOfFamePage_Loaded;
        }
        private void HallOfFamePage_Loaded(object sender, RoutedEventArgs e)
        {
            playerList.ItemsSource = MainWindow.playerHistoryList;
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = MainWindow.sPage;
        }
    }
}