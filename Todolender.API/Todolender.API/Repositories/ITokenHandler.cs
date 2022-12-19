using Todolender.API.Models.Domain;

namespace Todolender.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
