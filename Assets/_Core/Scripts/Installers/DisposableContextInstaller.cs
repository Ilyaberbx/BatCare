using System;
using Better.Attributes.Runtime.Select;
using Better.Locators.Runtime.Installers;
using UnityEngine;
using Workspace.Installers.Services;

namespace Workspace.Installers
{
    [Serializable]
    public class DisposableContextInstaller : ServicesInstaller<DisposablePocoService>
    {
        [SerializeReference, Select] private DisposablePocoService[] _services;

        protected override DisposablePocoService[] Services => _services;
        
        public override void UninstallBindings()
        {
            base.UninstallBindings();
            
            foreach (var service in _services)
            {
                service.Dispose();
            }
        }
    }
}