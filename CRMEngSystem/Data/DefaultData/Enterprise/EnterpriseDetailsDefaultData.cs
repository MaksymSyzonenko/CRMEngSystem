using CRMEngSystem.Data.Entities.Enterprise;

namespace CRMEngSystem.Data.DefaultData.Enterprise
{
    public static class EnterpriseDetailsDefaultData
    {
        public static readonly List<EnterpriseDetailsEntity> EnterprisesDetails = new()
        {
            new EnterpriseDetailsEntity()
            {
                EnterpriseId = 1,
                NameUA = "Стимекс Інжиніринг Груп",
                NameEN = "Stimex Engineering Group",
                OwnershipFormUA = "Товариство з обмеженою відповідальністю",
                OwnershipFormEN = "Limited Liability Company",
                IndustryBranch = "Оптова торгівля",
                Franchise = "Відсутня",
                Country = "Україна",
                City = "Кременчук",
                Region = "Кременчуцька область",
                PostalCode = "36000",
                Street = "вул. Шевченка, буд. 51, офіс 5",
                Coordinate = "49.07064930134435, 33.41136582439195"
            },
            new EnterpriseDetailsEntity()
            {
                EnterpriseId = 2,
                NameUA = "АктивПром",
                NameEN = "ActiveProm",
                OwnershipFormUA = "Товариство з обмеженою відповідальністю",
                OwnershipFormEN = "Limited Liability Company",
                IndustryBranch = "Машинобудування",
                Franchise = "Відсутня",
                Country = "Україна",
                City = "Львів",
                Region = "Львівська область",
                PostalCode = "03056",
                Street = "вул. Січових Стрільців, 25",
                Coordinate = "50.45627378418656, 30.500676964417707"
            },
            new EnterpriseDetailsEntity()
            {
                EnterpriseId = 3,
                NameUA = "Глобус",
                NameEN = "Globus",
                OwnershipFormUA = "Акціонерне товариство",
                OwnershipFormEN = "Joint-stock company",
                IndustryBranch = "Торгівля",
                Franchise = "Відсутня",
                Country = "Україна",
                City = "Київ",
                Region = "Київська область",
                PostalCode = "79000",
                Street = "вул. Шевченка, 20",
                Coordinate = "49.84322935396932, 24.015965681978702"
            },
            new EnterpriseDetailsEntity()
            {
                EnterpriseId = 4,
                NameUA = "ТехноСервіс",
                NameEN = "TechnoService",
                OwnershipFormUA = "Приватне підприємство",
                OwnershipFormEN = "Private Enterprise",
                IndustryBranch = "Інформаційні технології",
                Franchise = "Відсутня",
                Country = "Україна",
                City = "Одеса",
                Region = "Одеська область",
                PostalCode = "61000",
                Street = "пр. Науки, 14",
                Coordinate = "50.40302364063281, 30.526082184658364"
            },
            new EnterpriseDetailsEntity()
            {
                EnterpriseId = 5,
                NameUA = "Моршинський завод мінеральних вод МЗМВ Оскар",
                NameEN = "Budivelnyk",
                OwnershipFormUA = "Колективне підприємство",
                OwnershipFormEN = "Collective Enterprise",
                IndustryBranch = "Будівництво",
                Franchise = "Відсутня",
                Country = "Україна",
                City = "Харків",
                Region = "Харківська область",
                PostalCode = "65000",
                Street = "вул. Дерибасівська, 15",
                Coordinate = "46.48399678196123, 30.73914849717105"
            }

        };
    }
}
