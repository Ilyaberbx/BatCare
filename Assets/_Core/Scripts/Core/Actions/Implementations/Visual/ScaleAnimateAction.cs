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
    public class ScaleAnimateAction : VisualAction
    {
        [SerializeField] private bool _waitForCompetition;
        [SerializeField] private Transform _object;
        [SerializeField] private Vector3 _targetScale;
        [SerializeField] private float _duration;
        
        public override Task Execute(CancellationToken token)
        {
            var task = _object.DOScale(_targetScale, _duration).ToTask(token);

            return _waitForCompetition ? task : Task.CompletedTask;
        }
    }
}