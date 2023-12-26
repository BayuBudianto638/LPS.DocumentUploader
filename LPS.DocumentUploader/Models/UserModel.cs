namespace LPS.DocumentUploader.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int NoHP { get; set; }
        public string PasswordSalt { get; set; }
    }
}
