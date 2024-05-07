using System.Linq;
using UnityEngine;
using Workspace.Infrastructure.Global.States;
using Workspace.Services.SceneManagement.Data;

namespace Workspace.Services.App.Implementations.Global
{
    [CreateAssetMenu(fileName = "Global Service Settings", menuName = "Configs/Services/Global", order = 0)]
    
    public class GlobalServiceSettings : ScriptableObject
    {
        [SerializeField] private SceneGroupStatePair[] _groupStatePairs;

        public SceneGroup GetSceneGroup<TState>() where TState : GlobalState
        {
            var pair = _groupStatePairs.FirstOrDefault(pair => pair.StateType == typeof(TState));
            
            return pair?.Group;
        }
    }
}