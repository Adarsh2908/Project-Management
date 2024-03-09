using System.ComponentModel.DataAnnotations;

namespace Project_Manager.API.Models.DTO
{
    public class NewUserDto
    {
        [Required(ErrorMessage = "Full Name is Required")]
        public string fullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Full Name is Required")]
        public string role { get; set; }

    }
}
