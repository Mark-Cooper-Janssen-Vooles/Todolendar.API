using Microsoft.EntityFrameworkCore;
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

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            // TODO: need to convert password hash here somehow 

            var user = await dbContext.Users.FirstOrDefaultAsync( 
                x => x.Email.ToLower() == email.ToLower() &&
                x.PasswordHash == password.ToLower());

            if (user == null) {
                return null;
            }

            user.PasswordHash = null;
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.LastActive = DateTime.Now;

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUserAsync(Guid id, User updatedUser)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.Email = updatedUser.Email;
            existingUser.PasswordHash = updatedUser.PasswordHash;
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Mobile = updatedUser.Mobile;
            existingUser.CurrentGoal = updatedUser.CurrentGoal;
            await dbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}