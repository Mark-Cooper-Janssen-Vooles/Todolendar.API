using Microsoft.EntityFrameworkCore;
using Todolendar.API.Data;
using Todolendar.API.Models.Domain;
using Todolendar.API.Repositories.Interfaces;
using Todolendar.API.Models.DTO;

namespace Todolendar.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodolendarDbContext dbContext;

        public UserRepository(TodolendarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            // TODO: need to convert password hash here somehow 

            var user = await dbContext.Users.FirstOrDefaultAsync( 
                x => x.Email.ToLower() == email.ToLower() &&
                x.PasswordHash == password.ToLower());
            if (user == null) return null;

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

        public async Task<User> DeleteUserAsync(Guid id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return null;

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null) return null;

            return existingUser;
        }

        public async Task<User> UpdateUserAsync(Guid id, User updatedUser)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null) return null;

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