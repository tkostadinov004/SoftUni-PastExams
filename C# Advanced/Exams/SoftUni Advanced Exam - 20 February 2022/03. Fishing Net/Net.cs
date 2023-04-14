using System;
using System.Collections.Generic;
using System.Linq;

namespace FishingNet
{
    public class Net
    {
        public List<Fish> Fish { get; private set; }

        public Net(string material, int capacity)
        {
            Material = material;
            Capacity = capacity;
            Fish = new List<Fish>();
        }

        public string Material { get; set; }
        public int Capacity { get; set; }

        public int Count => Fish.Count;

        public string AddFish(Fish fish)
        {
            if (Fish.Count + 1 <= Capacity)
            {
                if (string.IsNullOrWhiteSpace(fish.FishType) || fish.Length <= 0 || fish.Weight <= 0)
                {
                    return "Invalid fish.";
                }
                else
                {
                    Fish.Add(fish);
                    return $"Successfully added {fish.FishType} to the fishing net.";
                }
            }
            else
            {
                return "Fishing net is full.";
            }
        }
        public bool ReleaseFish(double weight)
        {
            Fish fish = Fish.Where(i => i.Weight == weight).FirstOrDefault();
            if (fish != null)
            {
                Fish.Remove(fish);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Fish GetFish(string fishType)
        {
            return Fish.First(i => i.FishType == fishType);
        }
        public Fish GetBiggestFish()
        {
            return Fish.OrderByDescending(i => i.Length).First();
        }
        public string Report()
        {
            string ans = $"Into the {Material}:{Environment.NewLine}{string.Join(Environment.NewLine, Fish.OrderByDescending(i => i.Length))}";        
            return ans;
        }
    }
}
