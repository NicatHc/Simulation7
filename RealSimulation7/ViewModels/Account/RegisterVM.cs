using System.ComponentModel.DataAnnotations;

namespace RealSimulation7.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="melumat daxil edilmelidir")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "melumat daxil edilmelidir")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "melumat daxil edilmelidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirmed { get; set; }
    }
}
