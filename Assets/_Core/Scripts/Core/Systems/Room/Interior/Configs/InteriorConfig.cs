using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace Workspace.Core.Systems.Room.Interior.Configs
{
    [CreateAssetMenu(fileName = "Interior Config", menuName = "Configs/Room/Interior", order = 0)]
    public class InteriorConfig : ScriptableObject, IContainerFiller<InteriorDataContainer>
    {
        [SerializeReference, Select] private IContainerFiller<InteriorDataContainer>[] _fillers;
        
        public void Fill(ref InteriorDataContainer container)
        {
            if (_fillers.IsEmpty())
                return;
            
            foreach (var filler in _fillers)
            {
                filler.Fill(ref container);
            }
        }
    }
}