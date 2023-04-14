using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ExportDto
{
    [XmlType("Coach")]
    public class ExportCoachDTO
    {
        [XmlAttribute]
        public int FootballersCount { get; set; }

        [XmlElement]
        public string CoachName { get; set; }
        [XmlArray]
        public ExportFootballerDTO[] Footballers { get; set; }
    }
}
