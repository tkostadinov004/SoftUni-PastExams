using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Bee
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            char[,] territory = new char[n, n];

            int[] beeCoords = new int[2];
            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                for (int j = 0; j < n; j++)
                {
                    territory[i, j] = line[j];

                    if (territory[i, j] == 'B')
                    {
                        beeCoords = new int[2] { i, j };
                    }
                }
            }

            string c;

            int beeRow = beeCoords[0];
            int beeCol = beeCoords[1];

            int pollinatedFlowersCount = 0;

            bool outOfRange = false;
            while (outOfRange == false && (c = Console.ReadLine()) != "End")
            {
                if (c == "up")
                {
                    territory[beeRow, beeCol] = '.';
                    beeRow--;
                    while (beeRow >= 0 && territory[beeRow, beeCol] != '.')
                    {
                        if (territory[beeRow, beeCol] == 'f')
                        {
                            pollinatedFlowersCount++;
                            break;
                        }
                        else if (territory[beeRow, beeCol] == 'O')
                        {
                            territory[beeRow, beeCol] = '.';
                            beeRow--;
                        }
                    }

                    if (beeRow >= 0)
                    {
                        territory[beeRow, beeCol] = 'B';
                    }
                    else
                    {
                        outOfRange = true;
                    }
                }
                else if (c == "down")
                {
                    territory[beeRow, beeCol] = '.';
                    beeRow++;
                    while (beeRow <= territory.GetLength(0) - 1 && territory[beeRow, beeCol] != '.')
                    {
                        if (territory[beeRow, beeCol] == 'f')
                        {
                            pollinatedFlowersCount++;
                            break;
                        }
                        else if (territory[beeRow, beeCol] == 'O')
                        {
                            territory[beeRow, beeCol] = '.';
                            beeRow++;
                        }
                    }

                    if (beeRow <= territory.GetLength(0) - 1)
                    {
                        territory[beeRow, beeCol] = 'B';
                    }
                    else
                    {
                        outOfRange = true;
                    }
                }
                else if (c == "left")
                {
                    territory[beeRow, beeCol] = '.';
                    beeCol--;
                    while (beeCol >= 0 && territory[beeRow, beeCol] != '.')
                    {
                        if (territory[beeRow, beeCol] == 'f')
                        {
                            pollinatedFlowersCount++;
                            break;
                        }
                        else if (territory[beeRow, beeCol] == 'O')
                        {
                            territory[beeRow, beeCol] = '.';
                            beeCol--;
                        }
                    }

                    if (beeCol >= 0)
                    {
                        territory[beeRow, beeCol] = 'B';
                    }
                    else
                    {
                        outOfRange = true;
                    }
                }
                else if (c == "right")
                {
                    territory[beeRow, beeCol] = '.';
                    beeCol++;
                    while (beeCol <= territory.GetLength(0) - 1 && territory[beeRow, beeCol] != '.')
                    {
                        if (territory[beeRow, beeCol] == 'f')
                        {
                            pollinatedFlowersCount++;
                            break;
                        }
                        else if (territory[beeRow, beeCol] == 'O')
                        {
                            territory[beeRow, beeCol] = '.';
                            beeCol++;
                        }
                    }

                    if (beeCol <= territory.GetLength(0) - 1)
                    {
                        territory[beeRow, beeCol] = 'B';
                    }
                    else
                    {
                        outOfRange = true;
                    }
                }
            }

            if (outOfRange)
            {
                Console.WriteLine("The bee got lost!");
            }
            if (pollinatedFlowersCount < 5)
            {
                Console.WriteLine($"The bee couldn't pollinate the flowers, she needed {5 - pollinatedFlowersCount} flowers more");
            }
            else
            {
                Console.WriteLine($"Great job, the bee managed to pollinate {pollinatedFlowersCount} flowers!");
            }
            Print(territory);
        }
        static void Print(char[,] territory)
        {
            for (int i = 0; i < territory.GetLength(0); i++)
            {
                for (int j = 0; j < territory.GetLength(1); j++)
                {
                    Console.Write(territory[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}