using BasicProject.Service.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BasicProject.API.Controllers.Base
{
    public class BaseApiController : Controller
    {

        public UserModelGeneralDto CurrentUser
        {
            get
            {
                UserModelGeneralDto usuarioRetorno = null;

                var userIdentity = User.Identity as ClaimsIdentity;
                var idClaim = userIdentity.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                if (idClaim != null && idClaim.Value != null)
                {
                    string email = idClaim.Value;

                    usuarioRetorno = new UserModelGeneralDto
                    {
                        Email = email
                    };
                }
                return usuarioRetorno;
            }
        }
    }
}
