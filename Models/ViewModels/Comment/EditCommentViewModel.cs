namespace CRMEngSystem.Models.ViewModels.Comment
{
    public sealed class EditCommentViewModel
    {
        public int CommentId { get; init; }
        public int AuthorId { get; init; }
        public string ActionName { get; init; } = default!;
        public string ControllerName { get; init; } = default!;
        public int EntityId { get; init; }
        public string NewText { get; set; } = default!;
    }
}
