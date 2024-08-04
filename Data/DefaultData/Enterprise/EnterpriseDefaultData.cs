using CRMEngSystem.Data.Entities.Enterprise;

namespace CRMEngSystem.Data.DefaultData.Enterprise
{
    public static class EnterpriseDefaultData
    {
        public static readonly List<EnterpriseEntity> Enterprises = new List<EnterpriseEntity>()
        {
            new EnterpriseEntity()
            {
                EnterpriseId = 1,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now
            },
            new EnterpriseEntity()
            {
                EnterpriseId = 2,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now
            },
            new EnterpriseEntity()
            {
                EnterpriseId = 3,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now
            },
            new EnterpriseEntity()
            {
                EnterpriseId = 4,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now
            },
            new EnterpriseEntity()
            {
                EnterpriseId = 5,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now
            }
        };
    }
}
