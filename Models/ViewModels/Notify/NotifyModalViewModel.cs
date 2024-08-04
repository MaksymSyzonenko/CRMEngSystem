namespace CRMEngSystem.Models.ViewModels.Notify
{
    public sealed class NotifyModalViewModel
    {
        public string NotifyText { get; init; } = default!;
        public bool IsError { get; init; }
    }
}
