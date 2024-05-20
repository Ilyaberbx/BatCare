using System;
using System.Collections.Generic;

namespace Workspace.Core.Common.Data
{
    public interface IDataContainer
    {
        
    }
    public interface IDataContainer<in TDerivedElement> : IDataContainer
    {
        Dictionary<Type, object> DataMap { get; }
        void Add<TElement, TData>(TData data) where TElement : TDerivedElement;
        void Add<TData>(Type type, TData data);
        bool TryGetData(Type type, out object data);
        void Remove<TElement>() where TElement : TDerivedElement;
    }
}