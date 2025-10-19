using System;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Threading;

namespace NekoMonitor
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer updateTimer;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public MainWindow()
        {
            InitializeComponent();
            InitializePerformanceCounters();
            StartMonitoring();
        }

        private void InitializePerformanceCounters()
        {
            try
            {
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            }
            catch (Exception ex)
            {
                // Fallback if performance counters aren't available
                CpuText.Text = "CPU: N/A";
                RamText.Text = "RAM: N/A";
            }
        }

        private void StartMonitoring()
        {
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(1);
            updateTimer.Tick += UpdateStats;
            updateTimer.Start();
        }

        private void UpdateStats(object sender, EventArgs e)
        {
            try
            {
                if (cpuCounter != null)
                {
                    float cpuUsage = cpuCounter.NextValue();
                    CpuText.Text = $"CPU: {cpuUsage:F1}%";
                }

                if (ramCounter != null)
                {
                    float ramUsage = ramCounter.NextValue();
                    RamText.Text = $"RAM: {ramUsage:F1}%";
                }

                // Simple network and disk placeholders
                NetText.Text = "NET: 0 KB/s";
                DiskText.Text = "DISK: 0%";
            }
            catch
            {
                // Silent fail - keep showing last values
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
