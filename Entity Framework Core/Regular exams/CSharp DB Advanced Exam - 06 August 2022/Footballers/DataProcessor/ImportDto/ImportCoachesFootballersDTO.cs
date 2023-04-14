using Footballers.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Footballer")]
    public class ImportCoachesFootballersDTO
    {
        [StringLength(40, MinimumLength = 2), Required]
        public string Name { get; set; }
        [Required]
        public string ContractStartDate { get; set; }
        [Required]
        public string ContractEndDate { get; set; }
        [Required]
        public string PositionType { get; set; }
        [Required]
        public string BestSkillType { get; set; }
    }
}
