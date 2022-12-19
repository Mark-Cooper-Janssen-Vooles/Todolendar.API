using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;

namespace Todolender.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
    }
}
