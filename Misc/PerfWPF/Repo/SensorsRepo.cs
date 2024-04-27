using LibreHardwareMonitor.Hardware;
using PerfWPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PerfWPF.Repo
{
    internal class SensorsRepo
    {
        private Computer _computer { get; }
        private Timer _updateTimer { get; }
        private UpdateTimerState _updateTimerState { get; }
       
        private const int UPDATE_INTERVAL_DEFAULT = 1000;
        private int _updateInterval { get; set; } = UPDATE_INTERVAL_DEFAULT;

        private IDictionary<SensorType, IEnumerable<ISensor>> _mappedSensors { get; } = new Dictionary<SensorType, IEnumerable<ISensor>>();

        public IDictionary<SensorType, IEnumerable<ISensor>> MappedSensors
        {
            get { return _mappedSensors; }
        }

        public int UpdateInterval
        {
            get
            {
                return _updateInterval;
            }
            set
            {
                if (!(_updateTimer is null))
                {
                    _updateInterval = value;
                    if (!_updateTimer.Change(0, _updateInterval))
                    {
                        throw new FieldAccessException($"Failed to change timer '{nameof(_updateTimer)}' update interval");
                    }
                }
            }
        }

        #region Init

        public SensorsRepo()
        {
            _updateTimerState = new UpdateTimerState();
            _updateTimer = new Timer
            (
                callback: new TimerCallback(UpdateTimerState.UpdateTimerCallback),
                state: _updateTimerState,
                dueTime: 0,
                period: _updateInterval
            );

            _updateTimerState.IsPaused = false;
            _computer = new Computer()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = false,
                IsControllerEnabled = false,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };

            _computer.Open();
            _computer.Accept(new UpdateVisitor());

            IEnumerable<ISensor> totalSensors = GetSensors(_computer.Hardware, new List<ISensor>());

            foreach (SensorType type in Enum.GetValues(typeof(SensorType)).Cast<SensorType>())
            {
                _mappedSensors.TryAdd(type, totalSensors.Where(s => s.SensorType == type));
            }

#if DEBUG
            PrintSensors();
#endif

        }

        private IEnumerable<ISensor> GetSensors(IHardware rootHardware, List<ISensor> parentHardwareSensors)
        {
            foreach (ISensor sensor in rootHardware.Sensors)
            {
                if (!parentHardwareSensors.Any(s => s.Identifier.ToString() == sensor.Identifier.ToString()))
                {
                    parentHardwareSensors.Add(sensor);
                }
            }

            // Recurse into sub hardware
            foreach (IHardware subHardware in rootHardware.SubHardware)
            {
                GetSensors(subHardware, parentHardwareSensors);
            }

            return parentHardwareSensors;
        }

        private IEnumerable<ISensor> GetSensors(IEnumerable<IHardware> hardwareCollection, List<ISensor> cumulativeSensors)
        {
            foreach (IHardware hardware in hardwareCollection)
            {
                cumulativeSensors = GetSensors(hardware, cumulativeSensors).ToList();
            }

            return cumulativeSensors;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Pause the update timer
        /// </summary>
        public void Pause()
        {
            _updateTimerState.IsPaused = true;
        }

        /// <summary>
        /// Resume the update timer
        /// </summary>
        public void Resume()
        {
            _updateTimerState.IsPaused = false;
        }

        /// <summary>
        /// Immediately run the task to update the SensorsRepo data
        /// </summary>
        public void Update()
        {
            // This will force a tick on the timer
            UpdateInterval = UpdateInterval;
        }

        /// <summary>
        /// Update the interval of the timer in milliseconds to the specified value
        /// </summary>
        /// <param name="interval"></param>
        public void Update(int interval)
        {
            UpdateInterval = interval;
        }
        
        /// <summary>
        /// Clear all data in the SensorsRepo
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        //public void Clear(ISensorsRepoData data)
        //{
        //    if (typeof(data) is SomeSensorDataClass)
        //    {
        //        _sensorDataClass.Clear();
        //    }
        //    ...
        //    etc
        //    (Can also probably make this a switch)
        //}

        #endregion

        #region DEBUG
#if DEBUG // Debugging helpers

        public void PrintSensors()
        {
            foreach (KeyValuePair<SensorType, IEnumerable<ISensor>> mappedSensor in _mappedSensors)
            {
                Debug.WriteLine($"{mappedSensor.Key} Sensors:");

                foreach (ISensor sensor in mappedSensor.Value)
                {
                    Debug.WriteLine($"\t{sensor.Name} - {sensor.Value}");
                }
            }
        }

#endif
        #endregion

    }

}
