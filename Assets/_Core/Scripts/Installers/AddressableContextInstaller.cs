using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.Locators.Runtime.Installers;
using UnityEngine;
using Workspace.Services.Assets;

namespace Workspace.Installers
{
    [Serializable]
    public class AddressableContextInstaller : Installer
    {
        [SerializeField] private string _label;
        private AssetsService _assets;

        public override async Task InstallBindingsAsync(CancellationToken cancellationToken)
        {
            _assets = await ServiceLocator.GetAsync<AssetsService>(cancellationToken);
            await _assets.WarmUpAssetsByLabel(_label);
        }

        public override void UninstallBindings()
        {
            _assets.ReleaseAssetsByLabel(_label).Forget();
        }
    }
}