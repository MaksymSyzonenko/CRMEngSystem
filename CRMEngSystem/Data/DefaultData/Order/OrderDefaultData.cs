using CRMEngSystem.Data.Entities.Order;

namespace CRMEngSystem.Data.DefaultData.Order
{
    public static class OrderDefaultData
    {
        public static readonly List<OrderEntity> Orders = new()
        {
            new OrderEntity()
            {
                OrderId = 1,
                Status = Enums.OrderStatusValue.Completed,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.Low,
                DateTimeCreate = new DateTime(2004, 5, 11, 14, 56, 34),
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 3,
                CustomerId = 5,
            },
            new OrderEntity()
            {
                OrderId = 2,
                Status = Enums.OrderStatusValue.Processing,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.High,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 2,
                CustomerId = 2,
            },
            new OrderEntity()
            {
                OrderId = 3,
                Status = Enums.OrderStatusValue.Offer,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.Medium,
                DateTimeCreate = new DateTime(2007, 5, 11, 14, 56, 34),
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 3,
                CustomerId = 3,
            },
            new OrderEntity()
            {
                OrderId = 4,
                Status = Enums.OrderStatusValue.Completed,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.High,
                DateTimeCreate = new DateTime(2011, 5, 11, 14, 56, 34),
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 2,
                CustomerId = 4,
            },
            new OrderEntity()
            {
                OrderId = 5,
                Status = Enums.OrderStatusValue.Execution,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.Low,
                DateTimeCreate = new DateTime(2008, 5, 11, 14, 56, 34),
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 3,
                CustomerId = 2,
            },
            new OrderEntity()
            {
                OrderId = 6,
                Status = Enums.OrderStatusValue.Processing,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.Low,
                DateTimeCreate = new DateTime(2014, 5, 11, 14, 56, 34),
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 2,
                CustomerId = 3,
            },
            new OrderEntity()
            {
                OrderId = 7,
                Status = Enums.OrderStatusValue.Processing,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.Medium,
                DateTimeCreate = new DateTime(2001, 5, 11, 14, 56, 34),
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 3,
                CustomerId = 2,
            },
            new OrderEntity()
            {
                OrderId = 8,
                Status = Enums.OrderStatusValue.Execution,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.Medium,
                DateTimeCreate = new DateTime(2023, 10, 11, 14, 56, 34),
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 3,
                CustomerId = 5,
            },
            new OrderEntity()
            {
                OrderId = 9,
                Status = Enums.OrderStatusValue.Offer,
                DateTimeProcessingStatusStart = DateTime.Now,
                Priority = Enums.PriorityValue.High,
                DateTimeCreate = new DateTime(2001, 5, 11, 14, 56, 34),
                DateTimeUpdate = DateTime.Now,
                InitiatorId = 2,
                CustomerId = 5,
            }
        };
    }
}
