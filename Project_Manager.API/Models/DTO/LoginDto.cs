using System.ComponentModel.DataAnnotations;

namespace Project_Manager.API.Models.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage ="User Email is Required")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string password { get; set; }

    }
}
