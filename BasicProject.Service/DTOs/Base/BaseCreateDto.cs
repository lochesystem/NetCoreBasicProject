using System;

namespace BasicProject.Service.DTOs.Base
{
    public class BaseCreateDto : BaseDto
    {
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public bool Active { get; set; }
    }
}
