using UnityEngine;
using Workspace.Core.Systems.Room.Interior.Abstractions;

namespace Workspace.Core.Systems.Room.Interior
{
    public class InteriorSystem : MonoBehaviour, ISystem
    {
        [SerializeField] private BaseInteriorElement[] _interiorElements;

        public void SetData(InteriorDataContainer dataContainer)
        {
            foreach (var element in _interiorElements)
            {
                if (dataContainer.TryGetData(element.GetType(), out var data))
                {
                    element.SetDerivedData(data);
                }
                else
                {
                    element.Disable();
                }
            }
        }

        public void Rebuild()
        {
            foreach (var element in _interiorElements)
            {
                element.Rebuild();
            }
        }
    }
}