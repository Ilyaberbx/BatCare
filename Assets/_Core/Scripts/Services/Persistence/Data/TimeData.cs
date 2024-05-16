using System;

namespace Workspace.Services.Persistence.Data
{
    [Serializable]
    public class TimeData
    {
        public string Value;

        public bool IsEmpty() => string.IsNullOrEmpty(Value);
    }
}