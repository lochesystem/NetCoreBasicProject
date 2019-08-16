using System;
using BasicProject.Domain.Interface.Base;
using BasicProject.Domain.Model;

namespace BasicProject.Domain.Interface
{
    public interface IRepositoryAddress : IRepositoryBase<Address>
    {
        Address FindAddressById(Guid id);

        Address FindAddressByUserId(Guid id);
    }
}
