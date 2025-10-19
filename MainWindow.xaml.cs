using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;

namespace NekoMonitor
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer updateTimer;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private long lastBytesReceived = 0;
        private long lastBytesSent = 0;
        private DateTime lastUpdateTime = DateTime.Now;

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
                
                // Prime the counters (first read is always 0)
                cpuCounter.NextValue();
                ramCounter.NextValue();
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                // Fallback if performance counters fail
                CpuText.Text = "CPU: Error";
                RamText.Text = "RAM: Error";
            }
        }

        private void StartMonitoring()
        {
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(1.5); // Slower updates = more stable
            updateTimer.Tick += UpdateRealStats;
            updateTimer.Start();
        }

        private void UpdateRealStats(object sender, EventArgs e)
        {
            try
            {
                // CPU Usage
                if (cpuCounter != null)
                {
                    float cpuUsage = cpuCounter.NextValue();
                    CpuText.Text = $"CPU: {cpuUsage:F1}%";
                }

                // RAM Usage
                if (ramCounter != null)
                {
                    float ramUsage = ramCounter.NextValue();
                    RamText.Text = $"RAM: {ramUsage:F1}%";
                }

                // Network Usage
                UpdateNetworkStats();

                // Disk Usage (simplified)
                UpdateDiskStats();
            }
            catch (Exception ex)
            {
                // Silent fail - don't crash the app
                CpuText.Text = "CPU: --%";
                RamText.Text = "RAM: --%";
                NetText.Text = "NET: -- KB/s";
                DiskText.Text = "DISK: --%";
            }
        }

        private void UpdateNetworkStats()
        {
            try
            {
                long bytesReceived = 0;
                long bytesSent = 0;

                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus == OperationalStatus.Up)
                    {
                        bytesReceived += ni.GetIPv4Statistics().BytesReceived;
                        bytesSent += ni.GetIPv4Statistics().BytesSent;
                    }
                }

                DateTime now = DateTime.Now;
                double timeDiff = (now - lastUpdateTime).TotalSeconds;

                if (timeDiff > 0)
                {
                    double downloadSpeed = (bytesReceived - lastBytesReceived) / timeDiff / 1024;
                    double uploadSpeed = (bytesSent - lastBytesSent) / timeDiff / 1024;

                    NetText.Text = $"NET: ↓{downloadSpeed:F0} ↑{uploadSpeed:F0} KB/s";

                    lastBytesReceived = bytesReceived;
                    lastBytesSent = bytesSent;
                    lastUpdateTime = now;
                }
            }
            catch
            {
                NetText.Text = "NET: 0 KB/s";
            }
        }

        private void UpdateDiskStats()
        {
            try
            {
                var drive = new System.IO.DriveInfo("C");
                if (drive.IsReady)
                {
                    double totalSpace = drive.TotalSize;
                    double freeSpace = drive.AvailableFreeSpace;
                    double usedPercent = ((totalSpace - freeSpace) / totalSpace) * 100;
                    DiskText.Text = $"DISK: {usedPercent:F1}%";
                }
                else
                {
                    DiskText.Text = "DISK: --%";
                }
            }
            catch
            {
                DiskText.Text = "DISK: --%";
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {
                // Ignore drag errors
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            updateTimer?.Stop();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            updateTimer?.Stop();
            base.OnClosed(e);
        }
    }
}
