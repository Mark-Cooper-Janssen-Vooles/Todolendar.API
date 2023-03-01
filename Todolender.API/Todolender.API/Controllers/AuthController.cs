using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todolender.API.Models.Domain;
using Todolender.API.Models.DTO.Auth;
using Todolender.API.Repositories.Interfaces;

namespace Todolender.API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, IMapper mapper, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.tokenHandler = tokenHandler;
        }
        
        [HttpGet]
        [Authorize(Policy = "user")] // we only want the same user logged in to be able to see their info
        [Route("user/{userId:guid}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] Guid userId)
        {
            // use repository to get user object 
            // map it to userDTO 
            var user = await userRepository.GetUserAsync(userId);
            if (user == null) return NotFound();
            // return to user 

            var userDto = mapper.Map<UserDTO>(user);

            return Ok(userDto);
        }
        

        [HttpPost]
        [Route("CreateUser")]
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
        [Route("user/{userId:guid}")]
        [Authorize(Policy = "user")] // we only want the same user logged in to be able to update it
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UpdateUserRequest updateUserRequest)
        {
            // TODO: Authenticated (this needs to be the correct user logged in to do this!
            var user = mapper.Map<User>(updateUserRequest);
            user = await userRepository.UpdateUserAsync(userId, user);
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
                return Ok(new { CreateTokenAsync = tokenHandler.CreateTokenAsync(user), user.Id});
            }

            return BadRequest("Email or password is incorrect.");
        }
    }
}
