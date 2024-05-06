using Better.Locators.Runtime;
using TMPro;
using UnityEngine;
using Workspace.Core.Systems.Data;
using Workspace.Services.Time;
using Workspace.Utilities;

namespace Workspace.Core.Systems.Environment
{
    public class SunSystem : MonoBehaviour
    {
        [SerializeField] private Material _skyBox;
        [SerializeField] private SunData _sun;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private AnimationCurve _lightIntensityCurve;
        
        private GameTimeService _gameTimeService;
        private static readonly int Blend = Shader.PropertyToID("_Blend");

        private float SunAngle => SunUtility.GetSunAngle();

        private void Start()
        {
            _gameTimeService = ServiceLocator.Get<GameTimeService>();
        }

        private void Update()
        {
            _timeText.text = _gameTimeService.TimeProperty.Value.TimeOfDay.ToString();
            
            UpdateSunRotation();
            UpdateLightSettings();
            UpdateSlyBoxBlend();
        }

        private void UpdateSlyBoxBlend()
        {
            float dotProduct = Vector3.Dot(_sun.Transform.forward, Vector3.up);
            float blend = Mathf.Lerp(0, 1, _lightIntensityCurve.Evaluate(dotProduct));
            _skyBox.SetFloat(Blend, blend);
        }

        private void UpdateLightSettings()
        {
            float dotProduct = Vector3.Dot(_sun.Transform.forward, Vector3.down);
            _sun.MainLight.intensity = Mathf.Lerp(0, _sun.MaxIntensity, _lightIntensityCurve.Evaluate(dotProduct));
        }

        private void UpdateSunRotation()
        {
            _sun.Transform.rotation = Quaternion.AngleAxis(SunAngle, Vector3.right);
        }
    }
}