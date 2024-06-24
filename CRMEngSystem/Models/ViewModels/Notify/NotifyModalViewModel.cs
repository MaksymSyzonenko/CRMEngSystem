namespace CRMEngSystem.Models.ViewModels.Notify
{
    public sealed class NotifyModalViewModel
    {
        public string NotifyText { get; init; } = default!;
        public string CloseActionName { get; init; } = default!;
        public string ControllerName { get; init; } = default!;
        public bool IsError { get; init; }
        public object RouteValues { get; init; } = default!;
    }
}
