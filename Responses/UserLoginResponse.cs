using TP1.Dto.UserDto;
using TP1.Models;

namespace TP1.Responses
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public UserResponseDto user { get; set; }

        public UserLoginResponse(string accessToken, User _user) : base()
        {
            AccessToken = accessToken;
            user = new UserResponseDto(_user);
        }
    }
}
