



using LibreHardwareMonitor.Hardware;
using System.Diagnostics;
using System.Threading;
using System.Security;

namespace PerfTest
{
    class Program
    {

        static void Main(string[] args)
        {
            IHardware CPU;
            Dictionary<String, ISensor> CPUSensors = new Dictionary<String, ISensor>();
            String[] UsedSensorNames = { "CPU Total", "" };

            IHardware GPU;

            Monitor();

            PerformanceCounterCategory[] categories;
            categories = PerformanceCounterCategory.GetCategories();
            foreach (PerformanceCounterCategory category in categories)
            {
                System.Console.WriteLine(category.CategoryName);
            }
        }

        public Dictionary<String, ISensor> getSensors(IHardware Hardware, String[] SensorNames)
        {
            Dictionary<String, ISensor> Sensors = new Dictionary<String, ISensor>();
            foreach (ISensor Sensor in Hardware.Sensors)
            {
                foreach (String SensorName in SensorNames)
                {
                    if (Sensor.Name == SensorName)
                    {
                        Sensors.Add(SensorName, Sensor);
                    }
                }
            }
            return Sensors;
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

        static public void Monitor()
        {
            Computer computer = new Computer()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };

            computer.Open();
            computer.Accept(new UpdateVisitor());

            foreach (IHardware hardware in computer.Hardware)
            {
                Console.WriteLine("Hardware: {0}", hardware.Name);

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    Console.WriteLine("\tSubhardware: {0}", subhardware.Name);

                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        Console.WriteLine("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Values);
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    Console.WriteLine("\tSensor: {0} | {1}", sensor.Name, sensor.Value);
                }
            }

            computer.Close();
        }
    }
}