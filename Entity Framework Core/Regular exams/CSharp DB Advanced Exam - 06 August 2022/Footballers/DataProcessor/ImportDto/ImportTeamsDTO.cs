using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Footballers.DataProcessor.ImportDto
{
    public class ImportTeamsDTO
    {
        [StringLength(40, MinimumLength = 3), Required, RegularExpression(@"[\sA-Za-z0-9\.\-]+")]
        public string Name { get; set; }
        [Required, StringLength(40, MinimumLength = 2)]
        public string Nationality { get; set; }
        public string Trophies { get; set; }
        public int[] Footballers { get; set; }
    }
}
