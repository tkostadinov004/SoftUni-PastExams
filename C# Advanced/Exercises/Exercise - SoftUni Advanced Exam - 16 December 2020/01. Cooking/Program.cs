using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Cooking
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> foods = new Dictionary<string, int> { { "Bread", 25 }, { "Cake", 50 }, { "Pastry", 75 }, { "Fruit Pie", 100 } };

            Queue<int> liquids = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
            Stack<int> ingredients = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

            Dictionary<string, int> cookedFoods = new Dictionary<string, int> { { "Bread", 0 }, { "Cake", 0 }, { "Pastry", 0 }, { "Fruit Pie", 0 } };
            while (liquids.Count > 0 && ingredients.Count > 0)
            {
                int sum = liquids.Peek() + ingredients.Peek();
                if (foods.ContainsValue(sum))
                {
                    string foodName = foods.First(i => i.Value == sum).Key;
                    cookedFoods[foodName]++;
                    ingredients.Pop();
                }
                else
                {
                    ingredients.Push(ingredients.Pop() + 3);
                }
                liquids.Dequeue();
            }
            Console.WriteLine(!cookedFoods.ContainsValue(0) ? "Wohoo! You succeeded in cooking all the food!" : "Ugh, what a pity! You didn't have enough materials to cook everything.");
            Console.WriteLine(liquids.Count > 0 ? $"Liquids left: {string.Join(", ", liquids)}" : "Liquids left: none");
            Console.WriteLine(ingredients.Count > 0 ? $"Ingredients left: {string.Join(", ", ingredients)}" : "Ingredients left: none");
            foreach (var item in cookedFoods.OrderBy(i => i.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
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