using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using UnityEngine;
using Workspace.Infrastructure.Global.States;

namespace Workspace.Services.SceneManagement.Data
{
    [Serializable]
    public class SceneGroupStatePair
    {
        [Select(typeof(GlobalState))] 
        [SerializeField] private SerializedType _stateSerializedType;
        
        [SerializeField] private SceneGroup _sceneGroup;

        public Type StateType => _stateSerializedType.Type;

        public SceneGroup Group => _sceneGroup;
    }
}