namespace CRMEngSystem.Models.ViewModels.Pagination
{
    public abstract class PaginationViewModel
    {
        public string ActionName { get; set; } = default!;
        public string ControllerName { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int NumberItemsPerPage { get; init; } = 24;
        public int TotalPageCount { get; set; }
    }
}
