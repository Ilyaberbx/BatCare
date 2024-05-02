using System;
using System.Collections.Generic;
using Workspace.UI.Widgets.Abstractions;

namespace Workspace.UI.Widgets
{
    public class WidgetsDataContainer 
    {
        public Dictionary<Type, object> DataMap { get; } = new();

        public void Add<TWidget, TData>(TData data) where TWidget : BaseWidget
        {
            DataMap.Add(typeof(TWidget), data);
        }
        
        public void Remove<TWidget>() where TWidget : BaseWidget
        {
            var type = typeof(TWidget);
            
            if (DataMap.ContainsKey(type))
            {
                DataMap.Remove(type);
            }
        }
    }
}