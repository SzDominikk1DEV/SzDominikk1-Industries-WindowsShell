# =============================
# SzDominikk1 Industries | WindowsShell (clean-room clone)
# WPF/.NET 8 — GPLv3
# =============================
#
# This is an original, clean-room implementation inspired by the feature set you described (debloat, app removal,
# package store via Chocolatey, privacy tweaks, reboot to UEFI). It does NOT copy code from XtremeShell.
# The entire project is licensed under GPLv3 to stay compatible with the original spirit.
#
# Project structure below. Create files with the exact paths and contents.
#
# ├─ WindowsShell.sln
# └─ WindowsShell
#    ├─ WindowsShell.csproj
#    ├─ app.manifest
#    ├─ App.xaml
#    ├─ App.xaml.cs
#    ├─ MainWindow.xaml
#    ├─ MainWindow.xaml.cs
#    ├─ Models
#    │  └─ AppEntry.cs
#    └─ Utilities
#       ├─ PowerShellHelper.cs
#       └─ RegistryUninstallHelper.cs
#
# Build: .NET 8 SDK, Windows only. Run: F5 in Visual Studio 2022+ or `dotnet run -p WindowsShell`.
# Requires admin. Chocolatey feature needs choco installed (we can bootstrap).


// =============================
// File: WindowsShell.sln
// =============================
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31912.275
MinimumVisualStudioVersion = 10.0.40219.1
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "WindowsShell", "WindowsShell\WindowsShell.csproj", "{4E79B5EC-1A8F-4B18-9B17-6F8E2F78F0B1}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{4E79B5EC-1A8F-4B18-9B17-6F8E2F78F0B1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{4E79B5EC-1A8F-4B18-9B17-6F8E2F78F0B1}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{4E79B5EC-1A8F-4B18-9B17-6F8E2F78F0B1}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{4E79B5EC-1A8F-4B18-9B17-6F8E2F78F0B1}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal


// =============================
// File: WindowsShell/WindowsShell.csproj
// =============================
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
    <UseWPF>true</UseWPF>
    <AssemblyName>SzDominikk1 Industries | WindowsShell</AssemblyName>
    <RootNamespace>WindowsShell</RootNamespace>
    <ApplicationManifest>app.manifest</ApplicationManifest>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="System.Management.Automation" Version="7.3.8" />
  </ItemGroup>
</Project>


// =============================
// File: WindowsShell/app.manifest (requires admin)
// =============================
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity version="1.0.0.0" name="WindowsShell.app"/>
  <trustInfo xmlns="urn:schemas-microsoft-com:asm.v3">
    <security>
      <requestedPrivileges>
        <requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
      </requestedPrivileges>
    </security>
  </trustInfo>
  <compatibility xmlns="urn:schemas-microsoft-com:compatibility.v1">
    <application>
      <supportedOS Id="{8e0f7a12-bfb3-4fe8-b9a5-48fd50a15a9a}"/>
    </application>
  </compatibility>
</assembly>


// =============================
// File: WindowsShell/App.xaml
// =============================
<Application x:Class="WindowsShell.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
  <Application.Resources>
    <ResourceDictionary>
      <SolidColorBrush x:Key="Accent" Color="#6EE7B7"/>
      <SolidColorBrush x:Key="Bg" Color="#0B1220"/>
      <SolidColorBrush x:Key="Card" Color="#111827"/>
      <Style TargetType="Button">
        <Setter Property="Margin" Value="4"/>
        <Setter Property="Padding" Value="10,6"/>
        <Setter Property="Background" Value="{StaticResource Accent}"/>
        <Setter Property="Foreground" Value="Black"/>
        <Setter Property="FontWeight" Value="SemiBold"/>
        <Setter Property="BorderThickness" Value="0"/>
        <Setter Property="Cursor" Value="Hand"/>
      </Style>
      <Style TargetType="TabControl">
        <Setter Property="Background" Value="{StaticResource Bg}"/>
      </Style>
      <Style TargetType="Window">
        <Setter Property="Background" Value="{StaticResource Bg}"/>
        <Setter Property="Foreground" Value="White"/>
        <Setter Property="FontFamily" Value="Segoe UI"/>
      </Style>
    </ResourceDictionary>
  </Application.Resources>
</Application>


// =============================
// File: WindowsShell/App.xaml.cs
// =============================
using System.Windows;
namespace WindowsShell
{
    public partial class App : Application { }
}


