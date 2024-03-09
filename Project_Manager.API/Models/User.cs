namespace Project_Manager.API.Models
{
    public class User
    {
        public int userId { get; set; }
        public string fullName { get; set; }  = string.Empty;
        public string email { get; set; } = string.Empty;
        public string role { get; set; } = "Admin";
        public DateTime creationDate { get; set; }

    }
}
