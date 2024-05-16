using System.Collections.Generic;
using UnityEngine;
using Workspace.Core.Common.Data;
using Workspace.Core.Systems.Room.Interior.Implementations;

namespace Workspace.Core.Systems.Room.Interior.Configs
{
    [CreateAssetMenu(fileName = "Wallpapers Config", menuName = "Configs/Interior/Wallpapers", order = 0)]
    public class WallpapersConfig : ScriptableObject, IContainerFiller<InteriorDataContainer>
    {
        [SerializeField] private Texture2D[] _wallpapers;

        public void Fill(ref InteriorDataContainer container)
        {
            container.Add<WallpapersElement, IReadOnlyList<Texture2D>>(_wallpapers);
        }
    }
}