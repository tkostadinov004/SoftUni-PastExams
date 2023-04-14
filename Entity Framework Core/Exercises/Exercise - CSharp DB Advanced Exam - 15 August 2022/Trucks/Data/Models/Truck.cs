﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Trucks.Data.Models.Enums;

namespace Trucks.Data.Models
{
    public class Truck
    {
        public Truck()
        {
            ClientsTrucks = new List<ClientTruck>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(8)]
        public string RegistrationNumber { get; set; }
        [MaxLength(17)]
        public string VinNumber { get; set; }
        public int TankCapacity { get; set; }
        public int CargoCapacity { get; set; }
        [Required]
        public CategoryType CategoryType { get; set; }
        [Required]
        public MakeType MakeType { get; set; }
        [Required, ForeignKey("Despatcher")]
        public int DespatcherId { get; set; }

        public virtual Despatcher Despatcher { get; set; }
        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }

    }
}
