using UnityEngine;

namespace Workspace.Services.Time
{
    [CreateAssetMenu(fileName = "Time Service Settings", menuName = "Configs/Services/Time", order = 0)]
    public class TimeServiceSettings : ScriptableObject
    {
        [SerializeField, Min(0.1f)] private float _timeMultiplier;
        [SerializeField] private float _startHour;
        [SerializeField] private float _sunriseHour;
        [SerializeField] private float _sunsetHour;

        public float TimeMultiplier => _timeMultiplier;

        public float StartHour => _startHour;

        public float SunriseHour => _sunriseHour;

        public float SunsetHour => _sunsetHour;
    }
}