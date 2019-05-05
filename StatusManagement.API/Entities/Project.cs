using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatusManagement.API.Entities
{ 
    [Table("Projects")]
    public class Project : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProjectId { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        [Required]
        [MaxLength(150)]
        public string Venue { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        public Guid StatusId { get; set; }
    }
}
