using System;
using AutoMapper;
using BasicProject.Infra.Interfaces;
using BasicProject.Infra.Mapping;
using BasicProject.Service.DTOs.Log;
using BasicProject.Service.Interface;

namespace BasicProject.Service.Service
{
    public class ServiceLog : IServiceLog
    {

        private readonly IMapper _mapper;
        private readonly IRepositoryUnitOfWork _unitOfWork;

        public ServiceLog(IRepositoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(MapperProfile));
            });
            _mapper = config.CreateMapper();
        }

        public void Add(LogCreateDTO log)
        {

            Domain.Model.Log model = new Domain.Model.Log()
            {
                Id = Guid.NewGuid(),
                ClassName = log.ClassName,
                LogDate = DateTimeOffset.Now,
                Message = log.Message,
                MethodName = log.MethodName,
                UserId = log.UserId,
                UserEmail = log.UserEmail,
                ControllerName = log.ControllerName,
                Exception = log.Exception,
                FriendlyMessage = log.FriendlyMessage,
                StackTrace = log.StackTrace
            };

            _unitOfWork.Logs.Add(model);
            _unitOfWork.Commit();
        }
    }
}
