using System;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime;
using UnityEngine;
using Workspace.Services.Persistence;

namespace Workspace.Core.Systems.Room.Interior.Abstractions
{
    public abstract class BaseInteriorElement : MonoBehaviour
    {
        protected object DerivedData { get; private set; }

        public virtual void SetDerivedData(object value)
        {
            DerivedData = value;
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }

        public abstract void Rebuild();
    }

    public abstract class BaseInteriorElement<TData> : BaseInteriorElement
    {
        private UserService _userService;
        protected int RoomIndex => UserService.CurrentRoomIndexProperty.Value;
        protected TData Data { get; private set; }

        protected UserService UserService
        {
            get
            {
                if (_userService != null)
                {
                    return _userService;
                }

                return _userService = ServiceLocator.Get<UserService>();
            }
        }

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
    }
}