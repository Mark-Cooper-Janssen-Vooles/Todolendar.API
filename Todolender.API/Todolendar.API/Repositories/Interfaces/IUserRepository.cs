using Todolendar.API.Models.Domain;
using Todolendar.API.Models.DTO;

namespace Todolendar.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string email, string password);
        Task<User> CreateUserAsync(User user);
        Task<User> DeleteUserAsync(Guid id);
        Task<User> GetUserAsync(Guid id);
        Task<User> UpdateUserAsync(Guid id, User user);
    }
}
