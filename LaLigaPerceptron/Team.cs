using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaPerceptron
{
    class Team
    {
        private int points, eloRating, isHome, numWonHome, numDrawed, numLostHome, numWonAway, numLostAway;
        public string Name { get; set; }
        public int Points { get { return points; } set { if (value > 0) points = value; } }
        public int EloRating { get { return eloRating; } set { if (value > 0) eloRating = value; } }
        public int IsHome { get { return isHome; } set { if (value == 0 || value == 1) isHome = value; } }
        public List<MatchResults> AllMatches { get; set; } = new List<MatchResults>();
        public int NumberOfWinsHome { get => AllMatches.Where(x => x == MatchResults.WinHome).Count(); set { numWonHome = value; } }
        public int NumberOfDraws { get => AllMatches.Where(x => x == MatchResults.Draw).Count(); set { numDrawed = value; } }
        public int NumberOfLossesHome { get => AllMatches.Where(x => x == MatchResults.LossHome).Count(); set { numLostHome = value; } }
        public int NumberOfWinsAway { get => AllMatches.Where(x => x == MatchResults.WinAway).Count(); set { numWonAway = value; } }
        public int NumberOfLossesAway{ get => AllMatches.Where(x => x == MatchResults.LossAway).Count(); set { numLostAway = value; } }
  
    }
}
