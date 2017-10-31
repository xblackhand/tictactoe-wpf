using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Group
{
    public class XmlUpdater
    {
        private string _xmlPath;
        private XmlNode _root;
        public XmlUpdater(string xmlPath)
        {
            try
            {
                _xmlPath = xmlPath;
                _xml = new XmlDocument();
                if (!File.Exists(_xmlPath))
                {
                    File.Create(_xmlPath).Dispose();
                    StreamWriter stream = new StreamWriter(_xmlPath);
                    stream.WriteLine("<Players>");
                    stream.WriteLine("</Players>");
                    stream.Close();
                }
                _xml.Load(_xmlPath);
                _root = _xml.SelectSingleNode(Players);
            }
            catch
            {
                MessageBox.Show("Error 0x0008: Fatal error loading player data xml."
                    , "Fatal Error"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
        public void AddPlayer(string name, string password)
        {
            try
            {
                XmlElement playerNode = _xml.CreateElement(Player);
                XmlAttribute playerName = _xml.CreateAttribute(Name);
                playerName.Value = name;
                XmlAttribute playerPassword = _xml.CreateAttribute(Password);
                playerPassword.Value = password;
                XmlAttribute playerWins = _xml.CreateAttribute(Wins);
                playerWins.Value = String.Format("{0:d}", 0);
                XmlAttribute playerLosses = _xml.CreateAttribute(Losses);
                playerLosses.Value = String.Format("{0:d}", 0);
                XmlAttribute playerDraws = _xml.CreateAttribute(Draws);
                playerDraws.Value = String.Format("{0:d}", 0);
                XmlAttribute playerStreak = _xml.CreateAttribute(Streak);
                playerStreak.Value = String.Format("{0:d}", 0);
                XmlAttribute playerLastWin = _xml.CreateAttribute(LastWin);
                playerLastWin.Value = "false";
                XmlAttribute playerLongestStreak = _xml.CreateAttribute(LongestStreak);
                playerLongestStreak.Value = String.Format("{0:d}", 0);
                playerNode.Attributes.Append(playerName);
                playerNode.Attributes.Append(playerPassword);
                playerNode.Attributes.Append(playerWins);
                playerNode.Attributes.Append(playerLosses);
                playerNode.Attributes.Append(playerDraws);
                playerNode.Attributes.Append(playerStreak);
                playerNode.Attributes.Append(playerLastWin);
                playerNode.Attributes.Append(playerLongestStreak);
                _root.AppendChild(playerNode);
                _xml.Save(_xmlPath);
            }
            catch
            {
                throw;
            }
        }
        public void ReportWinLoss(string winner, string loser)
        {
            try
            {
                XmlNodeList nodes = _root.SelectNodes(Player);
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes[Name].Value == winner)
                    {
                        int score = int.Parse(node.Attributes[Wins].Value);
                        node.Attributes[Wins].Value = String.Format("{0:d}", ++score);
                        if (bool.Parse(node.Attributes[LastWin].Value))
                        {
                            int streak = int.Parse(node.Attributes[Streak].Value);
                            node.Attributes[Streak].Value = String.Format("{0:d}", ++streak);
                            if (streak > int.Parse(node.Attributes[LongestStreak].Value))
                            {
                                node.Attributes[LongestStreak].Value = String.Format("{0:d}", streak);
                            }
                        }
                        else
                        {
                            node.Attributes[LastWin].Value = "true";
                            node.Attributes[Streak].Value = string.Format("{0:d}", 1);
                            if (1 > int.Parse(node.Attributes[LongestStreak].Value))
                            {
                                node.Attributes[LongestStreak].Value = String.Format("{0:d}", 1);
                            }
                        }
                    }
                    else if (node.Attributes[Name].Value == loser)
                    {
                        int score = int.Parse(node.Attributes[Losses].Value);
                        node.Attributes[Losses].Value = String.Format("{0:d}", ++score);
                        if (bool.Parse(node.Attributes[LastWin].Value))
                        {
                            node.Attributes[LastWin].Value = "false";
                            node.Attributes[Streak].Value = string.Format("{0:d}", 0);
                        }
                    }
                }
                _xml.Save(MainWindow.xmlPath);
            }
            catch
            {
                throw;
            }
        }
        public void ReportDraw(string playerOne, string playerTwo)
        {
            try
            {
                XmlNodeList nodes = _root.SelectNodes(Player);
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes[Name].Value == playerOne || node.Attributes[Name].Value == playerTwo)
                    {
                        int score = int.Parse(node.Attributes[Draws].Value);
                        node.Attributes[Draws].Value = String.Format("{0:d}", ++score);
                    }
                }
                _xml.Save(MainWindow.xmlPath);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Function that returns a List of strings that has 4 values.
        /// 1) Name 2)Longest Win Streak Count 3) Wins 4) Losses
        /// </summary>
        /// <returns></returns>
        public List<PlayerStats> GetPlayerScoreList()
        {
            try
            {
                Dictionary<string, List<int>> playerScorePairs = new Dictionary<string, List<int>>();
                List<PlayerStats> output = new List<PlayerStats>();
                XmlNodeList nodes = _root.SelectNodes(Player);
                foreach (XmlNode node in nodes)
                {
                    playerScorePairs.Add(node.Attributes[Name].Value
                        , new List<int>
                                {
                                int.Parse(node.Attributes[LongestStreak].Value)
                                , int.Parse(node.Attributes[Wins].Value)
                                , int.Parse(node.Attributes[Losses].Value)
                                , int.Parse(node.Attributes[Draws].Value)
                                }
                        );
                }
                foreach (var item in playerScorePairs.OrderByDescending(r => r.Value[0]))
                {
                    List<string> playerInfo = new List<string>();
                    playerInfo.Add(item.Key);
                    foreach (var playerValue in item.Value)
                    {
                        playerInfo.Add(string.Format("{0:d}", playerValue));
                    }
                    output.Add(new PlayerStats(playerInfo));
                }
                return output;
            }
            catch
            {
                throw;
            }
        }

        private XmlDocument _xml;
        private const string LastWin = "LastWin";
        private const string Losses = "Losses";
        private const string LongestStreak = "LongestStreak";
        private const string Name = "Name";
        private const string Password = "Password";
        private const string Player = "Player";
        private const string Players = "Players";
        private const string Streak = "Streak";
        private const string Wins = "Wins";
        private const string Draws = "Draws";
    }
}