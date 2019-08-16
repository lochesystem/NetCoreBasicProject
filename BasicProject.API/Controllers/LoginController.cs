using BasicProject.API.Security;
using BasicProject.Infra.OperationalMessage;
using BasicProject.Service.DTOs.Login;
using BasicProject.Service.Helpers;
using BasicProject.Service.Interface;
using BasicProject.Service.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BasicProject.API.Controllers
{

    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        #region Atributos

        public IServiceUser _serviceUser;
        public IServiceLogin _serviceLogin;
        public IConfiguration _configuration;
        public IServiceLog _serviceLog;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor Controller de Login
        /// </summary>
        public LoginController(IServiceUser serviceUser, IServiceLogin serviceLogin, IConfiguration configuration, IServiceLog serviceLog)
        {
            _serviceUser = serviceUser;
            _serviceLogin = serviceLogin;
            _configuration = configuration;
            _serviceLog = serviceLog;
        }

        #endregion

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">Credenciais de login</param>
        ///  <param name="signingConfigurations">Config signing</param>
        ///  <param name="tokenConfigurations">Config token</param>
        /// <returns>Token e credenciais do usuário</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("auth")]
        public async Task<OperationResponse<object>> Post(
            [FromBody] LoginRequestDto login,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            OperationResponse<object> response = new OperationResponse<object>();

            try
            {
                var userValid = _serviceLogin.Auth(login);

                if (userValid)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                       new GenericIdentity(login.Email, "Login"),
                       new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, login.Email),
                        new Claim(JwtRegisteredClaimNames.UniqueName, EncryptHelper.EncryptMD5(login.Password))
                       }
                   );

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });
                    var token = handler.WriteToken(securityToken);

                    var user = new
                    {
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = token,
                        message = "OK"
                    };

                    response.Data = user;
                    response.Messages.Add(new OperationMessage { Description = LoginResource.Login_Sucessful, Type = OperationMessageTypes.Success });
                    return response;
                }

                response.Messages.Add(new OperationMessage() { Description = LoginResource.Login_Error, Type = OperationMessageTypes.Error });
                return response;

            }
            catch (Exception ex)
            {
                _serviceLog.Add(LogHelper.GenerateLog(ControllerContext, "Auth", LoginResource.Login_Error, "", ex));
                response.Messages.Add(new OperationMessage() { Description = LoginResource.Login_Error, Type = OperationMessageTypes.Error });
                return response;
            }
        }

    }
}
