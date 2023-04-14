using System;
using System.Collections.Generic;
using System.Text;
using Trucks.DataProcessor.ExportDto.SubDTOs;

namespace Trucks.DataProcessor.ExportDto
{
    public class ExportClientsDTO
    {
        public string Name { get; set; }
        public ExportClientsTrucksDTO[] Trucks { get; set; }
    }
}
