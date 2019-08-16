using AutoMapper;
using BasicProject.Domain.Interface;
using BasicProject.Infra.Context;
using BasicProject.Infra.Entity;
using BasicProject.Infra.Mapping;
using BasicProject.Infra.Repository.Base;

namespace BasicProject.Infra.Repository
{
    public class RepositoryLog : RepositoryBase<Domain.Model.Log, Log>, IRepositoryLog
    {
        private IMapper _mapper;

        public RepositoryLog(BasicProjectContext context) : base(context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(MapperProfile));
            });
            _mapper = config.CreateMapper();
        }
    }
}
