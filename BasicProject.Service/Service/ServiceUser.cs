using AutoMapper;
using BasicProject.Domain.Model;
using BasicProject.Infra.Interfaces;
using BasicProject.Infra.Mapping;
using BasicProject.Service.DTOs.User;
using BasicProject.Service.DTOValidator.User;
using BasicProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using BasicProject.Service.Helpers;

namespace BasicProject.Service.Service
{
    public class ServiceUser : IServiceUser
    {
        #region Atributos

        private readonly IMapper _mapper;
        private readonly IRepositoryUnitOfWork _unitOfWork;

        #endregion

        #region Construtor

        public ServiceUser(IRepositoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(MapperProfile));
            });
            _mapper = config.CreateMapper();
        }

        #endregion

        public Guid AddUser(UserModelCreateDto newUser)
        {
            if (newUser == null)
            {
                throw new Exception("Usuario invalido");
            }

            var validate = new UserModelCreateDtoValidator().Validate(newUser);

            if (!validate.IsValid)
            {
                throw new Exception("Usuario invalido");
            }

            User model = new User
            {
                Id = Guid.NewGuid(),
                Name = newUser.Name,
                Email = newUser.Email,
                Password = EncryptHelper.EncryptMD5(newUser.Password),
                Gender = newUser.Gender,
                UpdateDate = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                Active = true,

            };

            _unitOfWork.Users.Add(model);
            _unitOfWork.Commit();

            return model.Id;
        }

        public Guid UpdateUser(UserModelUpdateDto alteredUser)
        {
            if (alteredUser == null)
            {
                throw new Exception("Usuario invalido");
            }

            var validate = new UserModelUpdateDtoValidator().Validate(alteredUser);

            if (!validate.IsValid)
            {
                throw new Exception("Usuario invalido");
            }

            var oldUser = _unitOfWork.Users.Query(x => x.Id == alteredUser.Id);

            if (oldUser == null)
            {
                throw new Exception("Usuario invalido");
            }

            oldUser = UpdateAlteredUser(oldUser, alteredUser);

            _unitOfWork.Users.Update(oldUser);
            _unitOfWork.Commit();
            return alteredUser.Id;

        }

        public void DeleteUser(Guid id)
        {
            var user = _unitOfWork.Users.Query(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("Usuario invalido");
            }

            if (user.Address != null)
            {
                _unitOfWork.Addresses.Delete(user.Address);
            }
            _unitOfWork.Users.Delete(user);
            _unitOfWork.Commit();
        }

        public UserModelGeneralDto GetUserById(Guid id)
        {
            var user = _unitOfWork.Users.Query(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("Usuario invalido");
            }

            return _mapper.Map<UserModelGeneralDto>(user);
        }

        public UserModelGeneralDto GetUserByEmail(string email)
        {
            var user = _unitOfWork.Users.Query(x => x.Email == email);

            if (user == null)
            {
                throw new Exception("Usuario invalido");
            }

            return _mapper.Map<UserModelGeneralDto>(user);
        }

        public List<UserModelGeneralDto> ListUser()
        {
            var users = _unitOfWork.Users.List();

            return _mapper.Map<List<UserModelGeneralDto>>(users);
        }

        public List<UserModelGeneralDto> ListUserPaged(int skip, int take, out int totalLines)
        {
            var users = _unitOfWork.Users.List().Skip(skip).Take(take);

            totalLines = users.ToList().Count;

            return _mapper.Map<List<UserModelGeneralDto>>(users);
        }

        private User UpdateAlteredUser(User old,UserModelUpdateDto alteredDto)
        {
            old.Name = !string.IsNullOrEmpty(alteredDto.Name) 
                ? alteredDto.Name 
                : old.Name;

            old.Email = !string.IsNullOrEmpty(alteredDto.Email) 
                ? alteredDto.Email 
                : old.Email;

            if (!string.IsNullOrEmpty(alteredDto.Password) && alteredDto.Password != old.Password)
            {
                var alteredDtoPasswordEncrypted = EncryptHelper.EncryptMD5(alteredDto.Password);

                if (alteredDtoPasswordEncrypted != old.Password)
                {
                    old.Password = alteredDtoPasswordEncrypted;
                }
            }


            old.Gender = alteredDto.Gender;
            old.UpdateDate = DateTimeOffset.Now;

            return old;
        }
    }
}
