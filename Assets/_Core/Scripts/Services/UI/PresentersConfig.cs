using System;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Workspace.Core.MVP.Abstractions;

namespace Workspace.Services.UI
{
    [Serializable]
    public class PresentersConfig
    {
        [SerializeField] private PresenterData[] data;

        public PresenterData[] Data => data;
    }

    [Serializable]
    public class PresenterData
    {
        [Select(typeof(BasePresenter))] public SerializedType SerializedType;

        public AssetReference Reference;
    }
}