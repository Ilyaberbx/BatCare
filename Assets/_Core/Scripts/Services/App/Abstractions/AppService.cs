using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Services.Runtime;
using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules;
using Workspace.Infrastructure;

namespace Workspace.Services.App.Abstractions
{
    [Serializable]
    public abstract class AppService<TBaseState> : PocoService
        where TBaseState : InfrastructureState
    {
        private StateMachine<TBaseState> _stateMachine;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _stateMachine = new StateMachine<TBaseState>();
            _stateMachine.AddModule<StatesCacheModule<TBaseState>>();
            _stateMachine.Run();

            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        public async Task ChangeState<TState>(CancellationToken cancellationToken)
            where TState : TBaseState, new()
        {
            var module = _stateMachine.GetModule<StatesCacheModule<TBaseState>>();

            var state = module.GetOrAdd<TState>();

            await InitializeState(state);
            
            await _stateMachine.ChangeStateAsync(state, cancellationToken);
        }

        public void ChangeState<TState>() where TState : TBaseState, new()
        {
            ChangeState<TState>(CancellationToken.None).Forget();
        }

        public abstract Task InitializeState<TState>(TState state)
            where TState : TBaseState, new();

    }
}