using Artillery.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ExportDto
{
    [XmlType("Gun")]
    public class ExportGunsDTO
    {
        [XmlAttribute]
        public string Manufacturer { get; set; }
        [XmlAttribute]
        public GunType GunType { get; set; }
        [XmlAttribute]
        public double GunWeight { get; set; }
        [XmlAttribute]
        public double BarrelLength { get; set; }
        [XmlAttribute]
        public int Range { get; set; }

        [XmlArray]
        public ExportCountriesDTO[] Countries { get; set; }
    }
}
