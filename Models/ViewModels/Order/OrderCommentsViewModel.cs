using CRMEngSystem.Dto.Comment;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderCommentsViewModel : OrderMenuTabViewModel
    {
        public IEnumerable<CommentDto> Comments { get; init; } = default!;
    }
}
