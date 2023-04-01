using System.Collections.Generic;
using System.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Measure;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Defaults;
using System;

using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PerfWPF.Models
{
    [ObservableObject]
    public partial class CPUModel : UserControl
    {
        public CPUModel()
        {
            
            _frequency = new ObservableValue(500);
            _series = new GaugeBuilder().WithInnerRadius(50).AddValue(_frequency).BuildSeries();

        }
        private IEnumerable<ISeries> _series { get; set; }
        public IEnumerable<ISeries> Series { get { return _series; } }


        private ObservableValue _frequency;

        public double Frequency { set => _frequency.Value = value; }


        public void UpdateFreq(double newFreq)
        {
            Frequency = newFreq;
        }

        //#region INotifyPropertyChanged Members

        //public event PropertyChangedEventHandler? PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
    }
}
