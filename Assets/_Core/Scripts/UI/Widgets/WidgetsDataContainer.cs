using System;
using System.Collections.Generic;
using Workspace.Core.Common;
using Workspace.Core.Common.Data;
using Workspace.UI.Widgets.Abstractions;

namespace Workspace.UI.Widgets
{
    public class WidgetsDataContainer : IDataContainer<BaseWidget>
    {
        private readonly DataContainer<BaseWidget> _dataContainer = new();
        public Dictionary<Type, object> DataMap => _dataContainer.DataMap;
        public void Add<TElement, TData>(TData data) where TElement : BaseWidget => _dataContainer.Add<TElement, TData>(data);
        public bool TryGetData(Type type, out object data) => _dataContainer.TryGetData(type, out data);
        public void Remove<TElement>() where TElement : BaseWidget => _dataContainer.Remove<TElement>();
    }
}