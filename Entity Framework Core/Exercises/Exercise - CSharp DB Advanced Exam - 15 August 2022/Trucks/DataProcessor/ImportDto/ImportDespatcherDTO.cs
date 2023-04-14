using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Trucks.DataProcessor.ImportDto.SubDTOs;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Despatcher")]
    public class ImportDespatcherDTO
    {
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }
        public string Position { get; set; }
        [XmlArray("Trucks")]
        public ImportDespatcherTruckDTO[] Trucks { get; set; }
    }
}
