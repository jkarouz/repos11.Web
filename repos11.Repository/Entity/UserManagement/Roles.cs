using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repos11.Repository.Entity.UserManagement
{
    public class Roles : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength]
        public string Description { get; set; }
    }
}
