using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Group
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Global container/objects that are needed to be shared across all pages
        public static List<string> playerNames = new List<string>();
        public static List<PlayerStats> playerHistoryList = new List<PlayerStats>();
        public static XmlUpdater xmlHandler;
        //Global readonly/const variables that only need to be set at creation
        public const string aiName = "Computer";
        public static readonly string xmlPath = Path.Combine(Environment.CurrentDirectory, "PlayerStorage.xml");
        //create instance of each page that may have to be loaded multiple times
        public static TeamPage tPage;
        public static StartUp sPage;
         public MainWindow()
        {
            InitializeComponent();
            tPage = new TeamPage();
            sPage = new StartUp();
            xmlHandler = new XmlUpdater(xmlPath);
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ConstructPlayerList();
            this.Content = sPage;
        }
        public static void ConstructPlayerList()
        {
            playerNames.Clear();
            try
            {
                foreach (PlayerStats player in xmlHandler.GetPlayerScoreList())
                {
                    playerNames.Add(player.Name);
                }
            }
            catch
            {
                MessageBox.Show("Error 0x0001: Failed to construct player list.\n"
                    , "Fatal Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);
                Application.Current.Shutdown(-1);
            }
        }
    }
}