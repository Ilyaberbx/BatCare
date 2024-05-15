using System;
using System.Collections.Generic;
using Workspace.Core.Systems.Room.Interior.Abstractions;

namespace Workspace.Core.Systems.Room.Interior
{
    public class InteriorDataContainer
    {
        public Dictionary<Type, object> DataMap { get; } = new();

        public void Add<TElement, TData>(TData data) where TElement : BaseInteriorElement
        {
            DataMap.Add(typeof(TElement), data);
        }

        public bool TryGetData(Type type, out object data)
        {
            return DataMap.TryGetValue(type, out data);
        }

        public void Remove<TElement>() where TElement : BaseInteriorElement
        {
            var type = typeof(TElement);

            if (DataMap.ContainsKey(type))
            {
                DataMap.Remove(type);
            }
        }
    }
}