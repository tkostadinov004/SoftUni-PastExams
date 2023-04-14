using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto.SubDTOs
{
    [XmlType("Truck")]
    public class ExportDespatchersTrucksDTO
    {
        public string RegistrationNumber { get; set; }
        public string Make { get; set; }
    }
}
