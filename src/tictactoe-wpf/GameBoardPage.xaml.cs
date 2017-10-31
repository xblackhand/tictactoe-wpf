using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Group
{
    /// <summary>
    /// Page to display the game board and create the manager object to control the game flow and logic.
    /// </summary>
    public partial class GameBoardPage : Page, INotifyPropertyChanged
    {
        //List to hold all the board buttons (tic-tac-toe buttons)
        private List<Button> _boardButtons = new List<Button>();
        //object to control game flow
        private GameManager _manager;
        /// <summary>
        /// Default constructor to set players, current player, and initialize GameManager. Finally it 
        /// transfers control to the GameManager object
        /// </summary>
        public GameBoardPage(bool playerOneFirst, string playerOneName, string playerTwoName, bool p1X, int? aiDifficulty)
        {
            string p1Sym, p2Sym;
            Brush p1Color, p2Color;
            if(p1X)
            {
                p1Sym = "X";
                p1Color = Brushes.Red;
                p2Sym = "O";
                p2Color = Brushes.Blue;
            }
            else
            {
                p2Sym = "X";
                p2Color = Brushes.Red;
                p1Sym = "O";
                p1Color = Brushes.Blue;
            }
            Player playerOne = new Player(playerOneName, p1Sym, p1Color);
            Player playerTwo = (aiDifficulty != null)
                //if Ai game
                ? (new AiPlayer(playerTwoName, p2Sym, p2Color, (int)aiDifficulty))
                //if human vs human
                : (new Player(playerTwoName, p2Sym, p2Color));
            _manager = new GameManager(this, playerOne, playerTwo, playerOneFirst);
            InitializeComponent();
            CreateButtonList();
            SubscribeBtnClicks();
            //give GameManager object control of game 
            _manager.FirstTurnManagement();
        }
        /// <summary>
        /// Function called by any board button click to call GameManager object to use WinLogic.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            //give GameManager object Button to process current player's choice to test for winner
            //or switch current player
            _manager.ProcessTurn(sender as Button);
        }
        /// <summary>
        /// Function to create a list of Buttons for easier iteration through them later.
        /// </summary>
        private void CreateButtonList()
        {
            try
            {
                _boardButtons.Add(btn1);
                _boardButtons.Add(btn2);
                _boardButtons.Add(btn3);
                _boardButtons.Add(btn4);
                _boardButtons.Add(btn5);
                _boardButtons.Add(btn6);
                _boardButtons.Add(btn7);
                _boardButtons.Add(btn8);
                _boardButtons.Add(btn9);
                _boardButtons.Add(btn10);
                _boardButtons.Add(btn11);
                _boardButtons.Add(btn12);
                _boardButtons.Add(btn13);
                _boardButtons.Add(btn14);
                _boardButtons.Add(btn15);
                _boardButtons.Add(btn16);
                _boardButtons.Add(btn17);
                _boardButtons.Add(btn18);
                _boardButtons.Add(btn19);
                _boardButtons.Add(btn20);
                _boardButtons.Add(btn21);
                _boardButtons.Add(btn22);
                _boardButtons.Add(btn23);
                _boardButtons.Add(btn24);
                _boardButtons.Add(btn25);
            }
            catch
            {
                MessageBox.Show("Error 0x0002: Could not create game board."
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);
                Application.Current.MainWindow.Content = MainWindow.sPage;
            }
        }
        /// <summary>
        /// Subscribe all the board buttons to the Btn_Click event.
        /// </summary>
        private void SubscribeBtnClicks()
        {
            if (_boardButtons != null && _boardButtons.Count > 0)
            {
                foreach (Button button in _boardButtons)
                {
                    button.Click += Btn_Click;
                }
            }
        }
        /// <summary>
        /// Disable all board buttons. For use during AI turn.
        /// </summary>
        public void DisableBoardButtons()
        {
            if (_boardButtons != null && _boardButtons.Count > 0)
            {
                foreach (Button button in _boardButtons)
                {
                    button.IsEnabled = false;
                }
            }
        }
        /// <summary>
        /// Reenable all board buttons. For use during AI turn.
        /// </summary>
        public void EnableBoardButtons()
        {
            if (_boardButtons != null && _boardButtons.Count > 0)
            {
                foreach (Button button in _boardButtons)
                {
                    button.IsEnabled = true;
                }
            }
        }
        /// <summary>
        /// Shuts down the game and closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
        private string _currentPlayerText;
        /// <summary>
        /// Property that get's a string and formats for GUI output.
        /// </summary>
        public string CurrentPlayerText
        {
            set
            {
                if (value.EndsWith("s"))
                {
                    _currentPlayerText = value + "' turn.";
                }
                else
                {
                    _currentPlayerText = value + "'s turn.";
                }
                OnPropertyChanged();
            }
            get { return _currentPlayerText; }
        }
        /// <summary>
        /// Function to return deep copy of buttons on board.
        /// </summary>
        /// <returns></returns>
        public List<Button> GetBoardButtons()
        {
            List<Button> output = new List<Button>();
            if (_boardButtons != null && _boardButtons.Count > 0)
            {
                foreach (Button b in _boardButtons)
                {
                    output.Add(b);
                }
            }
            return output;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}