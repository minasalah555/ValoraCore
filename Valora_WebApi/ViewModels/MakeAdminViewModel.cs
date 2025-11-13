using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class MakeAdminViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}
