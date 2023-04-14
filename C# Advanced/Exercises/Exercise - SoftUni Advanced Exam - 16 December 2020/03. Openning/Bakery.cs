using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryOpenning
{
    class Bakery
    {
        private List<Employee> data;

        public Bakery(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            data = new List<Employee>();
        }

        public string Name { get; set; }
        public int Capacity { get; private set; }

        public int Count => data.Count;

        public void Add(Employee employee)
        {
            if (data.Count + 1 <= Capacity)
            {
                data.Add(employee);
            }
        }
        public bool Remove(string name)
        {
            if (data.Select(i => i.Name).Contains(name))
            {
                Employee employeeToGetRemoved = data.First(i => i.Name == name);
                data.Remove(employeeToGetRemoved);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Employee GetOldestEmployee()
        {
            return data.OrderByDescending(i => i.Age).First();
        }
        public Employee GetEmployee(string name)
        {
            return data.First(i => i.Name == name);
        }

        public string Report()
        {
            return $"Employees working at Bakery {Name}:\n{string.Join("\n", data)}";
        }
    }
}
