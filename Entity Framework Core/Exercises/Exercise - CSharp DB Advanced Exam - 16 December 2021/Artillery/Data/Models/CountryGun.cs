using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Artillery.Data.Models
{
    public class CountryGun
    {
        [ForeignKey(nameof(Country)), Required]
        public int CountryId { get; set; }
        [ForeignKey(nameof(Gun)), Required]
        public int GunId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Gun Gun { get; set; }
    }
}
