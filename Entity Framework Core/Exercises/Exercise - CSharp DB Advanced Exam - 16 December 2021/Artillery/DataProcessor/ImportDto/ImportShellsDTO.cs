using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Shell")]
    public class ImportShellsDTO
    {
        [Range(2d, 1680d)]
        public double ShellWeight { get; set; }
        [StringLength(30, MinimumLength = 4), Required]
        public string Caliber { get; set; }
    }
}
