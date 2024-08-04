
using CRMEngSystem.Data.Entities.Comment;

namespace CRMEngSystem.Data.DefaultData.Comment
{
    public static class OrderCommentsDefaultData
    {
        public static List<CommentEntity> Comments = new()
        {
            new()
            {
                CommentId = 1,
                Text = "Важливе замовлення, треба закрити до 18.10 та вислати комерційну пропозицію у 20-их числах.",
                DateTimeCreate = DateTime.Now,
                AuthorId = 2,
                RecipientOrderId = 2
            },
            new()
            {
                CommentId = 2,
                Text = "Додав до замовлення 7 позицій та оновив прайси.",
                DateTimeCreate = DateTime.Now,
                AuthorId = 2,
                RecipientOrderId = 2
            },
            new()
            {
                CommentId = 3,
                Text = "Перевірив замовлення, можна змінювати статус та відсилати комерційну пропозицію клієнту.",
                DateTimeCreate = DateTime.Now,
                AuthorId = 3,
                RecipientOrderId = 2
            },
            new()
            {
                CommentId = 4,
                Text = "Необхідно додати до замовлення 2 позиції 15mm BPC32Y Steam TrapPN25/40 Standard Capsule та 1 позицію DN1080 Long Level Gauge",
                DateTimeCreate = DateTime.Now,
                AuthorId = 2,
                RecipientOrderId = 2
            }
        };
    }
}
