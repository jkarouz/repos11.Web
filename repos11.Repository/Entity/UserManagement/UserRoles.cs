using System.ComponentModel.DataAnnotations;

namespace repos11.Repository.Entity.UserManagement
{
    public class UserRoles
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long RoleId { get; set; }
    }
}
