using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Workspace.Infrastructure.Global.States;
using Workspace.Services.App.Abstractions;

namespace Workspace.Services.App.Implementations.Global
{
    [Serializable]
    public class GlobalService : AppService<GlobalState>
    {
        [SerializeField] private GlobalServiceSettings _settings;

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnPostInitializeAsync(cancellationToken);
            
            await ChangeState<MainMenuState>(CancellationToken.None);
        }

        public override Task InitializeState<TState>(TState state)
        {
            var group = _settings.GetSceneGroup<TState>();
            
            state.SetData(group);
            
            return Task.CompletedTask;
        }
    }
}