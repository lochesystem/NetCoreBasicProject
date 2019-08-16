using System;
using AutoMapper;
using BasicProject.Infra.Interfaces;
using BasicProject.Infra.Mapping;
using BasicProject.Service.DTOs.Login;
using BasicProject.Service.Helpers;
using BasicProject.Service.Interface;

namespace BasicProject.Service.Service
{
    public class ServiceLogin : IServiceLogin
    {
        #region Atributos

        private readonly IMapper _mapper;
        private readonly IRepositoryUnitOfWork _unitOfWork;

        #endregion

        #region Construtor

        public ServiceLogin(IRepositoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(MapperProfile));
            });
            _mapper = config.CreateMapper();
        }

        #endregion

        public bool Auth(LoginRequestDto login)
        {
            if (login == null)
            {
                throw new Exception("Login invalido");
            }

            var encryptedPassword = EncryptHelper.EncryptMD5(login.Password);

            var user = _unitOfWork.Users.Query(x => x.Email == login.Email && x.Password == encryptedPassword);

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
