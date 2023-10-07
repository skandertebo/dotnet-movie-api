using TP1.Context;
using TP1.Models;
using System.IdentityModel.Tokens;
using TP1.Responses;

namespace TP1.Services
{
    public interface IUserService
    {
        public string GenerateUserJwtToken(string email, int id);
        public User GetUserByJwt(string jwtToken);

        public User GetJwtByEmail(string email);

        public UserLoginReponse Login(string email, string password);

        public UserSignupResponse Signup(User CreateUserDto); 
    }
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(MovieDbContext context) : base(context){}

        public string GenerateUserJwtToken(string email, int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByJwt(string jwtToken)
        {
            throw new NotImplementedException();
        }

        public User GetJwtByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public UserLoginResponse Login(string email, string password)
        {
            throw new NotImplementedException(); 
        }

        public UserSignupResponse Signup(User CreateUserDto)
        {
            throw new NotImplementedException();
        }

    }

}
