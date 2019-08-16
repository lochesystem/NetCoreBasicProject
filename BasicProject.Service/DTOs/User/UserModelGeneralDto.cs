using BasicProject.Domain.Enum;
using BasicProject.Service.DTOs.Address;
using BasicProject.Service.DTOs.Base;

namespace BasicProject.Service.DTOs.User
{
    public class UserModelGeneralDto : BaseCreateDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public GenderEnum Gender { get; set; }

        public string IdSocial { get; set; }

        public AddressModelGeneralDto Address { get; set; }
    }
}
