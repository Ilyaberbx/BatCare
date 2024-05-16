using System;
using System.Collections.Generic;
using UnityEngine;
using Workspace.Core.Common.Data;
using Workspace.Core.Systems.Room.Interior.Implementations;

namespace Workspace.Core.Systems.Room.Interior.Configs
{
    [Serializable]
    public class WallpapersConfig : IContainerFiller<InteriorDataContainer>
    {
        [SerializeField] private Texture2D[] _wallpapers;

        public void Fill(ref InteriorDataContainer container)
        {
            container.Add<WallpapersElement, IReadOnlyCollection<Texture2D>>(_wallpapers);
        }
    }
}