using UnityEngine;

namespace Workspace.Services.UI
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Configs/Services/UI", order = 0)]
    public class UIСonfig : ScriptableObject
    {
        [SerializeField] private PresentersConfig presentersConfig;

        public PresentersConfig PresentersConfig => presentersConfig;
    }
}