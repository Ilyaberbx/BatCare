using UnityEngine;
using UnityEngine.Serialization;
using Workspace.Services.Persistence.Data;

namespace Workspace.Services.Persistence
{
    [CreateAssetMenu(fileName = "User Service Settings", menuName = "Configs/Services/User", order = 0)]
    public class UserServiceSettings : ScriptableObject
    {
        [FormerlySerializedAs("_inGameSettingsData")] [SerializeField] private SettingsData settingsData;

        public SettingsData Settings => settingsData;
    }
}