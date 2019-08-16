using System;
using System.Collections.Generic;
using BasicProject.Service.DTOs.User;

namespace BasicProject.Service.Interface
{
    public interface IServiceUser
    {

        /// <summary>
        ///  Add an user
        /// </summary>
        Guid AddUser(UserModelCreateDto newUser);

        ///<summary>
        /// Update an user
        ///</summary>
        Guid UpdateUser(UserModelUpdateDto alteredUser);

        ///<summary>
        /// Delete an user by id
        ///</summary>
        void DeleteUser(Guid id);

        ///<summary>
        /// Get all users by paging
        ///</summary>
        List<UserModelGeneralDto> ListUser();

        ///<summary>
        /// Get all users by paging
        ///</summary>
        List<UserModelGeneralDto> ListUserPaged(int skip, int take, out int totalLines);

        ///<summary>
        /// Search an user by id
        ///</summary>
        UserModelGeneralDto GetUserById(Guid id);

        ///<summary>
        /// Search an user by email
        ///</summary>
        UserModelGeneralDto GetUserByEmail(string email);


    }
}
