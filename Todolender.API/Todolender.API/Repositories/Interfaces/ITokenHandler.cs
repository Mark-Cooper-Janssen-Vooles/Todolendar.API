using Todolender.API.Models.Domain;

namespace Todolender.API.Repositories.Interfaces
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
