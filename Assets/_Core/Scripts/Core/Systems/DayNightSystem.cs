using System;
using Better.Locators.Runtime;
using UnityEngine;
using Workspace.Core.Systems.Data;
using Workspace.Services.Time;

namespace Workspace.Core.Systems
{
    public class DayNightSystem : MonoBehaviour
    {
        [SerializeField] private SunData _sun;
        [SerializeField] private SunData _moon;

        [SerializeField] private float _sunsetHour;
        [SerializeField] private float _sunriseHour;
        [SerializeField] private float _midnightHour;

        private GameTimeService _gameTimeService;

        private void Start()
        {
            _gameTimeService = ServiceLocator.Get<GameTimeService>();

            _gameTimeService.TimeProperty.Subscribe(OnTimeChanged);
        }

        private void OnDestroy()
        {
            _gameTimeService.TimeProperty.Unsubscribe(OnTimeChanged);
        }

        private void OnTimeChanged(DateTime time)
        {
        }
    }
}