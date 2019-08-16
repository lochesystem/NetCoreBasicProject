using System;
using BasicProject.Infra.Entity.Base;

namespace BasicProject.Infra.Entity
{
    public class Address : EntityCreateBase
    {
        public string Street { get; set; }

        public string Number { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Complement { get; set; }

        //chave estrangeira
        public Guid UserId { get; set; }

        //propriedade de navegação
        public User User { get; set; }
    }
}
