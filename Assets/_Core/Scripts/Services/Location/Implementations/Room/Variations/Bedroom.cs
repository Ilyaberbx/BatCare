using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Workspace.Core.Common.Data;
using Workspace.Core.Systems.Room.Interior;

namespace Workspace.Services.Location.Implementations.Room.Variations
{
    public class Bedroom : Room
    {
        [SerializeField] private IContainerFiller<InteriorDataContainer>[] _interiorDataFillers;
        [SerializeField] private InteriorSystem _interiorSystem;
        
        public override Task Enter(CancellationToken token)
        {
            InitializeInterior();

            return Task.CompletedTask;
        }

        public override Task Exit(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        private void InitializeInterior()
        {
            var interiorDataContainer = new InteriorDataContainer();

            foreach (var filler in _interiorDataFillers)
            {
                filler.Fill(ref interiorDataContainer);
            }
            
            _interiorSystem.SetData(interiorDataContainer);
        }
    }
}