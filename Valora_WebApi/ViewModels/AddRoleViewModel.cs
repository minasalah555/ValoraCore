using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class AddRoleViewModel
    {
        [Display(Name ="Add Role")]
        [Required(ErrorMessage ="*")]
        public String RoleName { get; set; }
    }
}
