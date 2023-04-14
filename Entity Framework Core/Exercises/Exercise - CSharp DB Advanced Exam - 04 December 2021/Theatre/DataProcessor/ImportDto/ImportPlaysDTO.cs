using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Theatre.Data.Models.Enums;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Play")]
    public class ImportPlaysDTO
    {
        [StringLength(50, MinimumLength = 4)]
        public string Title { get; set; }
        [Range(typeof(TimeSpan), "01:00:00", "10675199.02:48:05.4775807")]
        public string Duration { get; set; }
        [Range(typeof(float), "0.00", "10.00")]
        public float Rating { get; set; }
        [Required]
        public string Genre { get; set; }
        [MaxLength(700)]
        [Required]
        public string Description { get; set; }
        [StringLength(30, MinimumLength = 4)]
        public string Screenwriter { get; set; }
    }
}
