using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class AddRoleToUserViewModel
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }
    }
}
