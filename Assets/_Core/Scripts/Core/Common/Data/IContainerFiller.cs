namespace Workspace.Core.Common.Data
{
    public interface IContainerFiller<TContainer> where TContainer : IDataContainer
    {
        void Fill(ref TContainer container);
    }
}