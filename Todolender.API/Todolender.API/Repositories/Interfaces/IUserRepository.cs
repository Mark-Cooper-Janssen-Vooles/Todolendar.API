using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;

namespace Todolender.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string email, string password);
        Task<User> CreateUserAsync(User user);
        Task<User> DeleteUserAsync(Guid id);
        Task<User> UpdateUserAsync(Guid id, User user);
    }
}
