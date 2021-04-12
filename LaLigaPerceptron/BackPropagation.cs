using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaPerceptron
{
    enum MatchResults
    {
        WinHome, Draw, WinAway, LossHome, LossAway
    }

    enum TeamNames
    {
        Barcelona, AtleticoMadrid, RealMadrid, Valencia, Villareal,
        Betis, Sevilla, Getafe, Eibar, Girona,
        RealSociedad, Celta, Espanyol, Alaves, Levante,
        AthleticBilbao, Leganes, Huesca, RayoVallecano, Valladolid
    }

    class Match
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public int[] input = new int[BackPropagation.InputNeurons];
        public double[] output = new double[BackPropagation.OutputNeurons];

        public int this[int index]
        {
            get { return input[index]; }

            set { //if (value > 1000)
            //        input[index] = value / 100;
            //    else if (Math.Abs(value) > 10) 
            //        input[index] = value / 100; 
            //    else
                    input[index] = value; }
        }
        public static void UpdateEloRatings(Team team1, Team team2, MatchResults matchResult)
        {
            int K = 20;

            double R1 = Math.Pow(10, team1.EloRating / 400.0);
            double R2 = Math.Pow(10, team2.EloRating / 400.0);

            double E1 = R1 / (R1 + R2);
            double E2 = R2 / (R1 + R2);

            double S1 = (matchResult == MatchResults.WinHome) ? 1.0 : (matchResult == MatchResults.WinAway) ? 0.0 : 0.5;
            double S2 = (matchResult == MatchResults.WinAway) ? 1.0 : (matchResult == MatchResults.WinHome) ? 0.0 : 0.5;

            team1.EloRating += (int)(K * (S1 - E1));
            team2.EloRating += (int)(K * (S2 - E2));

            team1.Points += (S1 == 1.0) ? 3 : (S1 == 0.5) ? 1 : 0;
            team2.Points += (S2 == 1.0) ? 3 : (S2 == 0.5) ? 1 : 0;
        }
    }
    class BackPropagation
    {
        #region variables
        public const int InputNeurons = 16, HiddenNeurons = 5, OutputNeurons = 3, MaxSamples = 350;
        public const double Rho = 0.2;

        Random rand = new Random();
        SamplesInitializer init = new SamplesInitializer();
        public Match[] Samples { get; set; } 
        public int[] Input { get; set; } = new int[InputNeurons];
        public double[] Hidden { get; set; } = new double[HiddenNeurons];
        public double[] Target { get; set; } = new double[OutputNeurons];
        public double[] Actual { get; set; } = new double[OutputNeurons];
        public double[] ErrorsHidden { get; set; } = new double[HiddenNeurons];
        public double[] ErrorsOutput { get; set; } = new double[OutputNeurons];
        public double[,] WeightsInputHidden { get; set; } = new double[InputNeurons + 1, HiddenNeurons];
        public double[,] WeightsHiddenOutput { get; set; } = new double[HiddenNeurons + 1, OutputNeurons];
        #endregion variables

        public BackPropagation()
        {
            AssignRandomWeights();
            Samples = init.InitializeElements();
        }
        void AssignRandomWeights()
        {
            for (int i = 0; i < InputNeurons + 1; i++)
            {
                for (int j = 0; j < HiddenNeurons; j++)
                {
                    WeightsInputHidden[i, j] = rand.NextDouble();
                }
            }

            for (int i = 0; i < HiddenNeurons + 1; i++)
            {
                for (int j = 0; j < OutputNeurons; j++)
                {
                    WeightsHiddenOutput[i, j] = rand.NextDouble();
                }
            }
        }

        double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));

        double BackSigmoid(double x) => x * (1.0 - x);

        public void FeedForward()
        {
            double sumSignal;

            for (int j = 0; j < HiddenNeurons; j++)
            {
                sumSignal = 0.0;

                for (int i = 0; i < InputNeurons; i++)
                {
                    sumSignal += Input[i] * WeightsInputHidden[i, j];
                }

                sumSignal += WeightsInputHidden[InputNeurons, j];
                Hidden[j] = Sigmoid(sumSignal);
            }

            for (int j = 0; j < OutputNeurons; j++)
            {
                sumSignal = 0.0;

                for (int i = 0; i < HiddenNeurons; i++)
                {
                    sumSignal += Hidden[i] * WeightsHiddenOutput[i, j];
                }

                sumSignal += WeightsHiddenOutput[HiddenNeurons, j];
                Actual[j] = Sigmoid(sumSignal);
            }
        }

        public void BackPropagate()
        {
            for (int i = 0; i < OutputNeurons; i++)
            {
                ErrorsOutput[i] = (Target[i] - Actual[i]) * BackSigmoid(Actual[i]);
            }

            for (int i = 0; i < HiddenNeurons; i++)
            {
                ErrorsHidden[i] = 0.0;

                for (int j = 0; j < OutputNeurons; j++)
                {
                    ErrorsHidden[i] += ErrorsOutput[j] * WeightsHiddenOutput[i, j];
                }

                ErrorsHidden[i] *= BackSigmoid(Hidden[i]);
            }

            for (int j = 0; j < OutputNeurons; j++)
            {
                for (int i = 0; i < HiddenNeurons; i++)
                {
                    WeightsHiddenOutput[i, j] += Rho * ErrorsOutput[j] * Hidden[i];
                }

                WeightsHiddenOutput[HiddenNeurons, j] += Rho * ErrorsOutput[j];
            }

            for (int j = 0; j < HiddenNeurons; j++)
            {
                for (int i = 0; i < InputNeurons; i++)
                {
                    WeightsInputHidden[i, j] += Rho * ErrorsHidden[j] * Input[i];
                }

                WeightsInputHidden[InputNeurons, j] += Rho * ErrorsHidden[j];
            }
        }
     
        public string ChooseWinner(double[] result, Match match)
        {
            int selected = 0;
            double currentMax = result[selected];

            for (int i = 1; i < OutputNeurons; i++)
            {
                if (result[i] > currentMax)
                {
                    currentMax = result[i];
                    selected = i;
                }
            }

            double res = Math.Round(result[0]);

            switch (selected)
            {
                case 0:
                    return $"{match.HomeTeam.Name} WON {match.AwayTeam.Name}";             
                case 1: 
                    return $"{match.HomeTeam.Name} PLAYED IN A DRAW WITH {match.AwayTeam.Name}";
                case 2:
                    return $"{match.HomeTeam.Name} LOST TO {match.AwayTeam.Name}";

                default: return null;
            }

        }

        //public string Test(string testInput)
        //{
        //    for (int i = 0; i < InputNeurons; i++)
        //    {
        //        Input[i] = (int)char.GetNumericValue(testInput[i]);
        //    }

        //    FeedForward();

        //    return testInput + " надо " + $"{ ChooseWinner(Actual) }";
        //}

    }
}
