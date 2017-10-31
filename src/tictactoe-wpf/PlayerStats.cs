using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group
{
    public class PlayerStats
    {
        public PlayerStats(List<string> playerInfo)
        {
            _name = playerInfo[0];
            _longestStreak = playerInfo[1];
            _wins = playerInfo[2];
            _losses = playerInfo[3];
            _draws = playerInfo[4];
        }
        public string Name
        {
            get { return _name; }
        }
        public string LongestStreak
        {
            get { return _longestStreak; }
        }
        public string Wins
        {
            get { return _wins; }
        }
        public string Losses
        {
            get { return _losses; }
        }
        public string Draws
        {
            get { return _draws; }
        }

        private string _name;
        private string _longestStreak;
        private string _wins;
        private string _losses;
        private string _draws;
    }
}
