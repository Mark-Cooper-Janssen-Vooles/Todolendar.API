namespace Todolendar.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string CurrentGoal { get; set; }
        public DateTime LastActive { get; set; }
    }
}
