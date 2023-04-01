using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using PerfWPF.ViewModels;
using PerfWPF.Models;

namespace PerfWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = new SensorsViewModel();

            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 8;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            this.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Left;
            this.Top = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Top;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Get this window's handle
            IntPtr hwnd = new WindowInteropHelper(this).Handle;

            Transparency.makeTransparent(hwnd);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }


}
