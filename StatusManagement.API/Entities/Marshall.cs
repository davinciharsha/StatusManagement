using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatusManagement.API.Entities
{
    public class Marshall : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MarshallId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
