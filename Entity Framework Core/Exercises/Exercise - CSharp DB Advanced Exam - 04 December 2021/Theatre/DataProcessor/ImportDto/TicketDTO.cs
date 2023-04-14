using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.DataProcessor.ImportDto
{
   public class TicketDTO
    {
        [Range(typeof(decimal), "1.00", "100.00"),Required]
        public decimal Price { get; set; }
        [Range(typeof(sbyte), "1", "10"),Required]
        public sbyte RowNumber { get; set; }
        public int PlayId { get; set; }
    }
}