// =============================
// File: WindowsShell/MainWindow.xaml
// =============================
<Window x:Class="WindowsShell.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="SzDominikk1 Industries | WindowsShell"
        Height="720" Width="1080">
  <DockPanel>
    <StackPanel DockPanel.Dock="Top" Orientation="Horizontal" Background="#0F172A" Padding="12">
      <TextBlock Text="WindowsShell" FontSize="20" FontWeight="Bold"/>
      <TextBlock Text="  — the ultimate Windows customizer" Opacity="0.8" Margin="4,0,0,0"/>
      <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
        <Button Content="GitHub" Click="OpenGitHub"/>
        <Button Content="Discord" Click="OpenDiscord"/>
      </StackPanel>
    </StackPanel>

    <TabControl>
      <TabItem Header="Home">
        <Grid Margin="12">
          <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
          </Grid.RowDefinitions>
          <StackPanel Orientation="Horizontal">
            <Button Content="Reboot to UEFI" Click="RebootToUefi_Click"/>
            <Button Content="Create Restore Point" Click="CreateRestorePoint_Click"/>
            <Button Content="Install Chocolatey" Click="InstallChoco_Click"/>
          </StackPanel>
          <TextBox Grid.Row="1" x:Name="LogBox" Margin="0,10,0,0" AcceptsReturn="True" VerticalScrollBarVisibility="Auto"/>
        </Grid>
      </TabItem>

      <TabItem Header="Remove Apps">
        <Grid Margin="12">
          <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"/>
            <ColumnDefinition Width="*"/>
          </Grid.ColumnDefinitions>
          <StackPanel Grid.Column="0">
            <TextBlock Text="Microsoft Store / Appx" FontWeight="Bold"/>
            <Button Content="Refresh" Click="RefreshAppx_Click"/>
            <ListBox x:Name="AppxList" Height="480" />
            <Button Content="Uninstall Selected Appx" Click="UninstallAppx_Click"/>
          </StackPanel>
          <StackPanel Grid.Column="1">
            <TextBlock Text="Win32 Programs" FontWeight="Bold"/>
            <Button Content="Refresh" Click="RefreshWin32_Click"/>
            <ListBox x:Name="Win32List" Height="480" DisplayMemberPath="Display" />
            <Button Content="Uninstall Selected Win32" Click="UninstallWin32_Click"/>
          </StackPanel>
        </Grid>
      </TabItem>

      <TabItem Header="Package Store (Chocolatey)">
        <Grid Margin="12">
          <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
          </Grid.RowDefinitions>
          <StackPanel Orientation="Horizontal">
            <TextBox x:Name="ChocoSearchBox" Width="320" PlaceholderText="Search packages (prefix ! for exact)"/>
            <Button Content="Search" Click="ChocoSearch_Click"/>
            <Button Content="Install Selected" Click="ChocoInstall_Click"/>
          </StackPanel>
          <ListBox x:Name="ChocoList" Grid.Row="1" />
        </Grid>
      </TabItem>

      <TabItem Header="Privacy & Tweaks">
        <StackPanel Margin="12">
          <CheckBox x:Name="DisableTelemetry" Content="Disable Telemetry (services + registry)"/>
          <CheckBox x:Name="DisableAds" Content="Disable Consumer Experiences / Start menu suggestions"/>
          <CheckBox x:Name="DisableCortana" Content="Disable Cortana"/>
          <Button Content="Apply Selected Tweaks" Click="ApplyTweaks_Click"/>
        </StackPanel>
      </TabItem>

      <TabItem Header="About">
        <StackPanel Margin="12">
          <TextBlock Text="SzDominikk1 Industries | WindowsShell" FontSize="18" FontWeight="Bold"/>
          <TextBlock Text="Free, user-friendly, reliable. Remove bloatware, tune privacy, and customize Windows."/>
          <TextBlock Text="License: GPLv3" Margin="0,8,0,0"/>
          <TextBlock Text="© 2025 Szuromi Dominik László"/>
        </StackPanel>
      </TabItem>
    </TabControl>
  </DockPanel>
</Window>


// =============================
// File: WindowsShell/MainWindow.xaml.cs
// =============================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WindowsShell.Models;
using WindowsShell.Utilities;

