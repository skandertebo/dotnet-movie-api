namespace TP1.Responses
{
    public class UserSignupResponse
    {
        public string AccessToken { get; set; }

        public UserSignupResponse(string accessToken) : base()
        {
            AccessToken = accessToken;
        }
    }
}
