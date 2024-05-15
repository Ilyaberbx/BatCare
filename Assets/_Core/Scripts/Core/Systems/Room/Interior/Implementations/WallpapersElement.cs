using System.Collections.Generic;
using UnityEngine;
using Workspace.Core.Systems.Room.Interior.Abstractions;

namespace Workspace.Core.Systems.Room.Interior.Implementations
{
    public class WallpapersElement : BaseInteriorElement<IReadOnlyCollection<Texture2D>>
    {
        [SerializeField] private MeshRenderer _renderer;
        
        private static readonly int MainTexture = Shader.PropertyToID("_MainTexture");

        protected override void Rebuild()
        {
            var propertyBlock = new MaterialPropertyBlock();
            
            _renderer.SetPropertyBlock(propertyBlock);
        }
    }
}