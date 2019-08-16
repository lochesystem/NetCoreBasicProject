using System;

namespace BasicProject.Infra.Entity.Base
{
    public class EntityCreateBase : EntityBase
    {
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public bool Active { get; set; }

    }
}
