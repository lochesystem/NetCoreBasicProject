using AutoMapper;
using BasicProject.Domain.Interface;
using BasicProject.Infra.Context;
using BasicProject.Infra.Entity;
using BasicProject.Infra.Mapping;
using BasicProject.Infra.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace BasicProject.Infra.Repository
{
    public class RepositoryUser : RepositoryBase<Domain.Model.User, User>, IRepositoryUser
    {

        #region Atributos

        private IMapper _mapper;

        #endregion

        #region Construtor

        public RepositoryUser(BasicProjectContext context) : base(context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(MapperProfile));
            });
            _mapper = config.CreateMapper();
        }

        #endregion

        #region FindUserById

        //Busca usuário pelo ID
        public Domain.Model.User FindUserById(Guid id)
        {
            return _mapper.Map<Domain.Model.User>(_db.Set<User>().ProjectTo<User>(_mapper.ConfigurationProvider).AsNoTracking().FirstOrDefault(x => x.Id == id));
        }

        #endregion
    }
}
