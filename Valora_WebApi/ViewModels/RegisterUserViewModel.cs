using System.ComponentModel.DataAnnotations;

namespace Valora.ViewModels
{
    public class RegisterUserViewModel
    {
        [Display(Name= "User Name")]
        public string UserName { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]

        public string ConfirmPassword { get; set; }

   
    }
}
