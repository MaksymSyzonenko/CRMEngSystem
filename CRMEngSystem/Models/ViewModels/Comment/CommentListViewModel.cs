using CRMEngSystem.Dto.Comment;

namespace CRMEngSystem.Models.ViewModels.Comment
{
    public sealed class CommentListViewModel
    {
        public IEnumerable<CommentDto> Comments { get; init; } = default!;
        public int EntityId { get; init; } = default!;
        public string Text { get; init; } = default!;
        public string ActionName { get; init; } = default!;
        public string ControllerName { get; init; } = default!;
    }
}
