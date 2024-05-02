using System;

namespace Workspace.Core.Common.Timer
{
    public abstract class BaseTimer
    {
        public event Action OnStart;
        public event Action OnStop;
        public event Action<BaseTimer> OnStopWithSender;

        protected float InitialTime { get; set; }
        protected float Time { get; set; }
        public bool IsRunning { get; private set; }

        public float Progress => Time / InitialTime;


        protected BaseTimer(float value)
        {
            InitialTime = value;
            IsRunning = false;
        }

        public void Start()
        {
            Time = InitialTime;
            
            if (!IsRunning)
            {
                IsRunning = true;
                OnStart?.Invoke();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                OnStop?.Invoke();
                OnStopWithSender?.Invoke(this);
            }
        }

        public void Resume()
        {
            IsRunning = true;
        }

        public void Pause()
        {
            IsRunning = false;
        }

        public abstract void Tick(float deltaTime);
    }
}