using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> knights = players.Where(i => i.GetType().Name == "Knight").ToList();
            List<IHero> barbarians = players.Where(i => i.GetType().Name == "Barbarian").ToList();

            while (knights.Count() > 0 && barbarians.Count() > 0)
            {
                foreach (var item in knights)
                {
                    foreach (var barb in barbarians)
                    {
                        barb.TakeDamage(item.Weapon.DoDamage());
                    }
                }
                knights = knights.Where(i => i.IsAlive).ToList();
                barbarians = barbarians.Where(i => i.IsAlive).ToList();
                foreach (var item in barbarians)
                {
                    foreach (var kn in knights)
                    {
                        kn.TakeDamage(item.Weapon.DoDamage());
                    }
                }
                knights = knights.Where(i => i.IsAlive).ToList();
                barbarians = barbarians.Where(i => i.IsAlive).ToList();
            }
            int aliveKnights = knights.Count();
            int aliveBarbs = barbarians.Count();
            return aliveKnights == 0 ? $"The barbarians took {players.Where(i => i.GetType().Name == "Barbarian").Count() - aliveBarbs} casualties but won the battle." : $"The knights took {players.Where(i => i.GetType().Name == "Knight").Count() - aliveKnights} casualties but won the battle.";
        }
    }
}
