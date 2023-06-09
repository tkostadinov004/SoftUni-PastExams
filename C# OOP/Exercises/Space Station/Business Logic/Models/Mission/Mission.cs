﻿using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (IAstronaut astronaut in astronauts)
            {
                while (astronaut.CanBreath)
                {
                    if (planet.Items.Count == 0)
                    {
                        break;
                    }
                    string item = planet.Items.First();

                    astronaut.Breath();

                    astronaut.Bag.Items.Add(item);
                    planet.Items.Remove(item);
                }
            }
        }
    }
}
