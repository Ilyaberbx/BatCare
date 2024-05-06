using Better.Locators.Runtime;
using UnityEngine;
using Workspace.Services.TimeScale;

namespace Workspace.Core.Common
{
    [RequireComponent(typeof(Animator))]
    public class CustomAnimator : MonoBehaviour
    {
        private TimeScaleService _timeScaleService;
        private Animator _animator;

        protected Animator Animator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        protected virtual void Awake()
        {
            _timeScaleService = ServiceLocator.Get<TimeScaleService>();

            _timeScaleService.Subscribe(OnTimeScaleChanged);

            OnTimeScaleChanged(_timeScaleService.TimeScale);
        }

        protected virtual void OnDestroy()
        {
            _timeScaleService.Unsubscribe(OnTimeScaleChanged);
        }

        private void OnTimeScaleChanged(float timeScale)
        {
            _animator.speed = timeScale;
        }
    }
}