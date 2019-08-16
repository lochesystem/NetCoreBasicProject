using System;
using System.Linq;
using AutoMapper;
using BasicProject.Domain.Interface;
using BasicProject.Infra.Context;
using BasicProject.Infra.Entity;
using BasicProject.Infra.Mapping;
using BasicProject.Infra.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace BasicProject.Infra.Repository
{
    public class RepositoryAddress : RepositoryBase<Domain.Model.Address, Address>, IRepositoryAddress
    {
        #region Atributos

        private IMapper _mapper;

        #endregion

        #region Construtor

        public RepositoryAddress(BasicProjectContext context) : base(context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(MapperProfile));
            });
            _mapper = config.CreateMapper();
        }

        #endregion

        #region FindAddressById

        //Busca Endereço pelo ID
        public Domain.Model.Address FindAddressById(Guid id)
        {
            return _mapper.Map<Domain.Model.Address>(_db.Set<Address>().AsNoTracking().FirstOrDefault(x => x.Id == id));
        }

        #endregion

        #region FindAddressByUserId

        //Busca usuário pelo ID
        public Domain.Model.Address FindAddressByUserId(Guid id)
        {
            return _mapper.Map<Domain.Model.Address>(_db.Set<Address>().AsNoTracking().FirstOrDefault(x => x.UserId == id));
        }

        #endregion
    }
}
