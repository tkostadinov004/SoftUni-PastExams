using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Trucks.DataProcessor.ImportDto.SubDTOs;

namespace Trucks.DataProcessor.ImportDto
{
    public class ImportClientDTO
    {
        [StringLength(40, MinimumLength = 3), Required]
        public string Name { get; set; }
        [StringLength(40, MinimumLength = 2), Required]
        public string Nationality { get; set; }
        [Required]
        public string Type { get; set; }
        public int[] Trucks { get; set; }
    }
}
