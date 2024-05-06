using System;
using UnityEngine;

namespace Workspace.Core.Systems.Data
{
    [Serializable]
    public class SunData
    {
        [SerializeField] private float _maxIntensity;
        [SerializeField] private float _minIntensity;
        [SerializeField] private Light _mainLight;

        public Light MainLight => _mainLight;

        public float MaxIntensity => _maxIntensity;

        public Transform Transform => _mainLight.transform;

        public float MinIntensity => _minIntensity;
    }
}