using CRMEngSystem.Data.Entities.WareHouse;

namespace CRMEngSystem.Data.DefaultData.WareHouse
{
    public static class WareHouseDefaultData
    {
        public static readonly List<WareHouseEntity> WareHouses = new()
        {
            new WareHouseEntity()
            {
                WareHouseId = 1,
                Name = "Склад Кременчук",
                Country = "Україна",
                City = "Кременчук",
                Region = "Полтавська область",
                PostalCode = "39600",
                Street = "Вул. Лесі Українки 10А"
            },
            new WareHouseEntity()
            {
                WareHouseId = 2,
                Name = "Склад Тернопіль",
                Country = "Україна",
                City = "Тернопіль",
                Region = "Тернопільска область",
                PostalCode = "56780",
                Street = "Вул. Шевченка 89Б"
            }
        };
    }
}
