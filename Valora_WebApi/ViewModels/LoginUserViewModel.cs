using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage ="*")]
        public string UserName { get; set; }
        
        
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        
        [Required(ErrorMessage = "*")]
        [Display(Name = "Remember Me !!!")]
        public bool RememberMe { get; set; }


    }
}
