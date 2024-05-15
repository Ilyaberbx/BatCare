using System;
using Better.Commons.Runtime.Utility;
using UnityEngine;

namespace Workspace.Core.Systems.Room.Interior.Abstractions
{
    public abstract class BaseInteriorElement : MonoBehaviour
    {
        protected object DerivedData { get; private set; }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
        public virtual void SetDerivedData(object value)
        {
            DerivedData = value;
        }
    }
    
    public abstract class BaseInteriorElement<TData> : BaseInteriorElement
    {
        protected TData Data { get; private set; }

        public sealed override void SetDerivedData(object value)
        {
            base.SetDerivedData(value);
            
            if (value is TData data)
            {
                Data = data;

                Rebuild();
            }
            else
                DebugUtility.LogException<InvalidCastException>();
        }

        protected abstract void Rebuild();
    }
}