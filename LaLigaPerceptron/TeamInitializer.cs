using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaPerceptron
{
    class TeamInitializer
    {
        public Dictionary<TeamNames, Team> InitializeTeams()
        {
            return new Dictionary<TeamNames, Team>()
            {
                [TeamNames.Barcelona] = new Team { Name = "Barcelona", EloRating = 2026 },
                [TeamNames.RealMadrid] = new Team { Name = "Real Madrid", EloRating = 2021 },
                [TeamNames.AtleticoMadrid] = new Team { Name = "Atletico Madrid", EloRating = 1930 },
                [TeamNames.Valencia]  = new Team { Name = "Valencia", EloRating = 1797 },
                [TeamNames.Villareal]  = new Team { Name = "Villareal", EloRating = 1759 },

                [TeamNames.Betis]  = new Team { Name = "Betis", EloRating = 1724 },
                [TeamNames.Sevilla]  = new Team { Name = "Sevilla", EloRating = 1763 },
                [TeamNames.Getafe]  = new Team { Name = "Getafe", EloRating = 1720 },
                [TeamNames.Eibar]  = new Team { Name = "Eibar", EloRating = 1724 },
                [TeamNames.Girona]  = new Team { Name = "Girona", EloRating = 1663 },

                [TeamNames.RealSociedad]  = new Team { Name = "Real Sociedad", EloRating = 1730 },
                [TeamNames.Celta]  = new Team { Name = "Celta", EloRating = 1717 },
                [TeamNames.Espanyol]  = new Team { Name = "Espanyol", EloRating = 1728 },
                [TeamNames.Alaves]  = new Team { Name = "Alaves", EloRating = 1705 },
                [TeamNames.Levante]  = new Team { Name = "Levante", EloRating = 1694 },

                [TeamNames.AthleticBilbao]  = new Team { Name = "Athletic Bilbao", EloRating = 1699 },
                [TeamNames.Leganes]  = new Team { Name = "Leganes", EloRating = 1634 },
                [TeamNames.Huesca]  = new Team { Name = "Huesca", EloRating = 1603 },
                [TeamNames.RayoVallecano]  = new Team { Name = "Rayo Vallecano", EloRating = 1608 },
                [TeamNames.Valladolid]  = new Team { Name = "Valladolid", EloRating = 1620 }
            };
        }
    }
}
