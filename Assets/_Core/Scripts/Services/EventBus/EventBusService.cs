using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;
using Workspace.Services.EventBus.Handlers;

namespace Workspace.Services.EventBus
{
    public class EventBusService : PocoService
    {
        private readonly Dictionary<Type, List<IGlobalSubscriber>> _subscribes = new();
        
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void CleanUp()
            => _subscribes.Clear();

        public void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = GetSubscriberTypes(subscriber);

            foreach (Type type in subscriberTypes)
            {
                if (!_subscribes.ContainsKey(type))
                    _subscribes[type] = new List<IGlobalSubscriber>();

                _subscribes[type].Add(subscriber);

                _subscribes[type] = _subscribes[type].OrderByDescending(x => x.Priority).ToList();
            }
        }

        public void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, IGlobalSubscriber
        {
            if (_subscribes.TryGetValue(typeof(TSubscriber), out List<IGlobalSubscriber> subscribers))
            {
                foreach (IGlobalSubscriber subscriber in subscribers.ToList())
                    action.Invoke(subscriber as TSubscriber);
            }
            else
                Debug.Log("Can't raise events of type: " + nameof(TSubscriber));
        }

        public void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = GetSubscriberTypes(subscriber);

            foreach (Type t in subscriberTypes.Where(t => _subscribes.ContainsKey(t)))
                _subscribes[t].Remove(subscriber);
        }

        private List<Type> GetSubscriberTypes(IGlobalSubscriber subscriber)
        {
            Type type = subscriber.GetType();
            List<Type> subscriberTypes = type
                .GetInterfaces()
                .Where(it => typeof(IGlobalSubscriber).IsAssignableFrom(it))
                .ToList();
            return subscriberTypes;
        }
    }
}