using System;
using Better.Locators.Runtime;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Workspace.Core.Systems.Data;
using Workspace.Services.Time;
using Workspace.Utilities;

namespace Workspace.Core.Systems.Environment
{
    public class SunSystem : MonoBehaviour
    {
        private static readonly int Blend = Shader.PropertyToID("_Blend");

        [SerializeField] private Material _skyBox;
        [SerializeField] private SunData _sun;
        [SerializeField] private AnimationCurve _lightIntensityCurve;
        [SerializeField] private Color _dayAmbientColor;
        [SerializeField] private Color _nightAmbientColor;
        [SerializeField] private Volume _volume;

        private ColorAdjustments _colorAdjustments;
        private GameTimeService _gameTimeService;

        private void Start()
        {
            _gameTimeService = ServiceLocator.Get<GameTimeService>();

            _volume.profile.TryGet(out _colorAdjustments);

            _gameTimeService.TimeProperty.Subscribe(OnTimeUpdated);
        }

        private void OnDestroy()
        {
            _gameTimeService.TimeProperty.Unsubscribe(OnTimeUpdated);
        }

        private void OnTimeUpdated(DateTime time)
        {
            UpdateSunRotation();
            UpdateLightSettings();
            UpdateColorAdjustments();
            UpdateSlyBoxBlend();
        }

        private void UpdateColorAdjustments()
        {
            float dotProduct = Vector3.Dot(_sun.Transform.forward, Vector3.up);
            _colorAdjustments.colorFilter.value = Color.Lerp(_dayAmbientColor, _nightAmbientColor,
                _lightIntensityCurve.Evaluate(dotProduct));
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
            _sun.MainLight.intensity = Mathf.Lerp(_sun.MinIntensity, _sun.MaxIntensity, _lightIntensityCurve.Evaluate(dotProduct));
        }

        private void UpdateSunRotation()
        {
            _sun.Transform.rotation = Quaternion.AngleAxis(SunUtility.GetSunAngle(), Vector3.right);
        }
    }
}