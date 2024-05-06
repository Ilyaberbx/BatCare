using UnityEngine;

namespace Workspace.Services.Time
{
    [CreateAssetMenu(fileName = "Time Of Day Settings", menuName = "Configs/Services/TimeOfDay", order = 0)]
    public class TimeOfDaySettings : ScriptableObject
    {
        [SerializeField] private float _sunsetHour;
        [SerializeField] private float _sunriseHour;

        public float SunsetHour => _sunsetHour;

        public float SunriseHour => _sunriseHour;
    }
}