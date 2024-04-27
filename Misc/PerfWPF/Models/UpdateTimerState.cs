using System;

namespace PerfWPF.Models
{
    internal class UpdateTimerState
    {
        // Accessibility of these is weird. Really we shouldn't be exposing access to Counter, IsExecuting, lastExecutionDurationMS, and lastExecutionTime
        public int Counter = 0;
        public bool IsExecuting;
        public bool IsPaused = false;
        public long lastExecutionDurationMS = 0;
        public DateTime lastExecutionTime = DateTime.MinValue;
    }
}
