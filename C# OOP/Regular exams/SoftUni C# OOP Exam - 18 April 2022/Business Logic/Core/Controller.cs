using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heroes = new HeroRepository();
        private WeaponRepository weapons = new WeaponRepository();
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroes.FindByName(heroName);
            IWeapon weapon = weapons.FindByName(weaponName);

            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {heroName} does not exist.");
            }

            if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }

            hero.AddWeapon(weapon);
            return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }

            if (type != "Barbarian" && type != "Knight")
            {
                throw new InvalidOperationException("Invalid hero type.");
            }

            IHero hero;
            if (type == "Knight")
            {
                hero = new Knight(name, health, armour);
            }
            else
            {
                hero = new Barbarian(name, health, armour);
            }
            heroes.Add(hero);

            return $"Successfully added {hero.GetType().Name} {name} to the collection.";
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }

            if (type != "Claymore" && type != "Mace")
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }

            IWeapon weapon;
            if (type == "Claymore")
            {
                weapon = new Claymore(name, durability);
            }
            else
            {
                weapon = new Mace(name, durability);
            }
            weapons.Add(weapon);

            return $"A {type.ToLower()} {name} is added to the collection.";
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var hero in heroes.Models.OrderBy(i => i.GetType().Name).ThenByDescending(i => i.Health).ThenBy(i => i.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {(hero.Health < 0 ? 0 : hero.Health)}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                sb.AppendLine($"--Weapon: {(hero.Weapon != null ? hero.Weapon.Name : "Unarmed")}");
            }
            return sb.ToString().Trim();
        }

        public string StartBattle()
        {
            return new Map().Fight(heroes.Models.Where(i => i.IsAlive && i.Weapon != null).ToList());
        }
    }
}
