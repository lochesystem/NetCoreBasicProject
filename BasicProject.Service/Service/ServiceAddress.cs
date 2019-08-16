using AutoMapper;
using BasicProject.Infra.Interfaces;
using BasicProject.Infra.Mapping;
using BasicProject.Service.DTOs.Address;
using BasicProject.Service.DTOValidator.Address;
using BasicProject.Service.Interface;
using System;
using BasicProject.Domain.Model;
using BasicProject.Service.DTOs.User;

namespace BasicProject.Service.Service
{
    public class ServiceAddress : IServiceAddress
    {
        #region Atributos

        private readonly IMapper _mapper;
        private readonly IRepositoryUnitOfWork _unitOfWork;

        #endregion

        #region Construtor

        public ServiceAddress(IRepositoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => { cfg.AddProfile(typeof(MapperProfile)); });
            _mapper = config.CreateMapper();
        }

        #endregion


        public Guid AddAddress(AddressModelCreateDto newAddress)
        {
            if (newAddress == null)
            {
                throw new Exception("Endereço invalido");
            }

            var validate = new AddressModelCreateDtoValidator().Validate(newAddress);

            if (!validate.IsValid)
            {
                throw new Exception("Endereço invalido");
            }

            var userExists = _unitOfWork.Users.Query(x => x.Id == newAddress.UserId);

            if (userExists == null)
            {
                throw new Exception("Usuario invalido");
            }

            var addressExistsForUser = _unitOfWork.Addresses.FindAddressByUserId(newAddress.UserId);

            if (addressExistsForUser != null)
            {
                throw new Exception("Usuario já contém um endereço");
            }


            Address model = new Address
            {
                Id = Guid.NewGuid(),
                City = newAddress.City,
                UserId = newAddress.UserId,
                Street = newAddress.Street,
                Number = newAddress.Number,
                Complement = newAddress.Complement,
                Neighborhood = newAddress.Neighborhood,

                UpdateDate = DateTimeOffset.Now,
                CreationDate = DateTimeOffset.Now,
                Active = true,

            };

            _unitOfWork.Addresses.Add(model);
            _unitOfWork.Commit();

            return model.Id;
        }

        public Guid UpdateAddress(AddressModelUpdateDto alteredAddress)
        {
            if (alteredAddress == null)
            {
                throw new Exception("Endereço invalido");
            }

            var validate = new AddressModelUpdateDtoValidator().Validate(alteredAddress);

            if (!validate.IsValid)
            {
                throw new Exception("Endereço invalido");
            }

            var oldAddress = _unitOfWork.Addresses.FindAddressById(alteredAddress.Id);

            if (oldAddress == null)
            {
                throw new Exception("Usuario invalido");
            }

            oldAddress = UpdateAlteredAddress(oldAddress, alteredAddress);

            _unitOfWork.Addresses.Update(oldAddress);
            _unitOfWork.Commit();
            return alteredAddress.Id;
        }

        public void DeleteAddress(Guid id)
        {
            var address = _unitOfWork.Addresses.FindAddressByUserId(id);

            if (address == null)
            {
                throw new Exception("Endereço invalido");
            }

            _unitOfWork.Addresses.Delete(address);
            _unitOfWork.Commit();
        }

        public AddressModelGeneralDto GetAddressByUserId(Guid id)
        {
            var address = _unitOfWork.Addresses.FindAddressByUserId(id);

            if (address == null)
            {
                return null;
            }

            return _mapper.Map<AddressModelGeneralDto>(address);
        }

        public Guid InsertOrUpdateAddressWithUserDto(UserModelUpdateDto user)
        {

            var addressExistsForUser = _unitOfWork.Addresses.FindAddressByUserId(user.Id);

            if (addressExistsForUser != null)
            {
                var validedForUpdate = ValidateUpdateAddressInsideDto(user,addressExistsForUser);

                if(VerifyAnyFieldFilled(validedForUpdate)) return UpdateAddress(validedForUpdate);

                return Guid.Empty;
            }

            var validedForInsert = ValidateInsertAddressInsideDto(user);

            if (VerifyAnyFieldFilled(validedForInsert)) return AddAddress(validedForInsert);

            return Guid.Empty;

        }

        private AddressModelUpdateDto ValidateUpdateAddressInsideDto(UserModelUpdateDto user, Address old)
        {
            AddressModelUpdateDto addressDto = new AddressModelUpdateDto();

            addressDto.Street = !string.IsNullOrEmpty(user.Street) ? user.Street : string.Empty;
            addressDto.Number = !string.IsNullOrEmpty(user.Number) ? user.Number : string.Empty;
            addressDto.Neighborhood = !string.IsNullOrEmpty(user.Neighborhood) ? user.Neighborhood : string.Empty;
            addressDto.Complement = !string.IsNullOrEmpty(user.Complement) ? user.Complement : string.Empty;
            addressDto.City = !string.IsNullOrEmpty(user.City) ? user.City : string.Empty;
            addressDto.Id = old.Id;
            addressDto.UserId = old.UserId;

            return addressDto;
        }

        private AddressModelCreateDto ValidateInsertAddressInsideDto(UserModelUpdateDto user)
        {
            AddressModelCreateDto addressDto = new AddressModelCreateDto();

            addressDto.Street = !string.IsNullOrEmpty(user.Street) ? user.Street : string.Empty;
            addressDto.Number = !string.IsNullOrEmpty(user.Number) ? user.Number : string.Empty;
            addressDto.Neighborhood = !string.IsNullOrEmpty(user.Neighborhood) ? user.Neighborhood : string.Empty;
            addressDto.Complement = !string.IsNullOrEmpty(user.Complement) ? user.Complement : string.Empty;
            addressDto.City = !string.IsNullOrEmpty(user.City) ? user.City : string.Empty;
            addressDto.UserId = user.Id;

            return addressDto;
        }

        private bool VerifyAnyFieldFilled(object dto)
        {
            if (dto.GetType() == typeof(AddressModelCreateDto))
            {
                AddressModelCreateDto instance = (AddressModelCreateDto) dto;

                if (!string.IsNullOrEmpty(instance.Street)) return true;
                else if (!string.IsNullOrEmpty(instance.Number)) return true;
                else if (!string.IsNullOrEmpty(instance.Neighborhood)) return true;
                else if (!string.IsNullOrEmpty(instance.Complement)) return true;
                else if (!string.IsNullOrEmpty(instance.City)) return true;
                else return false;
            }

            if (dto.GetType() == typeof(AddressModelUpdateDto))
            {
                AddressModelUpdateDto instance = (AddressModelUpdateDto)dto;

                if (!string.IsNullOrEmpty(instance.Street)) return true;
                else if (!string.IsNullOrEmpty(instance.Number)) return true;
                else if (!string.IsNullOrEmpty(instance.Neighborhood)) return true;
                else if (!string.IsNullOrEmpty(instance.Complement)) return true;
                else if (!string.IsNullOrEmpty(instance.City)) return true;
                else return false;
            }

            return false;
            
        }

        private Address UpdateAlteredAddress(Address old, AddressModelUpdateDto alteredDto)
        {

            old.City = alteredDto.City;
            old.UserId = alteredDto.UserId;
            old.Street = alteredDto.Street;
            old.Number = alteredDto.Number;
            old.Complement = alteredDto.Complement;
            old.Neighborhood = alteredDto.Neighborhood;
            old.UpdateDate = DateTimeOffset.Now;

            return old;
        }
    }
}
