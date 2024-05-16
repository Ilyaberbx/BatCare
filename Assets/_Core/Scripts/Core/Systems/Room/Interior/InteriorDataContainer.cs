using System;
using System.Collections.Generic;
using Workspace.Core.Common;
using Workspace.Core.Systems.Room.Interior.Abstractions;

namespace Workspace.Core.Systems.Room.Interior
{
    public class InteriorDataContainer : IDataContainer<BaseInteriorElement>
    {
        private readonly DataContainer<BaseInteriorElement> _dataContainer = new();
        public Dictionary<Type, object> DataMap => _dataContainer.DataMap;
        public void Add<TElement, TData>(TData data) where TElement : BaseInteriorElement => _dataContainer.Add<TElement, TData>(data);
        public bool TryGetData(Type type, out object data) => _dataContainer.TryGetData(type, out data);
        public void Remove<TElement>() where TElement : BaseInteriorElement => _dataContainer.Remove<TElement>();
    }
}