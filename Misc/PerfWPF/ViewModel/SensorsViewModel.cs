using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitor.Software;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Input;

using PerfWPF.Model;

namespace PerfWPF.ViewModel
{
    internal class SensorsViewModel :IDisposable
    {
        private bool disposedValue;
        private Computer _computer;
        private Thread _updateThread;
        private List<ISensor> _CPUSensors;
        private List<ISensor> _GPUSensors;
        private List<ISensor> _memorySensors;
        private List<IHardware> _networkDevices;
        private List<IHardware> _storageDevices;

        private SensorsModel _Sensors;

        public SensorsModel Sensors { 
            get { return _Sensors; }
        }


        public SensorsViewModel()
        {
            _Sensors = new SensorsModel();

            _computer = new Computer()
            {
                IsCpuEnabled = true,//
                IsGpuEnabled = true,//
                IsMemoryEnabled = true, //
                IsMotherboardEnabled = false,
                IsControllerEnabled = false,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };

            _computer.Open();
            _computer.Accept(new UpdateVisitor());

            foreach (IHardware rootHardware in _computer.Hardware)
            {
                switch (rootHardware.HardwareType)
                {
                    case HardwareType.Cpu:
                    {
                        _CPUSensors = GetSensors(rootHardware);
                        break;
                    }
                    case HardwareType.GpuNvidia:
                    {
                        _GPUSensors = GetSensors(rootHardware);
                        break;
                    }
                    case HardwareType.Memory:
                    {
                        _memorySensors = GetSensors(rootHardware);
                        break;
                    }
                }
            }

            PrintSensors();

            _updateThread = new Thread(new ThreadStart(UpdateThread));
            System.Diagnostics.Debug.WriteLine("test");

            _updateThread.Start();
        }
        private void UpdateThread()
        {
            while (true)
            {
                foreach (ISensor sensor in _CPUSensors)
                {
                    if (sensor.SensorType == SensorType.Clock)
                    {
                        sensor.Hardware.Update();
                        Debug.WriteLine(sensor.Name);
                        Debug.WriteLine(sensor.Value);
                        _Sensors.CPUClock = sensor.Value.HasValue ? sensor.Value.Value : 0;
                        break;
                    }
                }
                //PrintSensors();
                Thread.Sleep(500);
            }
            
        }

        private List<ISensor> GetSensors(IHardware root)
        {
            List<ISensor> sensorList = new List<ISensor>();
            foreach (ISensor sensor in root.Sensors)
            {
                sensorList.Add(sensor);
            }
            return sensorList;
        }

        private void PrintSensors()
        {


            foreach (ISensor sensor in _CPUSensors)
            {
                System.Diagnostics.Debug.WriteLine("\t\tSensor: {0}, Identifier: {1}, value: {2}", sensor.Name, sensor.Identifier, sensor.Value);
            }
            foreach (ISensor sensor in _GPUSensors)
            {
                System.Diagnostics.Debug.WriteLine("\t\tSensor: {0}, Identifier: {1}, value: {2}", sensor.Name, sensor.Identifier, sensor.Value);
            }
            foreach (ISensor sensor in _memorySensors)
            {
                System.Diagnostics.Debug.WriteLine("\t\tSensor: {0}, Identifier: {1}, value: {2}", sensor.Name, sensor.Identifier, sensor.Value);
            }
            /*
            foreach (IHardware hardware in _computer.Hardware)
            {
                hardware.Update();
                System.Diagnostics.Debug.WriteLine("Hardware: {0}", hardware.Name);

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    subhardware.Update();
                    System.Diagnostics.Debug.WriteLine("\tSubhardware: {0}", subhardware.Name);

                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        if (sensor.Values.Count() > 0)
                        {
                            System.Diagnostics.Debug.WriteLine("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Values);
                        }
                        if (sensor.Hardware.Sensors.Count() > 0)
                        {
                            foreach (ISensor sensor1 in sensor.Hardware.Sensors)
                            {
                                System.Diagnostics.Debug.WriteLine("\t\tSensor: {0}, value: {1}", sensor1.Name, sensor1.Values);
                            }

                        }
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    System.Diagnostics.Debug.WriteLine("\tSensor: {0} | {1}", sensor.Name, sensor.Value);
                }
            }
            */
        }

        public class UpdateVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }


                _computer.Close();
                disposedValue = true;
            }
        }
        ~SensorsViewModel()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
