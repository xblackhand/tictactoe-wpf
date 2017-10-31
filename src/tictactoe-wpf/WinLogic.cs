namespace Group
{
    /// <summary>
    /// Class to hold logic to check for winner of game after each turn.
    /// </summary>
    class WinLogic
    {
        /// <summary>
        /// Enum class to give direction an integer value for use in switch statements.
        /// </summary>
        public enum Dir { none, right, left, bottom, top, bottomLeft, bottomRight, topRight, topLeft };
        /// <summary>
        /// Function used to find winner of game. Only called on edge marks because an 
        /// edge mark is needed to win in this version of tic-tac-toe.</para>
        /// Uses the edge and walks in each direction where it will recursively call
        /// the function and keep count of call depth.  If depth hits 4, returns false;
        /// otherwise, returns false.
        /// </summary>
        /// <param name="marks"></param>
        /// <returns></returns>
        public static int[] AnalyzeBoard(bool[,] marks, bool[,] opponentMarks)
        {
            int[] winnerFound = new int[] { 1, -1, -1 };
            //iterate throuKDgh top and bottom row
            for (int i = 0; i < 5; i += 4)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (marks[i, j])
                    {
                        //to the right
                        if (j + 1 < 5 && i < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i, j + 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i, j + 1, Dir.right, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (!opponentMarks[i, j + 1])
                            {
                                winnerFound = new int[3] { 1, i, j + 1 };
                            }
                        }
                        //to the left
                        if (j < 5 && i < 5 && j - 1 >= 0 && i >= 0)
                        {
                            if (marks[i, j - 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i, j - 1, Dir.left, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (!opponentMarks[i, j - 1])
                            {
                                winnerFound = new int[3] { 1, i, j - 1 };
                            }
                        }
                        //to the bottom
                        if (j < 5 && i + 1 < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i + 1, j, Dir.bottom, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i + 1, j])
                            {
                                winnerFound = new int[3] { 1, i + 1, j };
                            }
                        }
                        //to the top
                        if (j < 5 && i < 5 && j >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i - 1, j, Dir.top, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if(winnerFound[0] < 1 && !opponentMarks[i - 1, j])
                            {
                                winnerFound = new int[3] { 1, i - 1, j };
                            }
                        }
                        //to the bottom left
                        if (j < 5 && i + 1 < 5 && j - 1 >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j - 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i + 1, j - 1, Dir.bottomLeft, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if(winnerFound[0] < 1 && !opponentMarks[i + 1, j - 1])
                            {
                                winnerFound = new int[3] { 1, i + 1, j - 1 };
                            }
                        }
                        //to the bottom right
                        if (j + 1 < 5 && i + 1 < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j + 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i + 1, j + 1, Dir.bottomRight, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if(winnerFound[0] < 1 && !opponentMarks[i + 1, j + 1])
                            {
                                winnerFound = new int[3] { 1, i + 1, j + 1 };
                            }
                        }
                        //to the top right
                        if (j + 1 < 5 && i < 5 && j >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j + 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i - 1, j + 1, Dir.topRight, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i - 1, j + 1])
                            {
                                winnerFound = new int[3] { 1, i - 1, j + 1 };
                            }
                        }
                        //to the top left
                        if (j < 5 && i < 5 && j - 1 >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j - 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i - 1, j - 1, Dir.topLeft, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i - 1, j - 1])
                            {
                                winnerFound = new int[3] { 1, i - 1, j - 1 };
                            }
                        }
                    }
                }
            }
            //iterate through left and right column
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 5; j+=4)
                {
                    if (marks[i, j])
                    {
                        //to the right
                        if (j + 1 < 5 && i < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i, j + 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i, j + 1, Dir.right, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i, j + 1])
                            {
                                winnerFound = new int[3] { 1, i, j + 1 };
                            }
                        }
                        //to the left
                        if (j < 5 && i < 5 && j - 1 >= 0 && i >= 0)
                        {
                            if (marks[i, j - 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i, j - 1, Dir.left, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i, j - 1])
                            {
                                winnerFound = new int[3] { 1, i, j - 1 };
                            }
                        }
                        //to the bottom
                        if (j < 5 && i + 1 < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i + 1, j, Dir.bottom, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i + 1, j])
                            {
                                winnerFound = new int[3] { 1, i + 1, j };
                            }
                        }
                        //to the top
                        if (j < 5 && i < 5 && j >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i - 1, j, Dir.top, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i - 1, j])
                            {
                                winnerFound = new int[3] { 1, i - 1, j };
                            }
                        }
                        //to the bottom left
                        if (j < 5 && i + 1 < 5 && j - 1 >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j - 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i + 1, j - 1, Dir.bottomLeft, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i + 1, j - 1])
                            {
                                winnerFound = new int[3] { 1, i + 1, j - 1 };
                            }
                        }
                        //to the bottom right
                        if (j + 1 < 5 && i + 1 < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j + 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i + 1, j + 1, Dir.bottomRight, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i + 1, j + 1])
                            {
                                winnerFound = new int[3] { 1, i + 1, j + 1 };
                            }
                        }
                        //to the top right
                        if (j + 1 < 5 && i < 5 && j >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j + 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i - 1, j + 1, Dir.topRight, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i - 1, j + 1])
                            {
                                winnerFound = new int[3] { 1, i - 1, j + 1 };
                            }
                        }
                        //to the top left
                        if (j < 5 && i < 5 && j - 1 >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j - 1])
                            {
                                int[] result = AnalyzeBoardRec(marks, opponentMarks, i - 1, j - 1, Dir.topLeft, 2);
                                if (result[0] > winnerFound[0])
                                {
                                    winnerFound = result;
                                }
                                if (winnerFound[0] == 4)
                                {
                                    return winnerFound;
                                }
                            }
                            else if (winnerFound[0] < 1 && !opponentMarks[i - 1, j - 1])
                            {
                                winnerFound = new int[3] { 1, i - 1, j - 1 };
                            }
                        }
                    }
                }
            }
            return winnerFound;
        }
        /// <summary>
        /// Recursive function call that will keep iterating through series of user
        /// controlled squares in same direction.
        /// </summary>
        /// <param name="marks"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="direction"></param>
        /// <param name="seriesCount"></param>
        /// <returns></returns>
        private static int[] AnalyzeBoardRec(bool[,] marks, bool[,] opponentMarks, int i, int j, Dir direction, int seriesCount)
        {
            int[] winnerFound = new int[] { seriesCount, i, j };
            //seriesCount counts how many times a piece a function is called 
            //on a series of marks in a row; when seriesCount is 4, there is 
            // a winner.
            if (seriesCount == 4)
            {
                return new int[] { 4, 0, 0 };
            }
            //recursive calls
            switch (direction)
            {
                case Dir.right:
                    {
                        //to the right
                        if (j + 1 < 5 && i < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i, j + 1])
                            {
                                winnerFound = AnalyzeBoardRec(marks, opponentMarks, i, j + 1, Dir.right, ++seriesCount);
                            }
                            else if(winnerFound[0] <= seriesCount)
                            {
                                if (!opponentMarks[i, j + 1])
                                {
                                    winnerFound = new int[3] { seriesCount, i, j + 1 };
                                }
                                else
                                {
                                    winnerFound = new int[3] { 1, -1, -1 };
                                }
                            }
                        }
                        else
                        {
                            winnerFound = new int[3] { 1, -1, -1 };
                        }
                        break;
                    }
                case Dir.left:
                    {
                        //to the left
                        if (j < 5 && i < 5 && j - 1 >= 0 && i >= 0)
                        {
                            if (marks[i, j - 1])
                            {
                                winnerFound = AnalyzeBoardRec(marks, opponentMarks, i, j - 1, Dir.left, ++seriesCount);
                            }
                            else if (winnerFound[0] <= seriesCount)
                            {
                                if (!opponentMarks[i, j - 1])
                                {
                                    winnerFound = new int[3] { seriesCount, i, j - 1 };
                                }
                                else
                                {
                                    winnerFound = new int[3] { 1, -1, -1 };
                                }
                            }
                        }
                        else
                        {
                            winnerFound = new int[3] { 1, -1, -1 };
                        }
                        break;
                    }
                case Dir.bottom:
                    {
                        //to the bottom
                        if (j < 5 && i + 1 < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j])
                            {
                                winnerFound = AnalyzeBoardRec(marks, opponentMarks, i + 1, j, Dir.bottom, ++seriesCount);
                            }
                            else if (winnerFound[0] <= seriesCount)
                            {
                                if (!opponentMarks[i + 1, j])
                                {
                                    winnerFound = new int[3] { seriesCount, i + 1, j };
                                }
                                else
                                {
                                    winnerFound = new int[3] { 1, -1, -1 };
                                }
                            }
                        }
                        else
                        {
                            winnerFound = new int[3] { 1, -1, -1 };
                        }
                        break;
                    }
                case Dir.top:
                    {
                        //to the top
                        if (j < 5 && i < 5 && j >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j])
                            {
                                winnerFound = AnalyzeBoardRec(marks, opponentMarks, i - 1, j, Dir.top, ++seriesCount);
                            }
                            else if (winnerFound[0] <= seriesCount)
                            {
                                if (!opponentMarks[i - 1, j])
                                {
                                    winnerFound = new int[3] { seriesCount, i - 1, j };
                                }
                                else
                                {
                                    winnerFound = new int[3] { 1, -1, -1 };
                                }
                            }
                        }
                        else
                        {
                            winnerFound = new int[3] { 1, -1, -1 };
                        }
                        break;
                    }
                case Dir.bottomLeft:
                    {
                        //to the bottom left
                        if (j < 5 && i + 1 < 5 && j - 1 >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j - 1])
                            {
                                winnerFound = AnalyzeBoardRec(marks, opponentMarks, i + 1, j - 1, Dir.bottomLeft, ++seriesCount);
                            }
                            else if (winnerFound[0] <= seriesCount)
                            {
                                if (!opponentMarks[i + 1, j - 1])
                                {
                                    winnerFound = new int[3] { seriesCount, i + 1, j - 1 };
                                }
                                else
                                {
                                    winnerFound = new int[3] { 1, -1, -1 };
                                }
                            }
                        }
                        else
                        {
                            winnerFound = new int[3] { 1, -1, -1 };
                        }
                        break;
                    }
                case Dir.bottomRight:
                    {
                        //to the bottom right
                        if (j + 1 < 5 && i + 1 < 5 && j >= 0 && i >= 0)
                        {
                            if (marks[i + 1, j + 1])
                            {
                                winnerFound = AnalyzeBoardRec(marks, opponentMarks, i + 1, j + 1, Dir.bottomRight, ++seriesCount);
                            }
                            else if (winnerFound[0] <= seriesCount)
                            {
                                if (!opponentMarks[i + 1, j + 1])
                                {
                                    winnerFound = new int[3] { seriesCount, i + 1, j + 1 };
                                }
                                else
                                {
                                    winnerFound = new int[3] { 1, -1, -1 };
                                }
                            }
                        }
                        else
                        {
                            winnerFound = new int[3] { 1, -1, -1 };
                        }
                        break;
                    }
                case Dir.topRight:
                    {
                        //to the top right
                        if (j + 1 < 5 && i < 5 && j >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j + 1])
                            {
                                winnerFound = AnalyzeBoardRec(marks, opponentMarks, i - 1, j + 1, Dir.topRight, ++seriesCount);
                            }
                            else if (winnerFound[0] <= seriesCount)
                            {
                                if (!opponentMarks[i - 1, j + 1])
                                {
                                    winnerFound = new int[3] { seriesCount, i - 1, j + 1 };
                                }
                                else
                                {
                                    winnerFound = new int[3] { 1, -1, -1 };
                                }
                            }
                        }
                        else
                        {
                            winnerFound = new int[3] { 1, -1, -1 };
                        }
                        break;
                    }
                case Dir.topLeft:
                    {
                        //to the top left
                        if (j < 5 && i < 5 && j - 1 >= 0 && i - 1 >= 0)
                        {
                            if (marks[i - 1, j - 1])
                            {
                                winnerFound = AnalyzeBoardRec(marks, opponentMarks, i - 1, j - 1, Dir.topLeft, ++seriesCount);
                            }
                            else if (winnerFound[0] <= seriesCount)
                            {
                                if (!opponentMarks[i - 1, j - 1])
                                {
                                    winnerFound = new int[3] { seriesCount, i - 1, j - 1 };
                                }
                                else
                                {
                                    winnerFound = new int[3] { 1, -1, -1 };
                                }
                            }
                        }
                        else
                        {
                            winnerFound = new int[3] { 1, -1, -1 };
                        }
                        break;
                    }
            }
            return winnerFound;
        }
    }
}