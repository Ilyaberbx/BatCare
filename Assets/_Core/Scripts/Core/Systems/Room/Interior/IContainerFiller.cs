using Workspace.Core.Common;

namespace Workspace.Core.Systems.Room.Interior
{
    public interface IContainerFiller<TContainer> where TContainer : IDataContainer
    {
        void Fill(ref TContainer container);
    }
}