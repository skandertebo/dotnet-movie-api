using Microsoft.AspNetCore.Mvc;
using TP1.Dto.UserDto;
using TP1.Middleware;
using TP1.Models;
using TP1.ResponseExceptions;
using TP1.Responses;
using TP1.Services;

namespace TP1.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public UserLoginResponse Login([FromBody] LoginDto userLoginDto)
        {
            return _userService.Login(userLoginDto.Email, userLoginDto.Password);
        }

        [HttpPost("signup")]
        public UserSignupResponse Signup([FromBody] User createUserDto)
        {
           
            return _userService.Signup(createUserDto);
           
        }

        [HttpGet("whoami")]
        [ServiceFilter(typeof(AuthMiddleware))]
        public UserResponseDto GetUserFromJWT()
        {
            var user = (User)HttpContext.Items["user"];
            if (user==null) { throw new BadRequestException("Unexpected?"); }
            return new UserResponseDto(user);
        }
    }
}
