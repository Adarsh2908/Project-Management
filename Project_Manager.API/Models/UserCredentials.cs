namespace Project_Manager.API.Models
{
    public class UserCredentials
    {
        public int id { get; set; }
        public bool isVerified { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
