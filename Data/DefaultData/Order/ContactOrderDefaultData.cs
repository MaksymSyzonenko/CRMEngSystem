using CRMEngSystem.Data.Entities.Order;

namespace CRMEngSystem.Data.DefaultData.Order
{
    public static class ContactOrderDefaultData
    {
        public static readonly List<ContactOrderEntity> ContactOrders = new()
        {
            new ContactOrderEntity()
            {
                OrderId = 1,
                ContactId = 2
            },
            new ContactOrderEntity()
            {
                OrderId = 1,
                ContactId = 3
            },
            new ContactOrderEntity()
            {
                OrderId = 2,
                ContactId = 4
            },
            new ContactOrderEntity()
            {
                OrderId = 2,
                ContactId = 5
            },
            new ContactOrderEntity()
            {
                OrderId = 2,
                ContactId = 3
            },
            new ContactOrderEntity()
            {
                OrderId = 4,
                ContactId = 3
            },
            new ContactOrderEntity()
            {
                OrderId = 5,
                ContactId = 3
            },
            new ContactOrderEntity()
            {
                OrderId = 7,
                ContactId = 3
            },
            new ContactOrderEntity()
            {
                OrderId = 8,
                ContactId = 3
            }
        };
    }
}
