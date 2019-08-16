using BasicProject.API.Controllers.Base;
using BasicProject.Infra.OperationalMessage;
using BasicProject.Service.DTOs.User;
using BasicProject.Service.Interface;
using BasicProject.Service.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using LogHelper = BasicProject.Service.Helpers.LogHelper;

namespace BasicProject.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseApiController
    {
        #region Atributos

        public IServiceUser _serviceUser;
        public IServiceAddress _serviceAddress;
        public IServiceLog _serviceLog;

        #endregion

        #region Construtor

        /// <summary>
        /// UserController constructor
        /// </summary>
        public UserController(IServiceUser serviceUser, IServiceAddress serviceAddress, IServiceLog serviceLog)
        {
            _serviceUser = serviceUser;
            _serviceAddress = serviceAddress;
            _serviceLog = serviceLog;
        }

        #endregion

        #region Get User By Id 

        /// <summary>
        /// Busca Usário pelo ID
        /// </summary>
        /// <param name="usuarioId">ID do Usuário</param>
        /// <returns>Dados do Usuário</returns>
        [HttpGet]
        [Authorize("Bearer")]
        [Route("GetById/{usuarioId}")]
        public OperationResponse<UserModelGeneralDto> GetById(Guid usuarioId)
        {
            OperationResponse<UserModelGeneralDto> response = new OperationResponse<UserModelGeneralDto>();

            try
            {
                var user = _serviceUser.GetUserById(usuarioId);

                var address = _serviceAddress.GetAddressByUserId(usuarioId);

                if (address != null)
                {
                    user.Address = address;
                }

                response.Data = user;
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Success_Search, Type = OperationMessageTypes.Success });

            }
            catch (Exception ex)
            {
                _serviceLog.Add(LogHelper.GenerateLog(ControllerContext, "GetById", UserResource.User_Error_Search, CurrentUser.Email, ex));

                response.Messages.Add(new OperationMessage { Description = UserResource.User_Error_Search + " : " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }

        #endregion

        #region Get All Users

        [HttpGet]
        [Authorize("Bearer")]
        [Route("GelAll")]
        public OperationResponse<List<UserModelGeneralDto>> GetAll()
        {
            OperationResponse<List<UserModelGeneralDto>> response = new OperationResponse<List<UserModelGeneralDto>>();

            try
            {
                var users = _serviceUser.ListUser();

                response.Data = users;
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Success_List, Type = OperationMessageTypes.Success });
            }
            catch (Exception ex)
            {
                _serviceLog.Add(LogHelper.GenerateLog(ControllerContext, "GetAll", UserResource.User_Error_List, CurrentUser.Email, ex));
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Error_List + " : " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }
        #endregion

        #region Get All Users Paged

        [HttpGet]
        [Authorize("Bearer")]
        [Route("GelAll/{take}/{page}")]
        public OperationResponse<List<UserModelGeneralDto>> GetAllPaged(int take, int page)
        {
            OperationResponse<List<UserModelGeneralDto>> response = new OperationResponse<List<UserModelGeneralDto>>();

            try
            {
                int total = 0;
                var users = _serviceUser.ListUserPaged(page, take, out total);

                response.Data = users;
                response.Count = total;
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Success_List, Type = OperationMessageTypes.Success });
            }
            catch (Exception ex)
            {
                _serviceLog.Add(LogHelper.GenerateLog(ControllerContext, "GetAllPaged", UserResource.User_Error_List, CurrentUser.Email, ex));
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Error_List + " : " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }

        #endregion  

        #region Insert User

        [HttpPost]
        [Authorize("Bearer")]
        [Route("AddUser")]
        public OperationResponse<Guid> AddUser([FromBody] UserModelCreateDto user)
        {
            OperationResponse<Guid> response = new OperationResponse<Guid>();

            try
            {
                var result = _serviceUser.AddUser(user);
                response.Data = result;
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Success_Create, Type = OperationMessageTypes.Success });

            }
            catch (Exception ex)
            {
                _serviceLog.Add(LogHelper.GenerateLog(ControllerContext, "AddUser", UserResource.User_Error_Create, CurrentUser.Email, ex));
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Error_Create + " : " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }

        #endregion

        #region Update User

        [HttpPut]
        [Authorize("Bearer")]
        [Route("UpdateUser")]
        public OperationResponse<Guid> UpdateUser([FromBody] UserModelUpdateDto user)
        {
            OperationResponse<Guid> response = new OperationResponse<Guid>();

            try
            {
                var result = _serviceUser.UpdateUser(user);
                _serviceAddress.InsertOrUpdateAddressWithUserDto(user);
                
                response.Data = result;
                response.Messages.Add(new OperationMessage {Description = UserResource.User_Success_Update, Type = OperationMessageTypes.Success});
            }
            catch (Exception ex)
            {
                _serviceLog.Add(LogHelper.GenerateLog(ControllerContext, "UpdateUser", UserResource.User_Error_Update, CurrentUser.Email, ex));
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Error_Update + " : " + ex.Message, Type = OperationMessageTypes.Error });
            }
            return response;
        }

        #endregion

        #region Delete User

        [HttpDelete]
        [Authorize("Bearer")]
        [Route("DeleteUser/{usuarioId}")]
        public OperationResponse<bool> DeleteUser(Guid usuarioId)
        {
            OperationResponse<bool> response = new OperationResponse<bool>();

            try
            {
                _serviceUser.DeleteUser(usuarioId);
                response.Data = true;
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Success_Delete, Type = OperationMessageTypes.Success });

            }
            catch (Exception ex)
            {
                _serviceLog.Add(LogHelper.GenerateLog(ControllerContext, "DeleteUser", UserResource.User_Error_Delete, CurrentUser.Email, ex));
                response.Messages.Add(new OperationMessage { Description = UserResource.User_Error_Delete + " : " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }

        #endregion
    }
}
