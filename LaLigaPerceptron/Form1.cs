using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaLigaPerceptron
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void MainOperations()
        {
            BackPropagation bp = new BackPropagation();
            int sample = -1;
            double sum = 0.0, lastError = 0.0;
            int step = 0, lastCheck = 20;
            double error;
            rtb1.Text = "La Liga Predictions 2018/2019" + Environment.NewLine;

            do
            {
                sample++;

                if (sample >= BackPropagation.MaxSamples)
                {
                    sample = 0;
                }

                for (int i = 0; i < BackPropagation.InputNeurons; i++)
                {
                    bp.Input[i] = bp.Samples[sample].input[i];
                }

                for (int i = 0; i < BackPropagation.OutputNeurons; i++)
                {
                    bp.Target[i] = bp.Samples[sample].output[i];
                }

                bp.FeedForward();
                error = 0;

                for (int i = 0; i < BackPropagation.OutputNeurons; i++)
                {
                    error += Math.Pow(bp.Samples[sample].output[i] - bp.Actual[i], 2.0);
                }

                error *= 0.5;
                bp.BackPropagate();

                if (step % 50000 == 0)
                    rtb1.Text += $"Шаг {step} ошибка {error:F10}" + Environment.NewLine;
                step++;
                

            } //while (step <= 40000);
            while (error > 0.001 || step < 500000);

            rtb1.Text += $"ошибка {error:F10}" + Environment.NewLine;

            for (int i = 0; i < BackPropagation.MaxSamples; i++)
            {
                for (int j = 0; j < BackPropagation.InputNeurons; j++)
                {
                    bp.Input[j] = bp.Samples[i].input[j];
                }

                for (int j = 0; j < BackPropagation.OutputNeurons; j++)
                {
                    bp.Target[j] = bp.Samples[i].output[j];
                }

                bp.FeedForward();

                if (bp.ChooseWinner(bp.Actual, bp.Samples[i]) != bp.ChooseWinner(bp.Target, bp.Samples[i]))
                {
                    if (i > BackPropagation.MaxSamples - lastCheck)
                    {
                        for (int j = 0; j < BackPropagation.InputNeurons; j++)
                        {
                            rtb1.Text += $"{bp.Input[j]} ";
                        }
                        lastError++;
                        rtb1.Text += $"   выбрано - {bp.ChooseWinner(bp.Actual, bp.Samples[i])}, а надо - {bp.ChooseWinner(bp.Target, bp.Samples[i])}" + Environment.NewLine;
                    }
                    
                }
                else
                    sum++;
            }

            rtb1.Text += Environment.NewLine + $"Корректность сети {sum / BackPropagation.MaxSamples * 100:F2} %";
            rtb1.Text += Environment.NewLine;
            rtb1.Text += Environment.NewLine + $"Корректность сети для последних 20 шагов {(lastCheck - lastError) / lastCheck * 100:F2} %";
            rtb1.Text += Environment.NewLine;

            //rtb1.Text += bp.Test("2111") + Environment.NewLine;
            //rtb1.Text += bp.Test("1112") + Environment.NewLine;
            //rtb1.Text += bp.Test("0000") + Environment.NewLine;
            //rtb1.Text += bp.Test("0111") + Environment.NewLine;
            //rtb1.Text += bp.Test("2013") + Environment.NewLine;
            //rtb1.Text += bp.Test("2103") + Environment.NewLine;
            //rtb1.Text += bp.Test("0103") + Environment.NewLine;

            var teamsOrdered = bp.Samples.Select(x => x.HomeTeam).Distinct().OrderByDescending(x => x.Points);
            int c = 1;

            foreach (var team in teamsOrdered)
            {
                rtb2.Text += $"{c++} {team.Name} \t {team.Points}" + Environment.NewLine;
            }
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            rtb1.Clear();
            rtb2.Clear();
            MainOperations();
        }
    }
}
