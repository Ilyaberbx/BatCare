using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Workspace.Services.Location.Implementations.Room
{
    [Serializable]
    public class RoomData
    {
        [SerializeField] private AssetReference _reference;
        
        [SerializeReference, Select(typeof(Room))] private SerializedType _serializedType;

        public AssetReference Reference => _reference;

        public Type Type => _serializedType.Type;
    }
}