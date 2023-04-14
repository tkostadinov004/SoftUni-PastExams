using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Flower_Wreaths
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> roses = Console.ReadLine().Split(", ").Select(int.Parse).ToList();
            List<int> lilies = Console.ReadLine().Split(", ").Select(int.Parse).ToList();

            int wreathCount = 0;
            int leftoverSum = 0;

            while (roses.Count > 0 && lilies.Count > 0)
            {
                int sum = roses.First() + lilies.Last();
                if (sum == 15)
                {
                    wreathCount++;
                    roses.RemoveAt(0);
                    lilies.RemoveAt(lilies.Count - 1);
                }
                else if (sum > 15)
                {
                    lilies[^1] -= 2;
                }
                else
                {
                    leftoverSum += sum;
                    roses.RemoveAt(0);
                    lilies.RemoveAt(lilies.Count - 1);
                }
            }

            wreathCount += (leftoverSum / 15);
            Console.WriteLine(wreathCount >= 5 ? $"You made it, you are going to the competition with {wreathCount} wreaths!" : $"You didn't make it, you need {5 - wreathCount} wreaths more!");
        }
    }
}