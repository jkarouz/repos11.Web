using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repos11.Repository.Entity.UserManagement
{
    public class Users : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string EmployeeCode { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [MaxLength(100)]
        public string UserCode { get; set; }
    }
}
