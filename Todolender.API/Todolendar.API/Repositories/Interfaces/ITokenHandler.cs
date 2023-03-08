using Todolendar.API.Models.Domain;

namespace Todolendar.API.Repositories.Interfaces
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
