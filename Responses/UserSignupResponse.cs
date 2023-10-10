using TP1.Dto.UserDto;
using TP1.Models;

namespace TP1.Responses
{
    public class UserSignupResponse
    {
        public string AccessToken { get; set; }
        public UserResponseDto user { get; set; }
        public UserSignupResponse(string accessToken, User _user) : base()
        {
            AccessToken = accessToken;
            user = new UserResponseDto(_user);
        }
    }
}
