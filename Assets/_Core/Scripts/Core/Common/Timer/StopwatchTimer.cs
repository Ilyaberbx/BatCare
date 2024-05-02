namespace Workspace.Core.Common.Timer
{
    public class StopwatchTimer : BaseTimer
    {
        public StopwatchTimer() : base(0)
        { }

        public override void Tick(float deltaTime)
        {
            if (IsRunning)
            {
                Time += deltaTime;
            }
        }

        public void Reset() => Time = 0;

        public float GetTime() => Time;
    }
}