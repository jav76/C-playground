using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections;

namespace PerfWPF.Models
{
    public class SensorsModel : INotifyPropertyChanged
    {
        public SensorsModel()
        {

        }


        private float _CPUClock;

        public float CPUClock
        {
            get { return _CPUClock; }
            set {
                _CPUClock = value;
                OnPropertyChanged(nameof(CPUClock));
            }
        }

        public void updateCPUChart(float newVal)
        {

        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
