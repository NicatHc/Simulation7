using System.ComponentModel.DataAnnotations;

namespace RealSimulation7.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "melumat daxil edilmelidir")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "melumat daxil edilmelidir")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }
}
