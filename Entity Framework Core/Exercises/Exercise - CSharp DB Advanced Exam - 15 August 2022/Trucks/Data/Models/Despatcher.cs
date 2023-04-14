﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trucks.Data.Models
{
    public class Despatcher
    {
        public Despatcher()
        {
            Trucks = new List<Truck>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(40)]
        public string Name { get; set; }
        public string? Position { get; set; }

        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
