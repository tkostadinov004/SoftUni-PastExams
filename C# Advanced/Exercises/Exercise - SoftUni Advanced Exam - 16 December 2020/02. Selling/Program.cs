using System;
using System.Collections.Generic;

namespace _02._Selling
{
    class Program
    {
        static Coords FindCoords(char character, char[,] bakery)
        {
            Coords coords = new Coords();
            for (int i = 0; i < bakery.GetLength(0); i++)
            {
                for (int j = 0; j < bakery.GetLength(0); j++)
                {
                    if (bakery[i, j] == character)
                    {
                        coords = new Coords(i, j);
                    }
                }
            }
            return coords;
        }
        static Coords RemovePillars(Coords firstPillar, char[,] bakery)
        {
            List<Coords> pillars = new List<Coords>();
            for (int i = 0; i < bakery.GetLength(0); i++)
            {
                for (int j = 0; j < bakery.GetLength(1); j++)
                {
                    if (bakery[i, j] == 'O')
                    {
                        pillars.Add(new Coords(i, j));
                    }
                }
            }
            for (int i = 0; i < pillars.Count; i++)
            {
                bakery[pillars[i].Row, pillars[i].Col] = '-';
            }
            return pillars.Find(i => i.Row != firstPillar.Row && i.Col != firstPillar.Col);
        }

        static void Main(string[] args)
        {
            List<Coords> coordsOfPillars = new List<Coords>();

            int n = int.Parse(Console.ReadLine());
            char[,] bakery = new char[n, n];
            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                for (int j = 0; j < n; j++)
                {
                    bakery[i, j] = line[j];
                    if (bakery[i, j] == 'O')
                    {
                        coordsOfPillars.Add(new Coords(i, j));
                    }
                }
            }

            bool isOutOfBakery = false;

            int money = 0;
            while (isOutOfBakery == false && money < 50)
            {
                Coords heroCoords = FindCoords('S', bakery);
                string command = Console.ReadLine();
                if (command == "left")
                {
                    if (heroCoords.Col - 1 == -1)
                    {
                        isOutOfBakery = true;
                    }
                    else
                    {
                        if (bakery[heroCoords.Row, heroCoords.Col - 1] == 'O' && coordsOfPillars.Count == 2)
                        {
                            Coords secondPillar = RemovePillars(new Coords(heroCoords.Row, heroCoords.Col - 1), bakery);
                            bakery[secondPillar.Row, secondPillar.Col] = 'S';
                        }
                        else
                        {
                            if (bakery[heroCoords.Row, heroCoords.Col - 1] != '-')
                            {
                                money += int.Parse(bakery[heroCoords.Row, heroCoords.Col - 1].ToString());
                            }
                            bakery[heroCoords.Row, heroCoords.Col - 1] = 'S';
                        }
                    }
                    bakery[heroCoords.Row, heroCoords.Col] = '-';
                }
                else if (command == "right")
                {
                    if (heroCoords.Col + 1 >= bakery.GetLength(0))
                    {
                        isOutOfBakery = true;
                    }
                    else
                    {
                        if (bakery[heroCoords.Row, heroCoords.Col + 1] == 'O' && coordsOfPillars.Count == 2)
                        {
                            Coords secondPillar = RemovePillars(new Coords(heroCoords.Row, heroCoords.Col + 1), bakery);
                            bakery[secondPillar.Row, secondPillar.Col] = 'S';
                        }
                        else
                        {
                            if (bakery[heroCoords.Row, heroCoords.Col + 1] != '-')
                            {
                                money += int.Parse(bakery[heroCoords.Row, heroCoords.Col + 1].ToString());
                            }
                            bakery[heroCoords.Row, heroCoords.Col + 1] = 'S';
                        }
                    }
                    bakery[heroCoords.Row, heroCoords.Col] = '-';
                }
                else if (command == "up")
                {
                    if (heroCoords.Row - 1 == -1)
                    {
                        isOutOfBakery = true;
                    }
                    else
                    {
                        if (bakery[heroCoords.Row - 1, heroCoords.Col] == 'O' && coordsOfPillars.Count == 2)
                        {
                            Coords secondPillar = RemovePillars(new Coords(heroCoords.Row - 1, heroCoords.Col), bakery);
                            bakery[secondPillar.Row, secondPillar.Col] = 'S';
                        }
                        else
                        {
                            if (bakery[heroCoords.Row - 1, heroCoords.Col] != '-')
                            {
                                money += int.Parse(bakery[heroCoords.Row - 1, heroCoords.Col].ToString());
                            }
                            bakery[heroCoords.Row - 1, heroCoords.Col] = 'S';
                        }
                    }
                    bakery[heroCoords.Row, heroCoords.Col] = '-';
                }
                else if (command == "down")
                {
                    if (heroCoords.Row + 1 >= bakery.GetLength(1))
                    {
                        isOutOfBakery = true;
                    }
                    else
                    {
                        if (bakery[heroCoords.Row + 1, heroCoords.Col] == 'O' && coordsOfPillars.Count == 2)
                        {
                            Coords secondPillar = RemovePillars(new Coords(heroCoords.Row + 1, heroCoords.Col), bakery);
                            bakery[secondPillar.Row, secondPillar.Col] = 'S';
                        }
                        else
                        {
                            if (bakery[heroCoords.Row + 1, heroCoords.Col] != '-')
                            {
                                money += int.Parse(bakery[heroCoords.Row + 1, heroCoords.Col].ToString());
                            }
                            bakery[heroCoords.Row + 1, heroCoords.Col] = 'S';
                        }
                    }
                    bakery[heroCoords.Row, heroCoords.Col] = '-';
                }
            }

            Console.WriteLine(isOutOfBakery ? "Bad news, you are out of the bakery." : "Good news! You succeeded in collecting enough money!");
            Console.WriteLine($"Money: {money}");
            for (int i = 0; i < bakery.GetLength(0); i++)
            {
                for (int j = 0; j < bakery.GetLength(0); j++)
                {
                    Console.Write(bakery[i, j]);
                }
                Console.WriteLine();
            }
        }
        class Coords
        {
            public Coords() : this(-1, -1) { }
            public Coords(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public int Row { get; set; }
            public int Col { get; set; }
        }
    }
}