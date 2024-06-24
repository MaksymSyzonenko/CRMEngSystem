using CRMEngSystem.Dto.Comment;

namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseCommentsViewModel : EnterpriseMenuTabViewModel
    {
        public IEnumerable<CommentDto> Comments { get; init; } = default!;
    }
}
