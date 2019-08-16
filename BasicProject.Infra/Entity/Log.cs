using System;
using BasicProject.Infra.Entity.Base;

namespace BasicProject.Infra.Entity
{
    public class Log : EntityBase
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
