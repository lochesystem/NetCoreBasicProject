using BasicProject.API.Controllers.Base;
using BasicProject.Infra.OperationalMessage;
using BasicProject.Service.DTOs.Address;
using BasicProject.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BasicProject.API.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : BaseApiController
    {
        #region Atributos

        public IServiceAddress _serviceAddress;

        #endregion

        #region Construtor

        /// <summary>
        /// UserController constructor
        /// </summary>
        public AddressController(IServiceAddress serviceAddress)
        {
            _serviceAddress = serviceAddress;
        }

        #endregion

        #region Get Address By User Id

        /// <summary>
        /// Busca Endereço pelo ID do usuário
        /// </summary>
        /// <param name="usuarioId">ID do Usuário</param>
        /// <returns>Dados do Usuário</returns>
        [HttpGet]
        [Route("GetAddressByUserId/{usuarioId}")]
        public OperationResponse<AddressModelGeneralDto> GetAddressByUserId(Guid usuarioId)
        {
            OperationResponse<AddressModelGeneralDto> response = new OperationResponse<AddressModelGeneralDto>();

            try
            {
                var address = _serviceAddress.GetAddressByUserId(usuarioId);

                if (address == null)
                {
                    response.Messages.Add(new OperationMessage { Description = "Falha: Usuario ou Endereço invalido", Type = OperationMessageTypes.Error });
                }
                else
                { 
                    response.Data = address;
                    response.Messages.Add(new OperationMessage { Description = "Sucesso", Type = OperationMessageTypes.Success });
                }

            }
            catch (Exception ex)
            {
                response.Messages.Add(new OperationMessage { Description = "Falha: " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }

        #endregion

        #region Insert Address

        [HttpPost]
        [Route("AddAddress")]
        public OperationResponse<Guid> AddAddress([FromBody] AddressModelCreateDto address)
        {
            OperationResponse<Guid> response = new OperationResponse<Guid>();

            try
            {
                var result = _serviceAddress.AddAddress(address);
                response.Data = result;
                response.Messages.Add(new OperationMessage { Description = "Sucesso", Type = OperationMessageTypes.Success });

            }
            catch (Exception ex)
            {
                response.Messages.Add(new OperationMessage { Description = "Falha: " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }

        #endregion

        #region Update Address

        [HttpPut]
        [Route("UpdateAddress")]
        public OperationResponse<Guid> UpdateAddress([FromBody] AddressModelUpdateDto address)
        {
            OperationResponse<Guid> response = new OperationResponse<Guid>();

            try
            {
                var result = _serviceAddress.UpdateAddress(address);
                response.Data = result;
                response.Messages.Add(new OperationMessage { Description = "Sucesso", Type = OperationMessageTypes.Success });

            }
            catch (Exception ex)
            {
                response.Messages.Add(new OperationMessage { Description = "Falha: " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }

        #endregion

        #region Delete Address

        [HttpDelete]
        [Route("DeleteAddressByUserId/{usuarioId}")]
        public OperationResponse<bool> DeleteAddressByUserId(Guid usuarioId)
        {
            OperationResponse<bool> response = new OperationResponse<bool>();

            try
            {
                _serviceAddress.DeleteAddress(usuarioId);
                response.Data = true;
                response.Messages.Add(new OperationMessage { Description = "Sucesso", Type = OperationMessageTypes.Success });

            }
            catch (Exception ex)
            {
                response.Messages.Add(new OperationMessage { Description = "Falha: " + ex.Message, Type = OperationMessageTypes.Error });
            }

            return response;
        }

        #endregion
    }
}
