using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Trucks.DataProcessor.ExportDto.SubDTOs;

namespace Trucks.DataProcessor.ExportDto
{
    [XmlType("Despatcher")]
    public class ExportDespatchersDTO
    {
        [XmlAttribute]
        public int TrucksCount { get; set; }

        public string DespatcherName { get; set; }
        public ExportDespatchersTrucksDTO[] Trucks { get; set; }
    }
}
