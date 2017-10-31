using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Group
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page, INotifyPropertyChanged
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.xmlHandler.AddPlayer(nameTB.Text.Trim(), passwordTB.Password.Trim());
                MainWindow.ConstructPlayerList();
            }
            catch
            {
                MessageBox.Show("Error 0x0010: Failed to add new player."
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);
            }
            Application.Current.MainWindow.Content = MainWindow.sPage;
        }
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Content = MainWindow.sPage;
        }
        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(nameTB.Text) && !string.IsNullOrEmpty(passwordTB.Password))
            {
                saveBtn_Click(sender, e);
            }
        }
        private void nameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MainWindow.playerNames.Contains(nameTB.Text.Trim()))
            {
                invalidNameLbl.Content = "Username already exists!";
                invalidNameLbl.Visibility = Visibility.Visible;
                SaveBtnEnabled = false;
            }
            else if(nameTB.Text.Trim().Length > 26)
            {
                invalidNameLbl.Content = "Username exceeds max length!";
                invalidNameLbl.Visibility = Visibility.Visible;             
            }
            else
            {
                invalidNameLbl.Visibility = Visibility.Hidden;
                if (!string.IsNullOrEmpty(passwordTB.Password.Trim()))
                {
                    SaveBtnEnabled = true;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        private void passwordTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordTB.Password.Count() < 4 || passwordTB.Password.Count() > 26)
            {
                invalidPasswordLbl.Visibility = Visibility.Visible;
            }
            else
            {
                invalidPasswordLbl.Visibility = Visibility.Hidden;
            }
            if (invalidNameLbl.Visibility != Visibility.Visible && invalidPasswordLbl.Visibility != Visibility.Visible 
                && nameTB.Text.Length > 0 && passwordTB.Password.Length > 0)
            {
                SaveBtnEnabled = true;
            }
            else
            {
                SaveBtnEnabled = false;
            }
        }
        private bool _saveBtnEnabled = false;
        public bool SaveBtnEnabled
        {
            get { return _saveBtnEnabled; }
            set
            {
                _saveBtnEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}