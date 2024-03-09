using System.ComponentModel.DataAnnotations;

namespace Project_Manager.API.Models
{
    public class UserLogin
    {
        public int userId { get; set; }
        public bool isVerified { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
