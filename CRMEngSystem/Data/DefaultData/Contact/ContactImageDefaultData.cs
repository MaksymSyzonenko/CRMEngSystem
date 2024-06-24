using CRMEngSystem.Data.Entities.Contact;

namespace CRMEngSystem.Data.DefaultData.Contact
{
    public static class ContactImageDefaultData
    {
        public static readonly List<ContactImageEntity> ContactsImage = new()
        {
            new ContactImageEntity()
            {
                ContactImageId = 1,
                ContactId = 1,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_syzonenko_maksym.jpg")
            },
            new ContactImageEntity()
            {
                ContactImageId = 3,
                ContactId = 3,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_lisniy_mykola.jpg")
            },
            new ContactImageEntity()
            {
                ContactImageId = 4,
                ContactId = 4,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_1.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 5,
                ContactId = 5,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_2.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 6,
                ContactId = 6,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_3.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 7,
                ContactId = 7,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_4.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 8,
                ContactId = 8,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_5.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 9,
                ContactId = 9,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_6.png")
            },
            new ContactImageEntity()
            {   
                ContactImageId = 10,
                ContactId = 10,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_1.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 11,
                ContactId = 11,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_2.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 12,
                ContactId = 12,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_3.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 13,
                ContactId = 13,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_5.png")
            },
            new ContactImageEntity()
            {
                ContactImageId = 14,
                ContactId = 14,
                Bytes = File.ReadAllBytes($"D:\\На новый пк\\Проэкты\\Универ\\CRM-EngSystem-WebApp\\CRM-EngSystem-WebApp\\wwwroot\\images\\contact\\contactimage_4.png")
            }
        };
    }
}
