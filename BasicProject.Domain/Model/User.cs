using BasicProject.Domain.Enum;
using BasicProject.Domain.Model.Base;

namespace BasicProject.Domain.Model
{
    public class User : CreateBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public GenderEnum Gender { get; set; }

        public string IdSocial { get; set; }

        public Address Address { get; set; }
    }
}
