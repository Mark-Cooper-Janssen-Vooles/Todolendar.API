using Microsoft.EntityFrameworkCore;
using Todolendar.API.Data;
using Todolendar.API.Models.Domain;
using Todolendar.API.Repositories.Interfaces;
using Todolendar.API.Models.DTO;
using System.Security.Cryptography;

namespace Todolendar.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodolendarDbContext dbContext;
        private readonly IHashHandler hashHandler;

        public UserRepository(TodolendarDbContext dbContext, IHashHandler hashHandler)
        {
            this.dbContext = dbContext;
            this.hashHandler = hashHandler; 
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            // TODO: need to convert password hash here somehow 

            // first find the user based on email.
            // get the salt
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(
                x => x.Email.ToLower() == email.ToLower());
            if (existingUser == null) return null;

            Console.WriteLine(existingUser.PasswordSalt);

            var validatedHash = hashHandler.ValidateHashedPassword(password, existingUser.PasswordHash, existingUser.PasswordSalt);

            // if so, authenticate user. 

            if (validatedHash)
            {
                existingUser.PasswordHash = null;
                return existingUser;
            }

            return null;

            //var user = await dbContext.Users.FirstOrDefaultAsync( 
            //    x => x.Email.ToLower() == email.ToLower() &&
            //    x.PasswordHash == password.ToLower());
            //if (user == null) return null;



            //user.PasswordHash = null;
            //return user;
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