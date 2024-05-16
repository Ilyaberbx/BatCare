using System;
using System.Collections.Generic;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Better.Commons.Runtime.Utility;
using UnityEngine;
using Workspace.Core.Systems.Room.Interior.Abstractions;

namespace Workspace.Core.Systems.Room.Interior.Implementations
{
    public class WallpapersElement : BaseInteriorElement<IReadOnlyList<Texture2D>>
    {
        [SerializeField] private MeshRenderer _renderer;
        
        private static readonly int MainTexture = Shader.PropertyToID("_MainTexture");
        private SerializedDictionary<int, int> WallpapersByRoom => UserService.WallpapersProperty.Value.WallpapersByRoom;

        public override void Rebuild()
        {
            var propertyBlock = new MaterialPropertyBlock();
            
            var index = GetWallpapersIndex();

            var texture = Data[index];
            
            if (texture != null)
            {
                propertyBlock.SetTexture(MainTexture, texture);
            }
            else
            {
                DebugUtility.LogException<InvalidOperationException>();
            }
            
            _renderer.SetPropertyBlock(propertyBlock);
        }

        private int GetWallpapersIndex()
        {
            if (!WallpapersByRoom.TryGetValue(RoomIndex, out var index))
            {
                WallpapersByRoom.Add(RoomIndex, 0);   
            }

            return index;
        }
    }
}