using BasicProject.Domain.Enum;
using BasicProject.Service.DTOs.Base;

namespace BasicProject.Service.DTOs.User
{
    public class UserModelUpdateDto : BaseDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public GenderEnum Gender { get; set; }

        public string IdSocial { get; set; }

        public bool Active { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Complement { get; set; }
    }
}
