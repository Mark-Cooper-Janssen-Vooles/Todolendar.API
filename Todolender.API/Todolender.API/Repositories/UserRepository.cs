using Todolender.API.Data;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;

namespace Todolender.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodolenderDbContext dbContext;

        public UserRepository(TodolenderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.LastActive = DateTime.Now;

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
