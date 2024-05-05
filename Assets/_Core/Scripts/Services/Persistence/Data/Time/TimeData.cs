using System;

namespace Workspace.Services.Persistence.Data.Time
{
    [Serializable]
    public class TimeData
    {
        public string Value;

        public bool IsEmpty() => string.IsNullOrEmpty(Value);
    }
}