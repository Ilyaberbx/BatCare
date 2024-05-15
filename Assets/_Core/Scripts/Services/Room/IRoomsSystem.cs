using Workspace.Core.Systems;

namespace Workspace.Services.Room
{
    public interface IRoomsSystem : ISystem
    {
        int CurrentRoomIndex { get; }
        
    }
}