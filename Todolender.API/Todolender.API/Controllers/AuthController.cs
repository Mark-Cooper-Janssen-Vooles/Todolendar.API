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

        [HttpPut]
        [Route("user")]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserRequest updateUserRequest)
        {
            // TODO: Validations 
            // TODO: Authenticated (this needs to be the correct user logged in to do this!

            // convert DTO to domain model 
            var user = new User()
            {
                Email = updateUserRequest.Email,
                PasswordHash = updateUserRequest.PasswordHash,
                FirstName = updateUserRequest.FirstName,
                LastName = updateUserRequest.LastName,
                Mobile = updateUserRequest.Mobile,
                CurrentGoal = updateUserRequest.CurrentGoal
            };

            // Update user using repository 
            user = await userRepository.UpdateUserAsync(id, user);
            if (user == null)
            {
                return NotFound();
            }

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

            // return OK response 
            return Ok(userDto);
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
