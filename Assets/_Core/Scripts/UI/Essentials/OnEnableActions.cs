using System.Threading;
using Better.Attributes.Runtime.Select;
using UnityEngine;
using Workspace.Core.Actions.Abstractions;
using Workspace.Core.Actions.Implementations.Visual;

namespace Workspace.UI.Essentials
{
    public class OnEnableActions : MonoBehaviour
    {
        [SerializeReference, Select] private VisualAction[] _actions;
        private CancellationTokenSource _tokenSource;

        private void Awake() => _actions.Initialize();

        private async void OnEnable()
        {
            _tokenSource = new CancellationTokenSource();

            foreach (var action in _actions)
            {
                await action.Execute(_tokenSource.Token);
            }
        }

        private void OnDisable()
        {
            _tokenSource.Cancel();
        }
    }
}