using System;
using BasicProject.Domain.Model.Base;

namespace BasicProject.Domain.Model
{
    public class Address : CreateBase
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
