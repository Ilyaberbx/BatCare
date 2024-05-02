namespace Workspace.Services.EventBus.Handlers
{
    public interface IApplicationCloseSubscriber : IGlobalSubscriber
    {
        void OnApplicationClose();
    }
}