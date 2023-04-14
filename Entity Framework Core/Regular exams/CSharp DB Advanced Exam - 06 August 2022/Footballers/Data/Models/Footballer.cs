using Footballers.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Footballers.Data.Models
{
    public class Footballer
    {
        public Footballer()
        {
            TeamsFootballers = new List<TeamFootballer>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime	ContractStartDate { get; set; }
        public DateTime	ContractEndDate { get; set; }
        [Required]
        public PositionType PositionType { get; set; }
        [Required]
        public BestSkillType BestSkillType { get; set; }
        [ForeignKey(nameof(Coach))]
        public int CoachId  { get; set; }
        public Coach Coach { get; set; }
        public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; }
    }
}
