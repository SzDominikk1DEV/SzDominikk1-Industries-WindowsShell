using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WindowsShell
{
    public partial class MainWindow : Window
    {
        private bool isEnglish = true;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var options = new List<string>
            {
                "Classic context menu",
                "Power throttling",
                "Windows update",
                "Visual effects",
                "Dark theme",
                "Windows Copilot",
                "Show file extensions",
                "Game mode",
                "Hibernation",
                "Open file explorer to \"This PC\"",
                "Windows error reporting",
                "Verbose logon",
                "Open to settings"
            };

            OptionsList.ItemsSource = options;

            var installedApps = new List<AppInfo>
            {
                new AppInfo { Name = "Google Chrome", Size = "120 MB", Version = "1.2.3", Publisher = "Google Inc.", Path = "C:\\Program Files\\Google\\Chrome" },
                new AppInfo { Name = "Mozilla Firefox", Size = "110 MB", Version = "4.5.6", Publisher = "Mozilla Foundation", Path = "C:\\Program Files\\Mozilla Firefox" },
                new AppInfo { Name = "Microsoft Word", Size = "2.5 GB", Version = "16.0.1", Publisher = "Microsoft Corporation", Path = "C:\\Program Files\\Microsoft Office" }
            };

            InstalledAppsGrid.ItemsSource = installedApps;

            var packages = new List<PackageInfo>
            {
                new PackageInfo { Name = "7-Zip", Version = "22.1", Description = "File archiver with high compression ratio" },
                new PackageInfo { Name = "VLC Media Player", Version = "3.0.18", Description = "Free and open source multimedia player" },
                new PackageInfo { Name = "Notepad++", Version = "8.4.8", Description = "Source code editor and Notepad replacement" }
            };

            PackageStoreGrid.ItemsSource = packages;
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            HomeTab.Background = (Brush)new BrushConverter().ConvertFrom("#1E1E1E");
            RemoveInstallTab.Background = (Brush)new BrushConverter().ConvertFrom("#1E1E1E");
            PackageStoreTab.Background = (Brush)new BrushConverter().ConvertFrom("#1E1E1E");

            HomeContent.Visibility = Visibility.Collapsed;
            RemoveInstallContent.Visibility = Visibility.Collapsed;
            PackageStoreContent.Visibility = Visibility.Collapsed;

            if (button == HomeTab)
            {
                HomeTab.Background = (Brush)new BrushConverter().ConvertFrom("#3A3A3A");
                HomeContent.Visibility = Visibility.Visible;
            }
            else if (button == RemoveInstallTab)
            {
                RemoveInstallTab.Background = (Brush)new BrushConverter().ConvertFrom("#3A3A3A");
                RemoveInstallContent.Visibility = Visibility.Visible;
            }
            else if (button == PackageStoreTab)
            {
                PackageStoreTab.Background = (Brush)new BrushConverter().ConvertFrom("#3A3A3A");
                PackageStoreContent.Visibility = Visibility.Visible;
            }
        }

        private void Language_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            isEnglish = radioButton == LangEN;

            UpdateLanguage();
        }

        private void UpdateLanguage()
        {
            if (isEnglish)
            {
                HomeTab.Content = "Home";
                RemoveInstallTab.Content = "Remove/Install apps";
                PackageStoreTab.Content = "Package store";
            }
            else
            {
                HomeTab.Content = "Kezdőlap";
                RemoveInstallTab.Content = "Alkalmazások eltávolítása/telepítése";
                PackageStoreTab.Content = "Csomagtár";
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public class AppInfo
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string Path { get; set; }
    }

    public class PackageInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}
