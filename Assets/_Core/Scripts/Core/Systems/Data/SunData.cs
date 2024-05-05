using System;
using UnityEngine;

namespace Workspace.Core.Systems.Data
{
    [Serializable]
    public class SunData
    {
        [SerializeField] private float _intensity;
        [SerializeField] private Light _light;

        public Light Light => _light;

        public float Intensity => _intensity;
    }
}