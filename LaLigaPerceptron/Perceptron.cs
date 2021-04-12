using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaPerceptron
{ 
    class Perceptron
    {
        const int N = 2;      // размерность клеток
        const int Teta = 2;       // порог
        const double DeltaW = 0.03;    // вес обучения

        class LC
        {                    // координаты
            public int L;                    // строка
            public int C;                    // столбец
        }

        Random rand = new Random();

        
        int[,] R = new int[N, N];                // слой рецепторов
        int[,] A = new int[N, N];                // ассоциативный слой
        LC[,,] S = new LC[N, N, N];             // связи
        double[,] W = new double[N, N];                // веса связей

        bool fTest = false;            // флаг проверки работоспособности
        void InitLayers()  // инициализация слоёв
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int c = 0; c < N; c++)
                    {
                        S[i, j, c] = new LC();
                    }
                }
            }

            for (int L = 0; L < N; L++)
            {
                for (int C = 0; C < N; C++)
                {
                    W[L, C] = 0;                    // очистка весов
                    for (int K = 0; K < N; K++)
                    {   // генерация связей
                        S[L, C, K].L = rand.Next(N);
                        S[L, C, K].C = rand.Next(N);
                    }
                }
            }
        }
        int Obraz(int H)  // создание образа по шагу H
        {
            // чистка рецепторов
            for (int L = 0; L < N; L++)
            {
                for (int C = 0; C < N; C++)
                {
                    R[L, C] = 0;
                }
            }

            int sd = H % 2;            // выбор образа: 0-нолик,1-крестик
            int rr = rand.Next(N / 2); // выбор радиуса образа
            if (rr < 5)
            {
                rr = 5;
            }

            // выбор места расположения образа
            int Lc = rr + rand.Next(N - 2 * rr);     // строка центра
            int Cc = rr + rand.Next(N - 2 * rr);     // столбец центра

            // рисование выбранного образа
            switch (sd)
            {
                case 0:  // рисование нолика
                    for (int i = 1; i < 150; i++)
                    {
                        int L = Lc + (int)(rr * Math.Cos(i));
                        int C = Cc + (int)(rr * Math.Sin(i));
                        R[L, C] = 1;
                    }
                    break;

                case 1:  // рисование крестика
                    for (int i = -rr; i <= rr; i++)
                    {
                        int L = Lc + i;
                        int C = Cc + i;
                        R[L, C] = 1;
                        C = Cc - i;
                        R[L, C] = 1;
                    }
                    break;
            }

            // вывод образа на экран во время проверки работоспособности
            if (fTest)
            {
                if (sd == 0)
                {
                    Console.WriteLine("Рисую нолик");
                }

                else
                {
                    Console.WriteLine("Рисую крестик");
                }

                for (int L = 0; L < N; L++)
                {
                    for (int C = 0; C < N; C++)
                    {
                        if (R[L, C] == 0)
                        {
                            Console.Write("_");
                        }
                        else
                        {
                            Console.Write("*");
                        }
                    }
                    Console.WriteLine();
                }
            }

            return sd;
        }

        void Otobr()  // отображение образа
        {
            for (int L = 0; L < N; L++)  // очистка ассоциативного слоя A
            {
                for (int C = 0; C < N; C++)
                {
                    A[L, C] = 0;
                }
            }

            for (int L = 0; L < N; L++)  // отображение в слое A (входы)
            {
                for (int C = 0; C < N; C++)
                {
                    if (R[L, C] == 1)
                    {
                        for (int K = 0; K < N; K++)
                        {
                            int Lv = S[L, C, K].L;
                            int Cv = S[L, C, K].C;
                            A[Lv, Cv]++;
                        }
                    }
                }
            }

            for (int L = 0; L < N; L++)  // отображение в слое A (выходы)
            {
                for (int C = 0; C < N; C++)
                {
                    if (A[L, C] > Teta)
                    {
                        A[L, C] = 1;
                    }
                    else
                    {
                        A[L, C] = 0;
                    }
                }
            }
        }
        int Reak()  // распознавание образа
        {
            double E = 0;      // эффекторный слой    

            for (int L = 0; L < N; L++)
            {
                for (int C = 0; C < N; C++)
                {
                    E += A[L, C] * W[L, C];
                }
            }

            // вывод результата при проверке работоспособности
            if (fTest)
            {
                Console.WriteLine();

                if (E > 0)
                {
                    Console.WriteLine("Я думаю, что это крестик");
                }
                else
                {
                    Console.WriteLine("Я думаю, что это нолик");
                }
            }

            return (E > 0) ? 1 : 0;
        }
        void Teach(int sd)  // обучение нейросети не угадавшую образ sd
        {
            for (int L = 0; L < N; L++)
            {
                for (int C = 0; C < N; C++)
                {
                    if (A[L, C] == 1)
                    {     // обучение виноватых
                        if (sd == 0)
                        {
                            W[L, C] -= DeltaW;
                        }
                        else
                        {
                            W[L, C] += DeltaW;
                        }
                    }
                }
            }
        }

        

        public void PlayMatch(Team team1, Team team2)
        {
            int[] team1Properties = new int[] { team1.EloRating, team1.Points, team1.IsHome };
            int[] team2Properties = new int[] { team2.EloRating, team2.Points, team2.IsHome };
        }
        public void MainFunc()
        {
            Console.WriteLine("Обучение перцептрона распознаванию двух образов: крестика и нолика");

            int lmax = 100000;       // число шагов обучения
            int nOk = 0;           // число удачных ответов

            InitLayers();           // инициализация слоёв

            for (int step = 1; step <= lmax; step++)
            {
                int dic = Obraz(step);  // новый образ: 0-нолик, 1-крестик
                Otobr();                // отображение
                int dic1 = Reak();      // опознавание

                if (dic == dic1)
                {
                    nOk++;
                }
                else
                {
                    Teach(dic);   // обучение
                }
                // вывод текущей информации на экран
                Console.WriteLine($"Шаг { step }: Доля удачных ответов {(double)nOk / (double)step * 100} %");
                // тестирование за 20 шагов до конца обучения
                if (step == (lmax - 20))
                {
                    fTest = true;
                }
            }

            Console.WriteLine("Конец");
            Console.ReadKey();
        }
    }
}
