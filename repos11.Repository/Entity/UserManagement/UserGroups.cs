using System.ComponentModel.DataAnnotations;

namespace repos11.Repository.Entity.UserManagement
{
    public class UserGroups
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public long GroupId { get; set; }
    }
}
