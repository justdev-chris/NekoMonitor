# 🐱 NekoMonitor - Floating System Monitor

A lightweight, always-on-top system monitoring widget for your desktop. Keep an eye on your system resources with this cute, customizable floating monitor.

![NekoMonitor Screenshot](https://via.placeholder.com/400x200/1a1a1a/ff6a00?text=🐱+NekoMonitor)

## ✨ Features

### 📊 Real-time Monitoring
- **CPU Usage** - Live processor utilization
- **RAM Usage** - Memory consumption tracking  
- **Network Activity** - Upload/download speeds
- **Disk Usage** - Storage activity monitoring

### 🎯 Desktop Widget
- **Always on Top** - Stays visible over other windows
- **Drag & Drop** - Move anywhere on your screen
- **Transparent Background** - Clean, unobtrusive design
- **Compact Size** - Takes minimal screen space

### 🎮 Gamer Friendly
- **Low Resource Usage** - Lightweight and efficient
- **Game Mode** - Auto-hide during fullscreen applications
- **Performance Focused** - Won't impact your gaming

### 🎨 Customization
- **Dark/Light Themes** - Choose your preferred style
- **Color Schemes** - Customizable accent colors
- **Size Options** - Adjustable widget dimensions
- **Position Memory** - Remembers where you put it

## 🚀 Quick Start

### Download & Run
1. **Download** the latest release from the [Releases page]
2. **Extract** the ZIP file to any folder
3. **Run** `NekoMonitor.exe` - no installation required!
4. **Drag** the widget to your preferred screen position

### Build from Source
```bash
```
# Clone the repository
git clone https://github.com/yourusername/NekoMonitor.git
cd NekoMonitor

# Build the application
dotnet build

# Run locally
dotnet run

# Create standalone executable
dotnet publish -c Release -r win-x64 --self-contained true
🛠️ System Requirements
Windows 10/11

.NET 8.0 Runtime (Download here)

4GB RAM minimum

10MB disk space

📁 Project Structure
```
NekoMonitor/
├── 📁 .github/workflows/    # CI/CD automation
├── 📄 App.xaml             # Application entry point
├── 📄 MainWindow.xaml      # Floating widget UI
├── 📄 MainWindow.xaml.cs   # Widget logic & monitoring
├── 📄 NekoMonitor.csproj   # Project configuration
├── 📄 global.json          # .NET SDK version
└── 📄 README.md            # This file!
```
# 🎯 Use Cases

# 🎮 Gaming

Monitor system resources while gaming without alt-tabbing. Keep an eye on CPU/ RAM usage during intense sessions.

# 💻 Development
Watch resource usage while coding, compiling, or running virtual machines.

# 🎥 Content Creation
Monitor system load during video editing, streaming, or 3D rendering.

# 🔧 System Administration
Quick glance at server or workstation performance.

# 🔧 Technical Details

# Built With
-
.NET 8.0 - Modern, cross-platform framework
-
WPF (Windows Presentation Foundation) - Rich desktop UI
-
Performance Counters - Accurate system metrics
-
GitHub Actions - Automated builds and releases

# Architecture

MVVM Pattern - Clean separation of concerns
-
DispatcherTimer - Smooth, real-time updates
-
PerformanceCounter - Native Windows performance API
-
Transparent Overlay - Non-intrusive desktop integration

# 🤝 Contributing
We welcome contributions! Here's how you can help:

Fork the repository

Create a feature branch (git checkout -b feature/amazing-feature)

Commit your changes (git commit -m 'Add amazing feature')

Push to the branch (git push origin feature/amazing-feature)

Open a Pull Request

# Development Setup
```
bash
```
# Install .NET 8.0 SDK
# Clone the repository
# Open in Visual Studio or VS Code
# Build and run!

# 🐛 Troubleshooting

Common Issues
Widget not appearing? - Check if it's behind other windows

High CPU usage? - Update interval is configurable in settings

Missing .NET runtime? - Download from Microsoft's website

# Performance Tips
Place widget on secondary monitor for gaming

Reduce update interval for better performance

Use dark theme for OLED screen battery savings

📝 License
This project is licensed under the MIT License 

Inspired by various system monitoring tools

Made with 🐾 by Chris - Because every desktop needs a cute system monitor!
