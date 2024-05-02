using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;
using Workspace.Core.MVP.Abstractions;
using Workspace.Utilities;
using Object = UnityEngine.Object;

namespace Workspace.Services.UI.Abstractions
{
    [Serializable]
    public abstract class UiService : PocoService<UIСonfig>
    {
        [SerializeField] private Transform _root;
        private BasePresenter _currentPresenter;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<TPresenter> Open<TPresenter, TModel>(TModel model)
            where TPresenter : BasePresenter<TModel>
            where TModel : IModel
        {
            var data = Settings
                .PresentersConfig
                .Data
                .FirstOrDefault(t => t.SerializedType.Type == typeof(TPresenter));

            if (data == null)
            {
                throw new NullReferenceException();
            }
            
            var presenter = await AssetsServiceUtility.Create<TPresenter>(data.Reference, _root);
            
            presenter.SetDerivedModel(model);

            Close();
            
            _currentPresenter = presenter;

            return presenter;
        }

        public void Close()
        {
            if (_currentPresenter == null)
                return;

            Object.Destroy(_currentPresenter.gameObject);
        }
    }
}