using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.DefaultData.Comment;

namespace CRMEngSystem.Data.Configurations.Comment
{
    public sealed class CommentEntityConfiguration : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.HasKey(comment => comment.CommentId);

            builder.HasOne(comment => comment.RecipientContact)
                .WithMany(contact => contact.Comments)
                .HasForeignKey(comment => comment.RecipientContactId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(comment => comment.RecipientEnterprise)
                .WithMany(enterprise => enterprise.Comments)
                .HasForeignKey(comment => comment.RecipientEnterpriseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(comment => comment.RecipientOrder)
                .WithMany(order => order.Comments)
                .HasForeignKey(comment => comment.RecipientOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasData(OrderCommentsDefaultData.Comments);        
        }
    }
}
