using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.DataProcessor.ImportDto
{
    public class ImportTheatersTicketsDTO
    {
        [StringLength(30, MinimumLength = 4), Required]
        public string Name { get; set; }
        [Range(typeof(sbyte), "1", "10"), Required]
        public sbyte NumberOfHalls { get; set; }
        [StringLength(30, MinimumLength = 4), Required]
        public string Director { get; set; }
        public TicketDTO[] Tickets { get; set; }
    }
}
