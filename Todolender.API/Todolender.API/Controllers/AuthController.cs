using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO;
using Todolender.API.Repositories;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("user")]
        [ActionName("CreateUserAsync")]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            // TODO: validations 
            // TODO: Mapper

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
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            // TODO: Validations 

            // check if user is authenticated 
            var user = await userRepository.AuthenticateUserAsync(loginRequest.Email, loginRequest.Password);

            if ( user != null )
            {
                return Ok(user); // this will need to be changed to token later
            }

            return BadRequest("Email or password is incorrect.");

        }
    }
}
