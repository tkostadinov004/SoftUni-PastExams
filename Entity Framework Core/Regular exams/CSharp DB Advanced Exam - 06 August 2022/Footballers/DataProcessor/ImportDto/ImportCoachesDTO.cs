using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Coach")]
    public class ImportCoachesDTO
    {
        [StringLength(40, MinimumLength = 2), Required]
        public string Name { get; set; }
        [Required]
        public string Nationality { get; set; }
        [XmlArray("Footballers")]
        public ImportCoachesFootballersDTO[] Footballers { get; set; }
    }
}
