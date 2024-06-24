namespace CRMEngSystem.Dto.Comment
{
    public sealed class CommentDto
    {
        public int CommentId { get; init; }
        public string AuthorInitials { get; init; } = default!;
        public int AuthorId { get; init; } = default!;
        public string Text { get; init; } = default!;
        public DateTime DateTimeCreate { get; init; }
        public string ActionName { get; set; } = default!;
        public string ControllerName { get; set; } = default!;
        public int EntityId { get; set; }
    }
}
