using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> internalModels = new List<IPlanet>();
        public IReadOnlyCollection<IPlanet> Models
        {
            get { return internalModels.AsReadOnly(); }
        }

        public void Add(IPlanet planet)
        {
            internalModels.Add(planet);
        }

        public IPlanet FindByName(string name)
        {
            return internalModels.FirstOrDefault(i => i.Name == name);
        }

        public bool Remove(IPlanet planet)
        {
            return internalModels.Remove(planet);
        }
    }
}
