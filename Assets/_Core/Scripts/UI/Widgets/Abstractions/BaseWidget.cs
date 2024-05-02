using System;
using Better.Commons.Runtime.Components.UI;
using Better.Commons.Runtime.Utility;

namespace Workspace.UI.Widgets.Abstractions
{
    public class BaseWidget : UIMonoBehaviour
    {
        protected object DerivedData { get; private set; }

        public virtual void SetDerivedData(object value)
        {
            DerivedData = value;
        }
    }

    public class BaseWidget<TData> : BaseWidget
    {
        protected TData Data { get; private set; }

        public sealed override void SetDerivedData(object value)
        {
            base.SetDerivedData(value);
            
            if (value is TData data)
            {
                Data = data;
            }
            else
                DebugUtility.LogException<InvalidCastException>();
        }
    }
}