using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Artillery.Data.Models
{
    public class Shell
    {
        public Shell()
        {
            Guns = new List<Gun>();
        }
        [Key]
        public int Id { get; set; }
        public double ShellWeight { get; set; }
        [MaxLength(30), Required]
        public string Caliber { get; set; }
        public ICollection<Gun> Guns { get; set; }
    }
}
