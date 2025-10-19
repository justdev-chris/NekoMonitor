using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace NekoMonitor
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer updateTimer;
        private bool isDragging = false;

        public MainWindow()
        {
            InitializeComponent();
            StartSafeMonitoring();
        }

        private void StartSafeMonitoring()
        {
            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(2); // Slower updates = more stable
            updateTimer.Tick += UpdateStatsSafely;
            updateTimer.Start();
        }

        private void UpdateStatsSafely(object sender, EventArgs e)
        {
            try
            {
                // Simple random demo data instead of PerformanceCounter
                var random = new Random();
                CpuText.Text = $"CPU: {random.Next(1, 100)}%";
                RamText.Text = $"RAM: {random.Next(1, 100)}%";
                NetText.Text = $"NET: {random.Next(0, 1000)} KB/s";
                DiskText.Text = $"DISK: {random.Next(1, 100)}%";
            }
            catch
            {
                // Silent fail - don't crash
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
