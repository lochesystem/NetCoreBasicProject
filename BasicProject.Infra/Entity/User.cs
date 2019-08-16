using BasicProject.Domain.Enum;
using BasicProject.Infra.Entity.Base;

namespace BasicProject.Infra.Entity
{
    public class User : EntityCreateBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }    

        public GenderEnum Gender { get; set; }

        public string IdSocial { get; set; }

        public Address Address { get; set; }
    }
}
