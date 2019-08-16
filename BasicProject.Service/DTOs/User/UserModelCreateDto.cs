using BasicProject.Domain.Enum;

namespace BasicProject.Service.DTOs.User
{
    public class UserModelCreateDto 
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public GenderEnum Gender { get; set; }
    }
}
