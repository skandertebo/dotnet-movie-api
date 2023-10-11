using TP1.Context;
using TP1.Models;
using System.IdentityModel.Tokens;
using TP1.Responses;
using System.Security.Claims;
using TP1.ResponseExceptions;
using TP1.Dto.UserDto;

namespace TP1.Services
{
    public interface IUserService
    {
        public string GenerateUserJwtToken(string email, int id);
        public User? GetUserByJwt(string jwtToken);

        public User? GetUserByEmail(string email);

        public UserLoginResponse Login(string email, string password);

        public UserSignupResponse Signup(User CreateUserDto); 
    }
    public class UserService : GenericService<User>, IUserService
    {

        private readonly IJwtService _jwtService;
        private readonly IHashingService _hashingService;

        public UserService(MovieDbContext context, IJwtService jwtService, IHashingService hashingService) : base(context){
            _jwtService = jwtService;
            _hashingService = hashingService;
        }


        public string GenerateUserJwtToken(string email, int id)
        {
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            });

            var token = _jwtService.GenerateToken(claims);
            return token;
        }

        public User? GetUserByJwt(string jwtToken)
        {
            var claims = _jwtService.VerifyToken(jwtToken);
            if (claims == null)
                return null;
            var email = claims.FindFirst(ClaimTypes.Name)?.Value;
            var parseIdResult = int.TryParse(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id);
            if(email == null || !parseIdResult)
            {
                return null;
            }
            var user = _repository.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public User? GetUserByEmail(string email)
        {
            Console.WriteLine(email);
            return _repository.FirstOrDefault(u => u.Email == email);
        }

        public UserLoginResponse Login(string email, string password)
        {
            var user = GetUserByEmail(email);
            if (user == null)
                throw new BadRequestException("No user found matching this email address");
            var isEligible = _hashingService.VerifyHash(password, user.Password);
            if (!isEligible)
                throw new UnauthorizedException("Password is not correct");
            var token = GenerateUserJwtToken(user.Email, user.Id);
            return new UserLoginResponse(token, user);
        }

        public UserSignupResponse Signup(User CreateUserDto)
        {
            if(CreateUserDto == null || CreateUserDto.FirstName == null || CreateUserDto.LastName == null || CreateUserDto.Email == null || CreateUserDto.Password == null)
            {
                throw new BadRequestException("Some of fields are missing");
            }
            var existingUser = _repository.FirstOrDefault(u => u.Email == CreateUserDto.Email);
            if(existingUser != null)
            {
                throw new BadRequestException("Email already exists");
            }
            var hashedPassword = _hashingService.ComputeHash(CreateUserDto.Password);
            CreateUserDto.Password = hashedPassword;
            _repository.Add(CreateUserDto);
            _context.SaveChanges();
            var createdUser = _repository.First(u => u.Email == CreateUserDto.Email);
            var token = GenerateUserJwtToken(createdUser.Email, createdUser.Id);
            return new UserSignupResponse(token, createdUser);
        }

        public UserResponseDto GetUserResponse(User user)
        {
            return new UserResponseDto(user);
        }

    }

}
