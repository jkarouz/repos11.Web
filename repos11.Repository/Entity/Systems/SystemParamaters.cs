using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repos11.Repository.Entity.Systems
{
    public class SystemParamaters
    {
        [Key]
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(200)]
        public string Value1 { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(200)]
        public string Value2 { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength]
        public string Description { get; set; }
    }
}
