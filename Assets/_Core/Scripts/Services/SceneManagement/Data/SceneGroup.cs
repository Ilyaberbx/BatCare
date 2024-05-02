using System;
using Better.SceneManagement.Runtime;
using UnityEngine;

namespace Workspace.Services.SceneManagement.Data
{
    [Serializable]
    public class SceneGroup
    {
        [SerializeField] private SceneReference[] _sceneReferences;
        
        public SceneReference[] SceneReferences => _sceneReferences;
    }
    
}