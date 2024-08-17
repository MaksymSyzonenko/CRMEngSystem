﻿using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.Enterprise
{
    public sealed class EnterpriseSelectEntity : IEntity
    {
        public int EnterpriseSelectId { get; set; }
        public int EnterpriseId { get; set; }
        public string UserId { get; set; } = default!;
    }
}
