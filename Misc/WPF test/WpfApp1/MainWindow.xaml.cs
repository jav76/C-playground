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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void updateDelegate(string bin, string hex);
        public MainWindow()
        {
            InitializeComponent();
            Thread t = new Thread(countThread);
            t.Start();
        }

        void countThread()
        {
            int dec = 0;
            string binary = "";
            string hex = "";
            while (true)
            {
                binary = decToBin(dec);
                hex = decToHex(dec);
                dec++;
                Dispatcher.Invoke(
                    new updateDelegate(updateText),
                    binary, hex);
                Task.WaitAll(new Task[] { Task.Delay(500) });
            }
        }
        
        void updateText(string bin, string hex)
        {
            Binary.Content = bin;
            Hex.Content = hex;
        }
        string decToBin(int dec)
        {
            int remainder = 0;
            int quotient = dec;
            string binary = "";
            while (quotient > 0)
            {
                quotient = Math.DivRem(quotient, 2, out remainder);
                if (remainder == 1)
                {
                    binary = '1' + binary;
                }
                else
                {
                    binary = '0' + binary;
                }
            }
            return binary;
        }

        string decToHex(int dec)
        {
            int remainder = 0;
            int quotient = dec;
            string hex = "";
            char nextDigit = '\0';
            while (quotient > 0)
            {
                quotient = Math.DivRem(quotient, 16, out remainder);
                if (remainder >= 10)
                {
                    switch (remainder)
                    {
                        case 10:
                            nextDigit = 'A';
                            break;
                        case 11:
                            nextDigit = 'B';
                            break;
                        case 12:
                            nextDigit = 'C';
                            break;
                        case 13:
                            nextDigit = 'D';
                            break;
                        case 14:
                            nextDigit = 'E';
                            break;
                        case 15:
                            nextDigit = 'F';
                            break;
                    }
                }
                else
                {
                    // Need 48 UTF-16 character offset to align to 0-9
                    nextDigit = (char)(remainder + 48);
                }
                hex = nextDigit + hex;
            }
            return hex;
        }
    }
}
