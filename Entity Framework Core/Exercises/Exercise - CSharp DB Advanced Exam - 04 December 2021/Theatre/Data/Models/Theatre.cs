﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.Data.Models
{
    public class Theatre
    {
        public Theatre()
        {
            Tickets = new List<Ticket>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public sbyte NumberOfHalls { get; set; }
        [Required]
        public string Director { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
