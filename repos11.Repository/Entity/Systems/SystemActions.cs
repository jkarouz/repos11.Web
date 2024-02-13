using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repos11.Repository.Entity.Systems
{
    public class SystemActions : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(200)]
        public string Controller { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(200)]
        public string Action { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength]
        public string Description { get; set; }
    }
}
