using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VetClinic
{
    class Clinic
    {
        private List<Pet> data;
        public int Capacity { get; private set; }
        public Clinic(int capacity)
        {
            data = new List<Pet>();
            Capacity = capacity;
        }

        public void Add(Pet pet)
        {
            if (data.Count + 1 <= Capacity)
            {
                data.Add(pet);
            }
        }
        public bool Remove(string name)
        {
            int index = data.FindIndex(i => i.Name == name);
            if (index != -1)
            {
                data.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Pet GetPet(string name, string owner)
        {
            int index = data.FindIndex(i => i.Name == name && i.Owner == owner);
            if (index != -1)
            {
                return data[index];
            }
            else
            {
                return null;
            }
        }
        public Pet GetOldestPet()
        {
            return data.OrderBy(i => i.Age).Last();
        }
        public int Count => data.Count;

        public string GetStatistics()
        {
            string ans = "The clinic has the following patients:\n";
            data.ForEach(i => ans+=($"Pet {i.Name} with owner: {i.Owner}\n"));
            return ans;
        }
    }
}
