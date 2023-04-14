using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private List<IAstronaut> internalModels = new List<IAstronaut>();
        public IReadOnlyCollection<IAstronaut> Models
        {
            get { return internalModels.AsReadOnly(); }
        }

        public void Add(IAstronaut model)
        {
            internalModels.Add(model);
        }

        public IAstronaut FindByName(string name)
        {
            return internalModels.FirstOrDefault(i => i.Name == name);
        }

        public bool Remove(IAstronaut model)
        {
            return internalModels.Remove(model);
        }
    }
}
