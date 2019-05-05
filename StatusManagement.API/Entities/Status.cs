using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatusManagement.API.Entities
{
    public class Status : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StatusId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Summary { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public decimal EstimatedProfits { get; set; }

        [Required]
        public DateTimeOffset StartDate { get; set; }

        [Required]
        public DateTimeOffset EndDate { get; set; }

        [Required]
        public Guid MarshallId { get; set; }

        [Required]
        public Marshall Marshall { get; set; }

        public Guid ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Manager Manager { get; set; }

        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
