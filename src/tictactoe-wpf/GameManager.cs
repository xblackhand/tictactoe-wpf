using System;
using System.Windows;
using System.Windows.Controls;

namespace Group
{
    /// <summary>
    /// Class used to control game flow. Contains members of game and functions to determine winners, switch players,
    /// and control AI during its turn.
    /// </summary>
    class GameManager
    {
        //used to store the reference to the page the GameManager object is controlling
        private readonly GameBoardPage _page;
        private bool[,] SpotsTaken = new bool[5, 5];
        private bool aiGame = false;
        //edge row and column numbers
        /// <summary>
        /// Default constructor to set the players, the current player, and the reference to the controlled page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="firstPlayer"></param>
        public GameManager(GameBoardPage page, Player p1, Player p2, bool playerOneFirst)
        {
            _page = page;
            playerOne = p1;
            playerTwo = p2;
            if (playerOneFirst)
            {
                currentPlayer = playerOne;
                waitingPlayer = playerTwo;
            }
            else
            {
                currentPlayer = playerTwo;
                waitingPlayer = playerOne;
            }
            if (playerTwo.GetType() == typeof(AiPlayer))
            {
                aiGame = true;
            }
        }
        //private varibles to store player information
        private Player playerOne;
        private Player playerTwo;
        private Player currentPlayer;
        private Player waitingPlayer;
        /// <summary>
        /// Function to give AI control if first player or give appropriate human player control.
        /// </summary>
        /// <param name="currentPlayerName"></param>
        public void FirstTurnManagement()
        {
            _page.CurrentPlayerText = currentPlayer.Name + " [" + currentPlayer.GetSymbol() + "]";
            if (currentPlayer.GetType() == typeof(AiPlayer))
            {
                ((AiPlayer)currentPlayer).PerformTurn(waitingPlayer.GetMarks(), _page.GetBoardButtons());
            }
            return;
        }
        /// <summary>
        /// Function called when a button is clicked in the linked GameBoardPage. Stores the player move in its
        /// 2d array and then checks to see if the move caused the player to win.
        /// </summary>
        /// <param name="btn"></param>
        public void ProcessTurn(Button btn)
        {
            _page.DisableBoardButtons();
            Player player = currentPlayer;
            if (btn.Content.ToString() == "")
            {
                btn.Background = player.GetColor();
                btn.Content = player.GetSymbol();
                string[] rowcol = btn.Uid.Split(':');
                player.AddMark(int.Parse(rowcol[0]), int.Parse(rowcol[1]));
                SpotsTaken[int.Parse(rowcol[0]), int.Parse(rowcol[1])] = true;
                // if winner found
                if (CheckIfWinner())
                {
                    WinnerFound();
                    // does nothing, just here to remind me that game quits
                    return;
                }
                // if no winner yet
                else
                {
                    // if the game board is full
                    if (IsBoardFull())
                    {
                        DrawGame();
                        // does nothing, just here to remind me that game quits
                        return;
                    }
                    else
                    {
                        NextTurn();
                        _page.CurrentPlayerText = currentPlayer.Name + " [" + currentPlayer.GetSymbol() + "]";
                        // check to see if next player is an AI
                        if (aiGame && currentPlayer.GetType() == typeof(AiPlayer))
                        {
                            ((AiPlayer)currentPlayer).PerformTurn(waitingPlayer.GetMarks(), _page.GetBoardButtons());
                        }
                    }
                }
            }
            _page.EnableBoardButtons();
        }
        /// <summary>
        /// Function to switch the current player and return new current player's name.
        /// </summary>
        /// <returns></returns>
        public void NextTurn()
        {
            if (currentPlayer == playerTwo)
            {
                currentPlayer = playerOne;
                waitingPlayer = playerTwo;
            }
            else
            {
                currentPlayer = playerTwo;
                waitingPlayer = playerOne;
            }
        }
        /// <summary>
        /// Function to return the GameManager's current player
        /// </summary>
        /// <returns></returns>
        public bool CheckIfWinner()
        {
            bool[,] marks = currentPlayer.GetMarks();
            //check top and bottom for marks
            for (int i = 0; i < 5; i += 4)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (marks[i, j])
                    {
                        //if edge mark has been found, check for a winner
                        //and return the result
                        if(WinLogic.AnalyzeBoard(marks, waitingPlayer.GetMarks())[0] == 4)
                        {
                            return true;
                        }
                    }
                }
            }
            //check left and right for marks
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 5; j += 4)
                {
                    //if edge mark has been found, check for a winner
                    //and return the result
                    if (marks[i, j])
                    {
                        if(WinLogic.AnalyzeBoard(marks, waitingPlayer.GetMarks())[0] == 4)
                        {
                            return true;
                        }
                    }
                }
            }
            //if no edge mark was found return false because you cannot win
            //without atleast one edge mark
            return false;
        }
        private void WinnerFound()
        {
            try
            {
                MainWindow.xmlHandler.ReportWinLoss(currentPlayer.Name, waitingPlayer.Name);
                MessageBoxResult closeChoice = MessageBox.Show(currentPlayer.Name + " wins!\n\nDo you want to keep playing?"
                    , "Game Won"
                    , MessageBoxButton.YesNo);
                if (closeChoice == MessageBoxResult.Yes)
                {
                    Application.Current.MainWindow.Content = MainWindow.sPage;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
            catch
            {
                MessageBox.Show("Error 0x0011: Error updating players win/loss stats."
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);
            }
        }
        public bool IsBoardFull()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!SpotsTaken[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void DrawGame()
        {
            try
            {
                MainWindow.xmlHandler.ReportDraw(currentPlayer.Name, waitingPlayer.Name);
                MessageBoxResult closeChoice = MessageBox.Show("Draw game. Noone wins!\n\nDo you want to keep playing?"
                    , "Draw Game"
                    , MessageBoxButton.YesNo);
                if (closeChoice == MessageBoxResult.Yes)
                {
                    Application.Current.MainWindow.Content = MainWindow.sPage;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
            catch
            {
                MessageBox.Show("Error 0x0020: Error updating players draw stats."
                    , "Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);
            }
        }
    }
}