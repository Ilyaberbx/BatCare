using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Workspace.Core.Actions.Abstractions;
using Workspace.Extensions;

namespace Workspace.Core.Actions.Implementations.Visual
{
    [Serializable]
    public class ToggleCanvasGroup : VisualAction
    {
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private bool _isActive;

        public override Task Execute(CancellationToken token)
        {
            if (_isActive)
            {
                if (_canvasGroup.interactable && Math.Abs(_canvasGroup.alpha - 1f) < 0.1f)
                {
                    return Task.CompletedTask;
                }

                _canvasGroup.interactable = true;
                return _canvasGroup.DOFade(1f, _duration).ToTask(token);
            }

            if (_canvasGroup.interactable == false && _canvasGroup.alpha == 0f)
            {
                return Task.CompletedTask;
            }

            _canvasGroup.interactable = false;
            return _canvasGroup.DOFade(0f, _duration).ToTask(token);
        }
    }
}