using System;
using BasicProject.Domain.Extensions;
using BasicProject.Service.DTOs.Log;
using Microsoft.AspNetCore.Mvc;

namespace BasicProject.Service.Helpers
{
    public static class LogHelper
    {

        public static LogCreateDTO GenerateLog(ControllerContext controllerContext, string methodName, string friendlyMessage, string userEmail,
            Exception exception)
        {
            return new LogCreateDTO
            {
                ClassName = controllerContext.RouteData.Values["action"].ToString(),
                MethodName = methodName,
                Message = exception.Message,
                ControllerName = controllerContext.RouteData.Values["controller"].ToString(),
                Exception = ExceptionExtensions.PrepareExceptionString(exception),
                FriendlyMessage = friendlyMessage,
                StackTrace = exception.StackTrace,
                UserId = Guid.Empty,
                UserEmail = userEmail
            };
        }
    }
}
