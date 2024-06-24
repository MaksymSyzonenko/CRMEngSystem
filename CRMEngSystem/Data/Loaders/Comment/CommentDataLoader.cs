using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Loaders.Comment
{
    public sealed class CommentDataLoader : IEntityDataLoader<CommentEntity>
    {
        public bool Author { get; init; }
        public bool RecipientContact { get; init; }
        public bool RecipientEnterprise { get; init; }
        public bool RecipientOrder { get; init; }
        public CommentDataLoader(bool author, bool recipientContact, bool recipientEnterprise, bool recipientOrder)
        {
            Author = author;
            RecipientContact = recipientContact;
            RecipientEnterprise = recipientEnterprise;
            RecipientOrder = recipientOrder;
        }

        public IQueryable<CommentEntity> LoadData(IQueryable<CommentEntity> query)
        {
            query = Author ? query.Include(comment => comment.Author).ThenInclude(author => author.Details) : query;
            query = RecipientContact ? query.Include(comment => comment.RecipientContact).ThenInclude(contact => contact!.Details) : query;
            query = RecipientEnterprise ? query.Include(comment => comment.RecipientEnterprise).ThenInclude(enterprise => enterprise!.Details) : query;
            query = RecipientOrder ? query.Include(comment => comment.RecipientOrder) : query;
            return query;
        }
    }
}