namespace WindowsShell
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AppendLog(string msg)
        {
            LogBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
            LogBox.ScrollToEnd();
        }

        private async void RebootToUefi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await PowerShellHelper.Run("shutdown /r /fw /t 0");
            }
            catch (Exception ex) { AppendLog(ex.Message); }
        }

        private async void CreateRestorePoint_Click(object sender, RoutedEventArgs e)
        {
            AppendLog("Creating restore point...");
            var script = "Checkpoint-Computer -Description 'WindowsShell' -RestorePointType 'MODIFY_SETTINGS'";
            var result = await PowerShellHelper.Run($"powershell -NoProfile -ExecutionPolicy Bypass -Command \"{script}\"");
            AppendLog(result.StdOut);
            if (!string.IsNullOrWhiteSpace(result.StdErr)) AppendLog(result.StdErr);
        }

        private async void InstallChoco_Click(object sender, RoutedEventArgs e)
        {
            AppendLog("Installing Chocolatey (if missing)...");
            string cmd = "powershell -NoProfile -ExecutionPolicy Bypass -Command \"if (!(Get-Command choco -ErrorAction SilentlyContinue)) { Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1')) }\"";
            var res = await PowerShellHelper.Run(cmd);
            AppendLog(res.StdOut);
            if (!string.IsNullOrWhiteSpace(res.StdErr)) AppendLog(res.StdErr);
        }

        private async void RefreshAppx_Click(object sender, RoutedEventArgs e)
        {
            AppendLog("Querying Appx packages...");
            var res = await PowerShellHelper.Run("powershell -NoProfile -Command \"Get-AppxPackage | Select-Object -ExpandProperty PackageFullName\"");
            var lines = res.StdOut.Split(new[] { '\', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            AppxList.ItemsSource = lines.OrderBy(x => x).ToList();
        }

        private async void UninstallAppx_Click(object sender, RoutedEventArgs e)
        {
            if (AppxList.SelectedItem is string pkg)
            {
                AppendLog($"Removing {pkg}...");
                var res = await PowerShellHelper.Run($"powershell -NoProfile -Command \"Remove-AppxPackage -Package '{pkg}'\"");
                AppendLog(res.StdOut + res.StdErr);
            }
        }

        private async void RefreshWin32_Click(object sender, RoutedEventArgs e)
        {
            AppendLog("Reading Win32 uninstall entries...");
            var entries = RegistryUninstallHelper.ReadInstalledApps();
            Win32List.ItemsSource = entries;
        }

        private async void UninstallWin32_Click(object sender, RoutedEventArgs e)
        {
            if (Win32List.SelectedItem is AppEntry entry && !string.IsNullOrWhiteSpace(entry.UninstallString))
            {
                AppendLog($"Uninstalling {entry.DisplayName}...");
                // Most uninstall strings are command lines; run silently if supported.
                await PowerShellHelper.Run(entry.UninstallString);
            }
        }

        private async void ChocoSearch_Click(object sender, RoutedEventArgs e)
        {
            var q = (ChocoSearchBox.Text ?? string.Empty).Trim();
            string arg = q.StartsWith("!") ? $"list {q.Substring(1)} -e -a" : $"search {q}";
            var res = await PowerShellHelper.Run($"choco {arg}");
            var lines = res.StdOut.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                   .Where(l => !l.StartsWith("Chocolatey "))
                                   .ToList();
            ChocoList.ItemsSource = lines;
        }

        private async void ChocoInstall_Click(object sender, RoutedEventArgs e)
        {
            if (ChocoList.SelectedItem is string line)
            {
                var pkg = line.Split(' ').FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(pkg))
                {
                    AppendLog($"Installing {pkg} via Chocolatey...");
                    var res = await PowerShellHelper.Run($"choco install {pkg} -y");
                    AppendLog(res.StdOut + res.StdErr);
                }
            }
        }

        private async void ApplyTweaks_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            if (DisableTelemetry.IsChecked == true)
            {
                sb.AppendLine("sc stop DiagTrack");
                sb.AppendLine("sc config DiagTrack start= disabled");
                sb.AppendLine("reg add HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection /v AllowTelemetry /t REG_DWORD /d 0 /f");
                sb.AppendLine("reg add HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\DataCollection /v AllowTelemetry /t REG_DWORD /d 0 /f");
            }
            if (DisableAds.IsChecked == true)
            {
                sb.AppendLine("reg add HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\CloudContent /v DisableWindowsConsumerFeatures /t REG_DWORD /d 1 /f");
                sb.AppendLine("reg add HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager /v SystemPaneSuggestionsEnabled /t REG_DWORD /d 0 /f");
                sb.AppendLine("reg add HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager /v SubscribedContent-338389Enabled /t REG_DWORD /d 0 /f");
            }
            if (DisableCortana.IsChecked == true)
            {
                sb.AppendLine("reg add HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search /v AllowCortana /t REG_DWORD /d 0 /f");
            }

            if (sb.Length == 0) { AppendLog("No tweaks selected."); return; }

            AppendLog("Applying tweaks (registry + services)...");
            var res = await PowerShellHelper.Run(sb.ToString());
            AppendLog(res.StdOut + res.StdErr);
        }

        private void OpenGitHub(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/yourname/WindowsShell") { UseShellExecute = true });
        }

        private void OpenDiscord(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.gg/yourinvite") { UseShellExecute = true });
        }
    }
}


