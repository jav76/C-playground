using System.Collections.Generic;
using LiveChartsCore;
using LiveChartsCore.Measure;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

using PerfWPF.Models;

namespace PerfWPF.ViewModels
{
    [ObservableObject]
    public partial class CPUViewModel
    {
        public CPUViewModel()
        {
            CPUModel = new CPUModel();
        }

        public CPUModel CPUModel { get; set; }
    }
}
