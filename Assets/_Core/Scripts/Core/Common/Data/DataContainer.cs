using System;
using System.Collections.Generic;

namespace Workspace.Core.Common.Data
{
    public class DataContainer<TDerivedElement> : IDataContainer<TDerivedElement>
    {
        public Dictionary<Type, object> DataMap { get; }

        public DataContainer(Dictionary<Type, object> dataMap)
        {
            DataMap = dataMap;
        }
        
        public DataContainer()
        {
            DataMap = new();
        }

        public void Add<TElement, TData>(TData data) where TElement : TDerivedElement
        {
            Add(typeof(TElement), data);
        }
        
        public void Add<TData>(Type type, TData data)
        {
            DataMap.Add(type, data);
        }

        public bool TryGetData(Type type, out object data)
        {
            return DataMap.TryGetValue(type, out data);
        }

        public void Remove<TElement>() where TElement : TDerivedElement
        {
            var type = typeof(TElement);

            if (DataMap.ContainsKey(type))
            {
                DataMap.Remove(type);
            }
        }
    }
}