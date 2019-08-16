using System;

namespace BasicProject.Service.DTOs.Address
{
    public class AddressModelCreateDto
    {
        public string Street { get; set; }

        public string Number { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Complement { get; set; }

        public Guid UserId { get; set; }
    }
}
