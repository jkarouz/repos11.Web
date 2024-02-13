using System.ComponentModel.DataAnnotations;

namespace repos11.Repository.Entity.UserManagement
{
    public class RolePermissions
    {
        [Required]
        public long RoleId { get; set; }

        [Required]
        public long PermissionId { get; set; }
    }
}
