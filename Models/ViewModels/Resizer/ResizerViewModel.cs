namespace CRMEngSystem.Models.ViewModels.Resizer
{
    public sealed class ResizerViewModel
    {
        public string TitleName { get; init; } = default!;
        public string SortActionName { get; init; } = default!;
        public string SortControllerName { get; init; } = default!;
        public object SortRouteValues { get; init; } = default!;
    }
}
