using SpaceStation.Models.Bags.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Bags
{
    public class Backpack : IBag
    {
        private ICollection<string> items;
        public Backpack()
        {
            items = new List<string>();
        }
        public ICollection<string> Items { get { return items; } }
    }
}
