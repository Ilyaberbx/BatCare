using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;

namespace Workspace.Services.Pause
{
    internal class PauseSystem : IPauseSystem
    {
        private readonly List<IPauseListener> _pauseListeners;
        public bool IsPaused { get; private set; }

        public PauseSystem()
        {
            _pauseListeners = new List<IPauseListener>();
        }
        
        public void Subscribe(IPauseListener listener)
        {
            _pauseListeners.Add(listener);
        }

        public void Unsubscribe(IPauseListener listener)
        {
            if (_pauseListeners.IsEmpty())
                return;

            if (_pauseListeners.Contains(listener))
            {
                _pauseListeners.Remove(listener);
            }
        }
        
        public void Pause(bool value)
        {
            IsPaused = value;

            foreach (var listener in _pauseListeners)
            {
                listener.OnPaused(value);
            }
        }
        
    }
}