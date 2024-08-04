using CRMEngSystem.Dto.Comment;

namespace CRMEngSystem.Models.ViewModels.Contact
{
    public sealed class ContactCommentsViewModel : ContactMenuTabViewModel
    {
        public IEnumerable<CommentDto> Comments { get; init; } = default!;
    }
}
