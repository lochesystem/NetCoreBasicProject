using System;
using BasicProject.Domain.Model.Base;

namespace BasicProject.Domain.Model
{
    public class Log : BaseModel
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Message { get; set; }
        public DateTimeOffset LogDate { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }

        public string ControllerName { get; set; }
        public string Exception { get; set; }
        public string FriendlyMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
