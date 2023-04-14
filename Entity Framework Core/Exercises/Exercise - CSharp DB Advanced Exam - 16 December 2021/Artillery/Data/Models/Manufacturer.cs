using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Artillery.Data.Models
{
    
    public class Manufacturer
    {
        public Manufacturer()
        {
            Guns = new List<Gun>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(40), Required]
        public string ManufacturerName { get; set; }
        [MaxLength(100), Required]
        public string Founded { get; set; }
        public ICollection<Gun> Guns { get; set; }
    }
}
