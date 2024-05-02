using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Workspace.Core.Actions.Abstractions;
using Workspace.Extensions;

namespace Workspace.Core.Actions.Implementations.Visual
{
    public class ColorAnimateAction : VisualAction
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _duration;
        [SerializeField] private Color _targetColor;
        [SerializeField] private bool  _waitForCompletion;
        
        public override Task Execute(CancellationToken token)
        {
            var task =  DOTween.To(
                () => _image.color
                , setter => _image.color = setter
                , _targetColor
                , _duration)
                .From()
                .ToTask(token);

            return _waitForCompletion ? task : Task.CompletedTask;
        }
    }
}