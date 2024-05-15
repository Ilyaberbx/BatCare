using System;
using Better.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Workspace.Services.SceneManagement.Data
{
    [Serializable]
    public class SceneGroup
    {
        [FormerlySerializedAs("_sceneReferences")] [SerializeField] private SceneReference[] references;
        
        public SceneReference[] References => references;
    }
    
}