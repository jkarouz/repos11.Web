using System.ComponentModel.DataAnnotations;

namespace repos11.Repository.Entity.UserManagement
{
    public class GroupRoles
    {
        [Required]
        public long GroupId { get; set; }

        [Required]
        public long RoleId { get; set; }
    }
}
