using BasicProject.Domain.Interface.Base;
using BasicProject.Domain.Model;
using System;

namespace BasicProject.Domain.Interface
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        User FindUserById(Guid id);
    }
}
