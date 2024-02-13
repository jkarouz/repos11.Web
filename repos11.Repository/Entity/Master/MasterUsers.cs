using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repos11.Repository.Entity.Master
{
    public class MasterUsers : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
