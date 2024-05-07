using System;
using System.Threading;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Components.UI;
using UnityEngine;
using UnityEngine.UI;
using Workspace.Core.Actions.Abstractions;
using Workspace.Extensions;

namespace Workspace.UI.Essentials
{
    [RequireComponent(typeof(Button))]
    public class ActionButton : UIMonoBehaviour
    {
        public event Action OnClick;

        [SerializeField] private Button _button;
        [SerializeReference, Select] private VisualAction[] _actions;

        private CancellationTokenSource _tokenSource;


        private void Start()
        {
            foreach (var action in _actions)
                action.Initialize();

            _tokenSource = new CancellationTokenSource();
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClicked);
            _tokenSource.Cancel();
        }

        private async void OnClicked()
        {
            await _actions.Execute(_tokenSource.Token);

            OnClick?.Invoke();
        }
    }
}