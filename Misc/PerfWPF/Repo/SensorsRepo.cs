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
       
        private int _updateInterval { get; set; }

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
                    _updateTimer.Change(0, _updateInterval);
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
                period: UpdateInterval
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
