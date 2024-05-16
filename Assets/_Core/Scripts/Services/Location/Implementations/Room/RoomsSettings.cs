using UnityEngine;

namespace Workspace.Services.Location.Implementations.Room
{
    [CreateAssetMenu(fileName = "Rooms Settings", menuName = "Configs/Services/Rooms", order = 0)]
    public class RoomsSettings : ScriptableObject
    {
        [SerializeField] private RoomData[] _roomsData;

        public RoomData[] Data => _roomsData;
    }
}