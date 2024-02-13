using System.ComponentModel.DataAnnotations;

namespace repos11.Repository.Entity.UserManagement
{
    public class PermissionActions
    {
        [Required]
        public long PermissionId { get; set; }

        [Required]
        public long ActionId { get; set; }
    }
}
