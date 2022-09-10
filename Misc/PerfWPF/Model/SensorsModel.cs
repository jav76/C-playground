using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PerfWPF.Model
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
