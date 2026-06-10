using System.ComponentModel.DataAnnotations;

namespace RealSimulation7.ViewModels.Worker
{
    public class UpdateVM
    {
        [Required(ErrorMessage = "Melumat daxil edilmelidir")]
        [MinLength(3, ErrorMessage = "melumat olcusu 3-den cox olmalidir")]
        [MaxLength(30, ErrorMessage = "melumat olcusu 30-dan az olmalidir")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Melumat daxil edilmelidir")]
        [MinLength(3, ErrorMessage = "melumat olcusu 3-den cox olmalidir")]
        [MaxLength(30, ErrorMessage = "melumat olcusu 30-dan az olmalidir")]
        public string Job { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo {  get; set; }
    }
}
