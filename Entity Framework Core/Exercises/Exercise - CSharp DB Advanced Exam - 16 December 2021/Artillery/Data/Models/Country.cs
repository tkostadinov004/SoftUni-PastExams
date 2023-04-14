using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Artillery.Data.Models
{
    public class Country
    {
        public Country()
        {
            CountriesGuns = new List<CountryGun>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(60), Required]
        public string CountryName { get; set; }
        public int ArmySize { get; set; }
        public ICollection<CountryGun> CountriesGuns { get; set; }
    }
}
