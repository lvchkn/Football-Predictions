using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaPerceptron
{
    class SamplesInitializer
    {
        Dictionary<TeamNames, Team> teams;
        TeamInitializer init = new TeamInitializer();
        public SamplesInitializer()
        {
            teams = init.InitializeTeams();
        }
        private Match AddMatch(TeamNames team1, TeamNames team2, MatchResults result)
        {
            teams[team1].AllMatches.Add(result == MatchResults.WinHome ? MatchResults.WinHome 
                : result == MatchResults.WinAway ? MatchResults.LossHome : MatchResults.Draw);

            teams[team2].AllMatches.Add(result == MatchResults.WinHome ? MatchResults.LossAway 
                : result == MatchResults.WinAway ? MatchResults.WinAway : MatchResults.Draw);

            int coeff = 1, ratingsDiff = 150, pointsDiff = 10;

            return new Match
            {
                //[0] = (teams[team1].EloRating - teams[team2].EloRating) / 100,
                //[1] = teams[team1].Points / 10,
                [0] = (teams[team1].EloRating - teams[team2].EloRating > ratingsDiff) ? 1 : 0,
                [1] = (teams[team1].Points - teams[team2].Points > pointsDiff) ? 1: 0,
                [2] = teams[team1].IsHome = 1,
                //[3] = (teams[team2].EloRating - teams[team1].EloRating) / 100,
                //[4] = teams[team2].Points / 10,
                [3] = (teams[team1].NumberOfWinsHome - teams[team2].NumberOfWinsHome > coeff) ? 1 : 0,
                [4] = (teams[team1].NumberOfWinsAway - teams[team2].NumberOfWinsAway > coeff) ? 1 : 0,
                [5] = (teams[team1].NumberOfDraws - teams[team2].NumberOfDraws > coeff) ? 1 : 0,
                [6] = (teams[team1].NumberOfLossesHome - teams[team2].NumberOfLossesHome > coeff) ? 1 : 0,
                [7] = (teams[team1].NumberOfLossesAway - teams[team2].NumberOfLossesAway > coeff) ? 1 : 0,

                [8] = (teams[team2].EloRating - teams[team1].EloRating > ratingsDiff) ? 1 : 0,
                [9] = (teams[team2].Points - teams[team1].Points > pointsDiff) ? 1 : 0,
                [10] = teams[team2].IsHome = 0,
                [11] = (teams[team2].NumberOfWinsHome - teams[team1].NumberOfWinsHome > coeff) ? 1 : 0,
                [12] = (teams[team2].NumberOfWinsAway - teams[team1].NumberOfWinsAway > coeff) ? 1 : 0,
                [13] = (teams[team2].NumberOfDraws - teams[team1].NumberOfDraws > coeff) ? 1 : 0,
                [14] = (teams[team2].NumberOfLossesHome - teams[team1].NumberOfLossesHome > coeff) ? 1 : 0,
                [15] = (teams[team2].NumberOfLossesAway - teams[team1].NumberOfLossesAway > coeff) ? 1 : 0,

                output = new double[] { result == MatchResults.WinHome ? 1 : 0, result == MatchResults.Draw ? 1 : 0,
                                result == MatchResults.WinAway ? 1 : 0 },
                HomeTeam = teams[team1],
                AwayTeam = teams[team2]
            };
        }
        public Match[] InitializeElements()
        {
            
            List<Match> matches = new List<Match>();

            #region Round 1
            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Valladolid, MatchResults.Draw));                    
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Valladolid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Levante, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Levante], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Espanyol, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Espanyol], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.RealSociedad, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.RealSociedad], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Huesca, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Huesca], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Sevilla, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Sevilla], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Getafe, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Getafe], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.AtleticoMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.AtleticoMadrid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Leganes], MatchResults.WinHome);
            #endregion Round 1

            #region Round 2
            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Eibar, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Eibar], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.RealSociedad, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.RealSociedad], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Betis, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Betis], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Valencia, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Valencia], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Villareal, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Villareal], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.RealMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.RealMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Celta, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Celta], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Huesca, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Huesca], MatchResults.Draw);
            #endregion Round 2

            #region Round 3
            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Valladolid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Valladolid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.RealSociedad, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.RealSociedad], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Girona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Girona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.AtleticoMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.AtleticoMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Leganes], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Valencia, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Valencia], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Espanyol, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Espanyol], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.AthleticBilbao, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.AthleticBilbao], MatchResults.Draw);
            #endregion Round 3

            #region Round 4
            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.RayoVallecano, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.RayoVallecano], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Eibar, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Eibar], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Betis, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Betis], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.RealMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.RealMadrid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Villareal, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Villareal], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Levante], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Alaves, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Alaves], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Getafe, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Getafe], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Celta], MatchResults.WinHome);
            #endregion Round 4

            #region Round 5
            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.RealSociedad, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.RealSociedad], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Valladolid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Valladolid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Leganes], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Alaves, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Alaves], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.AtleticoMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.AtleticoMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Espanyol, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Espanyol], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Girona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Girona], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Sevilla, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Sevilla], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.AthleticBilbao, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.AthleticBilbao], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Valencia, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Valencia], MatchResults.Draw);
            #endregion Round 5

            #region Round 6
            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Eibar, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Eibar], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.RayoVallecano, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.RayoVallecano], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Villareal, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Villareal], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Barcelona, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Barcelona], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Celta, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Celta], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.RealMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.RealMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Betis, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Betis], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Levante], MatchResults.WinHome);
            #endregion Round 6

            #region Round 7
            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Espanyol, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Espanyol], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Sevilla, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Sevilla], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Valencia, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Valencia], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.AtleticoMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.AtleticoMadrid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.AthleticBilbao, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.AthleticBilbao], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Leganes], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Girona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Girona], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Valladolid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Valladolid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Getafe], MatchResults.Draw);
            #endregion Round 7

            #region Round 8
            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.RealSociedad, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.RealSociedad], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.RealMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.RealMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Levante, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Levante], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Eibar, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Eibar], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Betis], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Villareal, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Villareal], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Barcelona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Barcelona], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Celta], MatchResults.WinHome);
            #endregion Round 8

            #region Round 9
            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Alaves, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Alaves], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.AtleticoMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.AtleticoMadrid], MatchResults.Draw);
            
            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Leganes, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Leganes], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Levante, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Levante], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Valladolid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Valladolid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.AthleticBilbao, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.AthleticBilbao], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Espanyol, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Espanyol], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Getafe, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Getafe], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Girona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Girona], MatchResults.Draw);
            #endregion Round 9

            #region Round 10
            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Espanyol, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Espanyol], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Valencia, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Valencia], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.RealSociedad, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.RealSociedad], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Eibar, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Eibar], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Leganes], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.RealMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.RealMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Villareal, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Villareal], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Betis], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Huesca], MatchResults.WinHome);
            #endregion Round 10

            #region Round 11
            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Valladolid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Valladolid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.AtleticoMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.AtleticoMadrid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Girona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Girona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Celta, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Celta], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Sevilla, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Sevilla], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Levante, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Levante], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.AthleticBilbao, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.AthleticBilbao], MatchResults.WinHome);
            #endregion Round 11

            #region Round 12
            matches.Add(AddMatch(TeamNames.Levante, TeamNames.RealSociedad, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.RealSociedad], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Valencia, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Valencia], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Leganes, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Leganes], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.AthleticBilbao, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.AthleticBilbao], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Eibar, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Eibar], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Betis, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Betis], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.RealMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.RealMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Villareal, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Villareal], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Espanyol, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Espanyol], MatchResults.WinHome);
            #endregion Round 12

            #region Round 13
            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Barcelona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Barcelona], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.RealMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.RealMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Levante, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Levante], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Girona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Girona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Valladolid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Valladolid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Betis], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Celta], MatchResults.WinHome);
            #endregion Round 13

            #region Round 14
            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Eibar, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Eibar], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Valencia, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Valencia], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Espanyol, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Espanyol], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Leganes, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Leganes], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Sevilla, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Sevilla], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Villareal, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Villareal], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.RealSociedad, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.RealSociedad], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.AtleticoMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.AtleticoMadrid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.AthleticBilbao, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.AthleticBilbao], MatchResults.WinHome);
            #endregion Round 14

            #region Round 15
            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Celta, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Celta], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Sevilla, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Sevilla], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Levante, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Levante], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.RealMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.RealMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Valladolid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Valladolid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Girona, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Girona], MatchResults.WinHome);
            #endregion Round 15

            #region Round 16
            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Leganes, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Leganes], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Valencia, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Valencia], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.RealSociedad, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.RealSociedad], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.AtleticoMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.AtleticoMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Villareal, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Villareal], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Betis, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Betis], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Girona, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Girona], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.AthleticBilbao, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.AthleticBilbao], MatchResults.Draw);
            #endregion Round 16

            #region Round 17
            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Alaves, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Alaves], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Valladolid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Valladolid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Espanyol, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Espanyol], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Celta], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Eibar, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Eibar], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Sevilla, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Sevilla], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Levante], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.RealMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.RealMadrid], MatchResults.Draw);
            #endregion Round 17

            #region Round 18
            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Leganes], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Girona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Girona], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Valencia, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Valencia], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Betis], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.RayoVallecano, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.RayoVallecano], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Villareal, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Villareal], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.RealSociedad, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.RealSociedad], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.AtleticoMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.AtleticoMadrid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.AthleticBilbao, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.AthleticBilbao], MatchResults.WinAway);
            #endregion Round 18

            #region Round 19
            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Celta], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Valladolid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Valladolid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Alaves, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Alaves], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Getafe, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Getafe], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Levante], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Eibar, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Eibar], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.RealMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.RealMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Espanyol, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Espanyol], MatchResults.WinHome);
            #endregion Round 19

            #region Round 20
            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Valencia, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Valencia], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.AtleticoMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.AtleticoMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Leganes], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Girona, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Girona], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Valladolid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Valladolid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.RealSociedad, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.RealSociedad], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.AthleticBilbao, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.AthleticBilbao], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Espanyol, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Espanyol], MatchResults.WinHome);
            #endregion Round 20

            #region Round 21
            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Getafe, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Getafe], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Villareal, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Villareal], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Eibar, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Eibar], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Levante], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Betis], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.RealMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.RealMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Celta], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Huesca, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Huesca], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.RayoVallecano, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.RayoVallecano], MatchResults.WinAway);
            #endregion Round 21

            #region Round 22
            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Valladolid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Valladolid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Valencia, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Valencia], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.AthleticBilbao, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.AthleticBilbao], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Girona, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Girona], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.AtleticoMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.AtleticoMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Espanyol, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Espanyol], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Leganes, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Leganes], MatchResults.WinAway);
            #endregion Round 22

            #region Round 23
            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Villareal, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Villareal], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.RealMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.RealMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Huesca, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Huesca], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Celta], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Barcelona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Barcelona], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Betis], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Eibar, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Eibar], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.RealSociedad, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.RealSociedad], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Levante], MatchResults.WinHome);
            #endregion Round 23

            #region Round 24
            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Valladolid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Valladolid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Levante, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Levante], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.AtleticoMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.AtleticoMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Leganes], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Alaves, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Alaves], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Espanyol, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Espanyol], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Girona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Girona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.AthleticBilbao, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.AthleticBilbao], MatchResults.WinAway);
            #endregion Round 24

            #region Round 25
            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Huesca, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Huesca], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Celta, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Celta], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Eibar, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Eibar], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Villareal, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Villareal], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Valencia, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Valencia], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.RealMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.RealMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Betis, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Betis], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.RealSociedad, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.RealSociedad], MatchResults.Draw);
            #endregion Round 25

            #region Round 26
            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Girona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Girona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Valladolid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Valladolid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Alaves, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Alaves], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Getafe, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Getafe], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.AthleticBilbao, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.AthleticBilbao], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.AtleticoMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.AtleticoMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Celta], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Levante], MatchResults.WinHome);
            #endregion Round 26

            #region Round 27
            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Espanyol, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Espanyol], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Eibar, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Eibar], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Leganes], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Valencia, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Valencia], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Villareal, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Villareal], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Betis, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Betis], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.RealMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.RealMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.RealSociedad, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.RealSociedad], MatchResults.WinHome);
            #endregion Round 27

            #region Round 28
            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Levante, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Levante], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.AtleticoMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.AtleticoMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Alaves, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Alaves], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Celta], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Girona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Girona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.Valladolid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.Valladolid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Sevilla, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Sevilla], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.RayoVallecano], MatchResults.WinHome);
            #endregion Round 28

            #region Round 29
            matches.Add(AddMatch(TeamNames.Girona, TeamNames.AthleticBilbao, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.AthleticBilbao], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.AtleticoMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.AtleticoMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Espanyol, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Espanyol], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Villareal, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Villareal], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Leganes, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Leganes], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Valencia, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Valencia], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Betis, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Betis], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Eibar, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Eibar], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Huesca, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Huesca], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.RealSociedad, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.RealSociedad], MatchResults.Draw);
            #endregion Round 29

            #region Round 30
            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Girona, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Girona], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Barcelona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Barcelona], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Levante], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Celta, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Celta], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.RealMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.RealMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Valladolid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Valladolid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Betis], MatchResults.WinHome);
            #endregion Round 30

            #region Round 31
            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Valencia, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Valencia], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.AtleticoMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.AtleticoMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.Eibar, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.Eibar], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Espanyol, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Espanyol], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Leganes, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Leganes], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Villareal, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Villareal], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.RealSociedad, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.RealSociedad], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.AthleticBilbao, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.AthleticBilbao], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Huesca, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Huesca], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Valladolid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Sevilla], MatchResults.WinAway);
            #endregion Round 31

            #region Round 32
            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Celta, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Celta], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Alaves, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Alaves], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Barcelona, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Barcelona], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.Betis], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.RayoVallecano], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Villareal, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Villareal], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Eibar, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Eibar], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Getafe, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Getafe], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Levante], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.RealMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.RealMadrid], MatchResults.Draw);
            #endregion Round 32

            #region Round 33
            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Valladolid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Valladolid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.RealSociedad, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.RealSociedad], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Celta, TeamNames.Girona, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Celta], teams[TeamNames.Girona], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Eibar, TeamNames.AtleticoMadrid, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Eibar], teams[TeamNames.AtleticoMadrid], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.Huesca, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.Huesca], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Valencia, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Valencia], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RealMadrid, TeamNames.AthleticBilbao, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealMadrid], teams[TeamNames.AthleticBilbao], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Espanyol, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Espanyol], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Leganes, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Leganes], MatchResults.WinHome);
            #endregion Round 33

            #region Round 34
            matches.Add(AddMatch(TeamNames.Alaves, TeamNames.Barcelona, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Alaves], teams[TeamNames.Barcelona], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Huesca, TeamNames.Eibar, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Huesca], teams[TeamNames.Eibar], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valladolid, TeamNames.Girona, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Valladolid], teams[TeamNames.Girona], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Valencia, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Valencia], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.AthleticBilbao, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.AthleticBilbao], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Levante, TeamNames.Betis, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Levante], teams[TeamNames.Betis], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Espanyol, TeamNames.Celta, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Espanyol], teams[TeamNames.Celta], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Getafe, TeamNames.RealMadrid, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Getafe], teams[TeamNames.RealMadrid], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Villareal, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Villareal], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.Sevilla, TeamNames.RayoVallecano, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Sevilla], teams[TeamNames.RayoVallecano], MatchResults.WinHome);
            #endregion Round 34

            #region Round 35
            matches.Add(AddMatch(TeamNames.AtleticoMadrid, TeamNames.Valladolid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.AtleticoMadrid], teams[TeamNames.Valladolid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Barcelona, TeamNames.Levante, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Barcelona], teams[TeamNames.Levante], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.AthleticBilbao, TeamNames.Alaves, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.AthleticBilbao], teams[TeamNames.Alaves], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Leganes, TeamNames.Celta, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Leganes], teams[TeamNames.Celta], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Girona, TeamNames.Sevilla, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.Girona], teams[TeamNames.Sevilla], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.RayoVallecano, TeamNames.RealMadrid, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RayoVallecano], teams[TeamNames.RealMadrid], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Valencia, TeamNames.Eibar, MatchResults.WinAway));
            Match.UpdateEloRatings(teams[TeamNames.Valencia], teams[TeamNames.Eibar], MatchResults.WinAway);

            matches.Add(AddMatch(TeamNames.RealSociedad, TeamNames.Getafe, MatchResults.WinHome));
            Match.UpdateEloRatings(teams[TeamNames.RealSociedad], teams[TeamNames.Getafe], MatchResults.WinHome);

            matches.Add(AddMatch(TeamNames.Villareal, TeamNames.Huesca, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Villareal], teams[TeamNames.Huesca], MatchResults.Draw);

            matches.Add(AddMatch(TeamNames.Betis, TeamNames.Espanyol, MatchResults.Draw));
            Match.UpdateEloRatings(teams[TeamNames.Betis], teams[TeamNames.Espanyol], MatchResults.Draw);
            #endregion Round 35

            return matches.ToArray();
        }
    }
}
