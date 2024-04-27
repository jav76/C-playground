using System;
using System.Diagnostics;

namespace PerfWPF.Models
{
    internal class UpdateTimerState
    {
        private int _counter = 0;
        private bool _isExecuting;
        private long _lastExecutionDurationMS = 0;
        private DateTime _lastExecutionTime = DateTime.MinValue;

        public int Counter { get => _counter; }
        public bool IsExecuting { get => _isExecuting; }
        public bool IsPaused = false;
        public long lastExecutionDurationMS { get => _lastExecutionDurationMS; }
        public DateTime lastExecutionTime { get => _lastExecutionTime; }

        #region UpdateTimer

        internal static void UpdateTimerCallback(object timerState)
        {
            if (timerState is UpdateTimerState)
            {
                UpdateTimerState state = (UpdateTimerState)timerState;

                if (state.IsPaused)
                {
                    return;
                }

                lock (state)
                {
                    state._counter++;
                    state._lastExecutionTime = DateTime.Now;
                    state._isExecuting = true;
                }
                Stopwatch updateTimerWatch = new Stopwatch();
                updateTimerWatch.Start();


#if DEBUG
                Debug.WriteLine($"Timer Callback - Counter: {state.Counter} LastExecutionTime: {state.lastExecutionTime} lastExecutionDurationMS: {state.lastExecutionDurationMS}");
#endif

                // Do update work here


                updateTimerWatch.Stop();
                lock (state)
                {
                    state._lastExecutionDurationMS = updateTimerWatch.ElapsedMilliseconds;
                    state._isExecuting = false;
                }
            }
            else
            {
                throw new InvalidOperationException($"Invalid timer state object - Expected '{nameof(UpdateTimerState)}'");
            }
        }

        #endregion
    }
}
