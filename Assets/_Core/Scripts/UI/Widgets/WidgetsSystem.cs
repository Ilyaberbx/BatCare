 using System;
using UnityEngine;
using Workspace.Core.Systems;
using Workspace.UI.Widgets.Abstractions;

namespace Workspace.UI.Widgets
{
    public class WidgetsSystem : MonoBehaviour, ISystem
    {
        [SerializeField] private BaseWidget[] _widgets;

        public void SetData(WidgetsDataContainer dataContainer)
        {
            var dataMap = dataContainer.DataMap;

            foreach (var widget in _widgets)
            {
                var type = widget.GetType();

                if (dataMap.TryGetValue(type, out var data))
                {
                    widget.SetDerivedData(data);
                }
                else
                {
                    var message = $"Data {data.GetType()} does not fit widget {widget.GetType()}";
                    
                    throw new InvalidCastException(message);
                }
            }
        }
    }
}