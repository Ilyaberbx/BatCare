namespace Workspace.Core.Common.Timer
{
    public class CountdownTimer : BaseTimer
    {
        public CountdownTimer(float value) : base(value) { }

        public bool IsFinished => Time <= 0;

        public override void Tick(float deltaTime) {
            if (IsRunning && Time > 0) {
                Time -= deltaTime;
            }
            
            if (IsRunning && Time <= 0) {
                Stop();
            }
        }

        public void Reset() => Time = InitialTime;

        public void Reset(float newTime) {
            InitialTime = newTime;
            Reset();
        }
    }
}