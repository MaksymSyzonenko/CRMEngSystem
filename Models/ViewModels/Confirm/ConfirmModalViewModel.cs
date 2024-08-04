namespace CRMEngSystem.Models.ViewModels.Confirm
{
    public sealed class ConfirmModalViewModel
    {
        public string NotifyText { get; init; } = default!;
        public string ConfirmActionName { get; init; } = default!;
        public string ControllerName { get; init; } = default!;
        public object RouteValues { get; init; } = default!;
    }
}
