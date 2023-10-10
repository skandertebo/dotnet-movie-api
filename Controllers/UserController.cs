using Microsoft.AspNetCore.Mvc;
using TP1.Dto.UserDto;
using TP1.Models;
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
    }
}
