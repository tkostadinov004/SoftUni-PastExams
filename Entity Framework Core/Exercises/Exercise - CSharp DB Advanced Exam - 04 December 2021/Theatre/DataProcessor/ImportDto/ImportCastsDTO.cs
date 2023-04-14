using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Cast")]
    public class ImportCastsDTO
    {
        [StringLength(30, MinimumLength = 4), Required]
        public string FullName { get; set; }
        public bool IsMainCharacter { get; set; }
        [RegularExpression(@"\+[4][4]-([0-9]){2}-([0-9]){3}-([0-9]){4}")]
        public string PhoneNumber { get; set; }
        public int PlayId { get; set; }
    }
}
