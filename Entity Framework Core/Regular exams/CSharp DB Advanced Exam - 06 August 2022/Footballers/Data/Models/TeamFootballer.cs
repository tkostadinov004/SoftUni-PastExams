﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Footballers.Data.Models
{
    public class TeamFootballer
    {
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public Team Team { get; set; }

        [ForeignKey(nameof(Footballer))]
        public int FootballerId { get; set; }
        public Footballer Footballer { get; set; }
    }
}
