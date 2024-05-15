using UnityEngine;
using Workspace.Core.Systems.Room.Interior;
using Workspace.Core.Systems.Room.Interior.Configs;

namespace Workspace.Core.Systems.Room
{
    public class RoomFacade : MonoBehaviour
    {
        [SerializeField] private InteriorSystem _interiorSystem;
        [SerializeField] private InteriorConfig _config;
        
        public void Initialize()
        {
            InitializeInterior();
        }

        private void InitializeInterior()
        {
            InteriorDataContainer container = new InteriorDataContainer();
            
            _config.Fill(ref container);
            
            _interiorSystem.SetData(container);
        }
    }
}