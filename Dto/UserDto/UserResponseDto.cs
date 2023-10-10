using TP1.Models;
using static TP1.Models.User;

namespace TP1.Dto.UserDto
{
    public class UserResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public StatusEnum Status { get; set; }

        public UserResponseDto(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Status = user.Status;
        }
    }
}
