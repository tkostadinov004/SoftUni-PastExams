using System;
using System.Collections.Generic;

namespace _02._Beaver_at_Work
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());

            Coords beaverCoords = new Coords();
            char[,] pond = new char[size, size];

            int initialBranchesCount = 0;
            for (int i = 0; i < size; i++)
            {
                string[] line = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < size; j++)
                {
                    pond[i, j] = char.Parse(line[j]);
                    if (char.IsLower(pond[i, j]))
                    {
                        initialBranchesCount++;
                    }
                    if (pond[i, j] == 'B')
                    {
                        beaverCoords = new Coords(i, j);
                    }
                }
            }
            string command;

            int noRemovalBranchesCount = 0;
            List<char> branches = new List<char>();

            while (ContainsBranches(pond) && (command = Console.ReadLine()) != "end")
            {
                if (command == "up")
                {
                    if (beaverCoords.Row - 1 >= 0)
                    {
                        if (char.IsLower(pond[beaverCoords.Row - 1, beaverCoords.Col]))
                        {
                            branches.Add(pond[beaverCoords.Row - 1, beaverCoords.Col]);
                            pond[beaverCoords.Row - 1, beaverCoords.Col] = 'B'; noRemovalBranchesCount++;
                        }
                        else if (pond[beaverCoords.Row - 1, beaverCoords.Col] == 'F')
                        {
                            pond[size - 1, beaverCoords.Col] = 'B';
                            pond[beaverCoords.Row - 1, beaverCoords.Col] = '-';
                        }
                        else
                        {
                            pond[beaverCoords.Row - 1, beaverCoords.Col] = 'B';
                        }
                        pond[beaverCoords.Row, beaverCoords.Col] = '-';
                    }
                    else
                    {
                        if (branches.Count > 0)
                        {
                            branches.RemoveAt(branches.Count - 1);
                        }
                    }
                }
                else if (command == "down")
                {
                    if (beaverCoords.Row + 1 <= size - 1)
                    {
                        if (char.IsLower(pond[beaverCoords.Row + 1, beaverCoords.Col]))
                        {
                            branches.Add(pond[beaverCoords.Row + 1, beaverCoords.Col]);
                            pond[beaverCoords.Row + 1, beaverCoords.Col] = 'B'; noRemovalBranchesCount++;
                        }
                        else if (pond[beaverCoords.Row + 1, beaverCoords.Col] == 'F')
                        {
                            if (beaverCoords.Row + 1 == size - 1)
                            {
                                pond[0, beaverCoords.Col] = 'B';
                            }
                            else
                            {
                                pond[size - 1, beaverCoords.Col] = 'B';
                            }
                            pond[beaverCoords.Row + 1, beaverCoords.Col] = '-';
                        }
                        else
                        {
                            pond[beaverCoords.Row + 1, beaverCoords.Col] = 'B';
                        }
                        pond[beaverCoords.Row, beaverCoords.Col] = '-';
                    }
                    else
                    {
                        if (branches.Count > 0)
                        {
                            branches.RemoveAt(branches.Count - 1);
                        }
                    }
                }
                else if (command == "left")
                {
                    if (beaverCoords.Col - 1 >= 0)
                    {
                        if (char.IsLower(pond[beaverCoords.Row, beaverCoords.Col - 1]))
                        {
                            branches.Add(pond[beaverCoords.Row, beaverCoords.Col - 1]);
                            pond[beaverCoords.Row, beaverCoords.Col - 1] = 'B'; noRemovalBranchesCount++;
                        }
                        else if (pond[beaverCoords.Row, beaverCoords.Col - 1] == 'F')
                        {
                            pond[beaverCoords.Row, size - 1] = 'B';
                            pond[beaverCoords.Row, beaverCoords.Col - 1] = '-';
                        }
                        else
                        {
                            pond[beaverCoords.Row, beaverCoords.Col - 1] = 'B';
                        }
                        pond[beaverCoords.Row, beaverCoords.Col] = '-';
                    }
                    else
                    {
                        if (branches.Count > 0)
                        {
                            branches.RemoveAt(branches.Count - 1);
                        }
                    }
                }
                else if (command == "right")
                {
                    if (beaverCoords.Col + 1 <= size - 1)
                    {
                        if (char.IsLower(pond[beaverCoords.Row, beaverCoords.Col + 1]))
                        {
                            branches.Add(pond[beaverCoords.Row, beaverCoords.Col + 1]);
                            pond[beaverCoords.Row, beaverCoords.Col + 1] = 'B'; noRemovalBranchesCount++;
                        }
                        else if (pond[beaverCoords.Row, beaverCoords.Col + 1] == 'F')
                        {
                            if (beaverCoords.Col + 1 == size - 1)
                            {
                                pond[beaverCoords.Row, 0] = 'B';
                            }
                            else
                            {
                                pond[beaverCoords.Row, size - 1] = 'B';
                            }
                            pond[beaverCoords.Row, beaverCoords.Col + 1] = '-';
                        }
                        else
                        {
                            pond[beaverCoords.Row, beaverCoords.Col + 1] = 'B';
                        }
                        pond[beaverCoords.Row, beaverCoords.Col] = '-';
                    }
                    else
                    {
                        if (branches.Count > 0)
                        {
                            branches.RemoveAt(branches.Count - 1);
                        }
                    }
                }
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (pond[i, j] == 'B')
                        {
                            beaverCoords.Row = i;
                            beaverCoords.Col = j;
                        }
                    }
                }
            }
            Console.WriteLine(ContainsBranches(pond) == false ? $"The Beaver successfully collect {branches.Count} wood branches: {string.Join(", ", branches)}." : $"The Beaver failed to collect every wood branch. There are {initialBranchesCount - noRemovalBranchesCount} branches left.");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j != size - 1)
                    {
                        Console.Write(pond[i, j] + " ");
                    }
                    else
                    {
                        Console.Write(pond[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }
        static bool ContainsBranches(char[,] pond)
        {
            for (int i = 0; i < pond.GetLength(0); i++)
            {
                for (int j = 0; j < pond.GetLength(1); j++)
                {
                    if (char.IsLower(pond[i, j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    class Coords
    {
        public Coords()
        {
            Row = -1;
            Col = -1;
        }
        public Coords(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }
}