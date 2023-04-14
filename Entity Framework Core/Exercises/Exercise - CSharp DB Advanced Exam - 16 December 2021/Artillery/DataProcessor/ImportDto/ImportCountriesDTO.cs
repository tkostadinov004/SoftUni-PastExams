using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Country")]
    public class ImportCountriesDTO
    {
        [StringLength(60, MinimumLength = 4), Required]
        public string CountryName { get; set; }
        [Range(50000, 10000000)]
        public int ArmySize { get; set; }
    }
}
