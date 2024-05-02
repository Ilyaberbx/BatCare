using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Workspace.Core.Actions.Abstractions;
using Workspace.Extensions;

namespace Workspace.Core.Actions.Implementations.Visual
{
    [Serializable]
    public class SwitchAction : VisualAction
    {
        [SerializeField] private RectTransform _handleTransform;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private RectTransform _onPoint;
        [SerializeField] private RectTransform _offPoint;
        [SerializeField] private float _duration;

        public override Task Execute(CancellationToken token)
        {
            var position = _toggle.isOn ? _onPoint.position : _offPoint.position;

            if (_duration > 0)
                return _handleTransform.DOMove(position, _duration).ToTask(token);
            
            _handleTransform.position = position;
            return Task.CompletedTask;
        }
    }
}