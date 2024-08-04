using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Loaders.Core;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Data.Loaders.User
{
    public sealed class UserDataLoader : IEntityDataLoader<UserEntity>
    {
        public bool Contact { get; init; }
        public bool Image { get; init; }
        public bool Comments { get; init; }
        public UserDataLoader(bool contact, bool image, bool comments)
        {
            Contact = contact;
            Image = image;
            Comments = comments;
        }

        public IQueryable<UserEntity> LoadData(IQueryable<UserEntity> query)
        {
            query = Contact ? query.Include(user => user.Contact).ThenInclude(contact => contact.Details) : query;
            query = Image ? query.Include(user => user.Contact).ThenInclude(contact => contact.Image) : query;
            query = Comments ? query.Include(user => user.Comments) : query;
            return query;
        }
    }
}