// =============================
// File: WindowsShell/Models/AppEntry.cs
// =============================
namespace WindowsShell.Models
{
    public class AppEntry
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string UninstallString { get; set; } = string.Empty;
        public override string ToString() => Display;
        public string Display => string.IsNullOrWhiteSpace(DisplayName) ? UninstallString : $"{DisplayName} — {Publisher}";
    }
}


// =============================
// File: WindowsShell/Utilities/PowerShellHelper.cs
// =============================
using System.Diagnostics;
using System.Threading.Tasks;

namespace WindowsShell.Utilities
{
    public static class PowerShellHelper
    {
        public struct Result { public string StdOut; public string StdErr; public int ExitCode; }

        public static async Task<Result> Run(string command)
        {
            var psi = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
            };
            var p = new Process { StartInfo = psi };
            p.Start();
            var stdout = await p.StandardOutput.ReadToEndAsync();
            var stderr = await p.StandardError.ReadToEndAsync();
            p.WaitForExit();
            return new Result { StdOut = stdout, StdErr = stderr, ExitCode = p.ExitCode };
        }
    }
}


// =============================
// File: WindowsShell/Utilities/RegistryUninstallHelper.cs
// =============================
using Microsoft.Win32;
using System.Collections.Generic;
using WindowsShell.Models;

namespace WindowsShell.Utilities
{
    public static class RegistryUninstallHelper
    {
        private static readonly string[] UninstallRoots = new[]
        {
            @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall",
            @"SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall"
        };

        public static List<AppEntry> ReadInstalledApps()
        {
            var list = new List<AppEntry>();
            foreach (var root in UninstallRoots)
            {
                using var baseKey = Registry.LocalMachine.OpenSubKey(root);
                if (baseKey == null) continue;
                foreach (var subName in baseKey.GetSubKeyNames())
                {
                    using var sub = baseKey.OpenSubKey(subName);
                    if (sub == null) continue;
                    var name = sub.GetValue("DisplayName") as string;
                    var publisher = sub.GetValue("Publisher") as string ?? "";
                    var uninstall = sub.GetValue("UninstallString") as string;
                    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(uninstall))
                    {
                        list.Add(new AppEntry { DisplayName = name, Publisher = publisher, UninstallString = uninstall });
                    }
                }
            }
            list.Sort((a,b) => string.Compare(a.DisplayName, b.DisplayName, true));
            return list;
        }
    }
}


// =============================
// File: WindowsShell/LICENSE (GPLv3)
// =============================
# Place the GPLv3 text here (as provided in your message). Also add the standard file headers to each .cs file:
#
# // SzDominikk1 Industries | WindowsShell
# // Copyright (C) 2025 Szuromi Dominik László
# // This program is free software: you can redistribute it and/or modify it under the terms of the GNU GPL v3.
#


// =============================
// File: WindowsShell/README.md
// =============================
# SzDominikk1 Industries | WindowsShell

Free, user-friendly, and reliable Windows customizer.

## Features
- Remove Appx bloat + uninstall Win32 programs
- Chocolatey package search + install (prefix `!` for exact)
- Privacy tweaks (telemetry, ads, Cortana)
- Reboot to UEFI; create restore point

## Build & Run
1. Install .NET 8 SDK and Visual Studio 2022+ with .NET Desktop + WPF.
2. Clone this repo and open `WindowsShell.sln`.
3. Press F5. App requires Administrator.

## Notes
- Some tweaks require reboot to take effect.
- Use responsibly: always create a restore point before big changes.
- This is an original codebase, GPLv3. Not affiliated with Neonity/XtremeShell.
