using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper mapper;

        public AuthController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("user")]
        [ActionName("CreateUserAsync")]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            // convert DTO to domain model
            var user = mapper.Map<User>(createUserRequest);
            // use repository to create 
            user = await userRepository.CreateUserAsync(user);
            // convert domain back to DTO 
            var userDto = mapper.Map<UserDTO>(user);
            // give user response + DTO
            return new CreatedAtActionResult(nameof(CreateUserAsync), "Auth", new { id = userDto.Id }, userDto);
        }

        [HttpPut]
        [Authorize] // we only want the same user logged in to be able to update it
        [Route("user/{id:guid}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserRequest updateUserRequest)
        {
            // TODO: Authenticated (this needs to be the correct user logged in to do this!
            var user = mapper.Map<User>(updateUserRequest);
            user = await userRepository.UpdateUserAsync(id, user);
            if (user == null) return NotFound();

            var userDto = mapper.Map<UserDTO>(user);

            return Ok(userDto);
        }

        [HttpDelete]
        [Route("user/{id:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var user = await userRepository.DeleteUserAsync(id);
            if (user == null) return NotFound();

            var userDto = mapper.Map<UserDTO>(user);

            return Ok(userDto);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var user = await userRepository.AuthenticateUserAsync(loginRequest.Email, loginRequest.Password);
            if (user != null)
            {
                return Ok(user); // this will need to be changed to JWT token later
            }

            return BadRequest("Email or password is incorrect.");
        }
    }
}
