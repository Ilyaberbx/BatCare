using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime.Interfaces;

namespace Workspace.Core.Actions.Abstractions
{
    [Serializable]
    public abstract class BaseAction : IAction
    {
        public virtual void Initialize()
        { }

        public abstract Task Execute(CancellationToken token);
    }


    [Serializable]
    public abstract class BaseAction<T> : BaseAction where T : IService
    {
        protected T Reference { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            Reference = ServiceLocator.Get<T>();
        }
    }
}