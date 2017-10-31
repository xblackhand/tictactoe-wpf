using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Group
{
    /// <summary>
    /// delete this
    /// Inherited class from Player to hold additional functionality for Ai turn function.
    /// </summary>
    class AiPlayer : Player
    {
        /// <summary>
        /// Constructor that calls base class constructor with same arguments.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        public AiPlayer(string name, string symbol, Brush color, int aiDifficulty) : base(name, symbol, color)
        {
            _aiDifficulty = aiDifficulty;
        }
        /// <summary>
        /// Function to call Ai turn functionality.
        /// </summary>
        /// <param name="opponentMarks"></param>
        /// <param name="buttons"></param>
        public void PerformTurn(bool[,] opponentMarks, List<Button> buttons)
        {
            switch (_aiDifficulty)
            {
                case 1:
                    EasyAiTurn(Marks, opponentMarks, buttons);
                    break;
                case 2:
                    MediumAiTurn(Marks, opponentMarks, buttons);
                    break;
                case 3:
                    EasyAiTurn(Marks, opponentMarks, buttons);
                    //HardAiTurn(Marks, opponentMarks, buttons);
                    break;
                default:
                    MessageBox.Show("ERROR: Ai difficulty error.");
                    Application.Current.Shutdown(1);
                    break;
            }
        }
        private static void EasyAiTurn(bool[,] aiMarks, bool[,] opponentMarks, List<Button> buttons)
        {
            Random rand = new Random();
            bool[,] takenSpots = aiMarks;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (opponentMarks[i, j])
                    { takenSpots[i, j] = true; }
                }
            }
            int row = rand.Next(5);
            int col = rand.Next(5);
            while (takenSpots[row, col])
            {
                row = rand.Next(5);
                col = rand.Next(5);
            }
            buttons[row * 5 + col].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
        private static void MediumAiTurn(bool[,] aiMarks, bool[,] opponentMarks, List<Button> buttons)
        {
            int[] turn = WinLogic.AnalyzeBoard(opponentMarks, aiMarks);
            if (turn[1] == -1 || turn[1] == -1)
            {
                EasyAiTurn(aiMarks, opponentMarks, buttons);
            }
            else
            {
                buttons[turn[1] * 5 + turn[2]].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
        private static void HardAiTurn(bool[,] aiMarks, bool[,] opponentMarks, List<Button> buttons)
        {
            
        }
        private int _aiDifficulty;
    }
}