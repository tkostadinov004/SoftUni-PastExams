using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Bakery_Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> goods = new Dictionary<string, double>()
            {
                {"Croissant", 50 },
                {"Muffin", 40 },
                {"Baguette", 30  },
                {"Bagel", 20  }
            };

            Queue<double> water = new Queue<double>(Console.ReadLine().Split().Select(double.Parse));
            Stack<double> flour = new Stack<double>(Console.ReadLine().Split().Select(double.Parse));

            Dictionary<string, int> baked = new Dictionary<string, int>();

            while (water.Count > 0 && flour.Count > 0)
            {
                double sum = water.Peek() + flour.Peek();
                double waterPercentage = (water.Peek() * 100) / sum;

                string name;
                if (goods.ContainsValue(waterPercentage))
                {
                    flour.Pop();
                    name = goods.First(i => i.Value == waterPercentage).Key;
                }
                else
                {
                    name = "Croissant";
                    double flourToTake = water.Peek();
                    flour.Push(flour.Pop() - flourToTake);
                }

                water.Dequeue();
                if (!baked.ContainsKey(name))
                {
                    baked.Add(name, 0);
                }
                baked[name]++;
            }

            foreach (var item in baked.OrderByDescending(i => i.Value).ThenBy(i => i.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            Console.WriteLine($"Water left: {(water.Count == 0 ? "None" : string.Join(", ", water))}");
            Console.WriteLine($"Flour left: {(flour.Count == 0 ? "None" : string.Join(", ", flour))}");
        }
    }
}
