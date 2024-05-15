namespace Workspace.Core.Systems.Room.Interior
{
    public interface IContainerFiller<TContainer> 
    {
        void Fill(ref TContainer container);
    }
}