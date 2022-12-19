using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;
using Todolender.API.Repositories;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [ActionName("CreateUserAsync")]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            // convert DTO to domain model
            var user = new User()
            {
                Email = createUserRequest.Email,
                PasswordHash = createUserRequest.PasswordHash,
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                Mobile = createUserRequest.Mobile,
                CurrentGoal = createUserRequest.CurrentGoal
            };

            // use repository to create 
            user = await userRepository.CreateUserAsync(user);

            // convert domain back to DTO 
            var userDto = new UserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                CurrentGoal = user.CurrentGoal,
                LastActive = user.LastActive
            };

            // give user response + DTO
            return new CreatedAtActionResult(nameof(CreateUserAsync), "Auth", new { id = userDto.Id }, userDto);

            //return new CreatedAtAction(nameof(CreateUserAsync), new { id = userDto.Id }, userDto);

        }
    }
}
