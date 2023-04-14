using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Manufacturer")]
    public class ImportManufacturersDTO
    {
        [StringLength(40, MinimumLength = 4), Required]
        public string ManufacturerName { get; set; }
        [StringLength(100, MinimumLength = 10), Required]
        public string Founded { get; set; }
    }
}
