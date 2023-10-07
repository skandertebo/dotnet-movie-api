using TP1.Models;

namespace TP1.Responses
{
    public class UserLoginResponse : User
    {
        public string AccessToken { get; set; }
      
        public UserLoginResponse(string accessToken) : base()
        {
            AccessToken = accessToken;
        }
    }
}
