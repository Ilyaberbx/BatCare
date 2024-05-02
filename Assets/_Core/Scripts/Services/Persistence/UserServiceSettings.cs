using UnityEngine;
using Workspace.Services.Persistence.Data.Settings;

namespace Workspace.Services.Persistence
{
    [CreateAssetMenu(fileName = "User Service Settings", menuName = "Configs/Services/User", order = 0)]
    public class UserServiceSettings : ScriptableObject
    {
        [SerializeField] private InGameSettingsData _inGameSettingsData;

        public InGameSettingsData InGameInGameSettings => _inGameSettingsData;
    }
}