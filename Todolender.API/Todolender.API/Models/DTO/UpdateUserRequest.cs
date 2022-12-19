namespace Todolender.API.Models.DTO
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string CurrentGoal { get; set; }
    }
}