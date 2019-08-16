using BasicProject.Service.DTOs.User;
using System;
using BasicProject.Service.DTOs.Address;

namespace BasicProject.Service.Interface
{
    public interface IServiceAddress
    {

        /// <summary>
        ///  Add an address for user
        /// </summary>
        Guid AddAddress(AddressModelCreateDto newAddress);

        ///<summary>
        /// Update an address
        ///</summary>
        Guid UpdateAddress(AddressModelUpdateDto alteredAddress);

        ///<summary>
        /// Delete an address of user by userid
        ///</summary>
        void DeleteAddress(Guid id);

        ///<summary>
        /// Search an address by userid
        ///</summary>
        AddressModelGeneralDto GetAddressByUserId(Guid id);

        Guid InsertOrUpdateAddressWithUserDto(UserModelUpdateDto user);
    }
}
