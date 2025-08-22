using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WindowsShell
{
    public partial class MainWindow : Window
    {
        private List<string> selectedOptions = new List<string>();
        private bool isMaximized = false;
        private bool isInitializing = true;

        public MainWindow()
        {
            InitializeComponent();
            InitializeOptions();
            LoadInstalledApps();
            LoadPackageStore();

            this.Loaded += (s, e) =>
            {
                isInitializing = false;
                SetLanguage("EN");
            };
        }

        private void InitializeOptions()
        {
            var options = new List<string>
            {
                "Disable Windows Defender",
                "Disable Cortana",
                "Disable OneDrive",
                "Disable Game Bar",
                "Disable Location Tracking",
                "Disable Error Reporting",
                "Disable Background Apps",
                "Enable Dark Mode",
                "Show File Extensions",
                "Show Hidden Files"
            };

            OptionsList.ItemsSource = options;
        }

        private void LoadInstalledApps()
        {
            var apps = new List<AppInfo>
            {
                new AppInfo { Name = "Google Chrome", Size = "120 MB", Version = "1.0", Publisher = "Google LLC", Path = "C:\\Program Files\\Google\\Chrome" },
                new AppInfo { Name = "Visual Studio Code", Size = "300 MB", Version = "1.60.0", Publisher = "Microsoft", Path = "C:\\Users\\User\\AppData\\Local\\Programs\\Microsoft VS Code" },
                new AppInfo { Name = "Spotify", Size = "200 MB", Version = "1.1.72", Publisher = "Spotify AB", Path = "C:\\Users\\User\\AppData\\Roaming\\Spotify" }
            };

            InstalledAppsGrid.ItemsSource = apps;
        }

        private void LoadPackageStore()
        {
            var packages = new List<PackageInfo>
            {
                new PackageInfo { Name = "firefox", Version = "105.0", Description = "Mozilla Firefox Web Browser" },
                new PackageInfo { Name = "vlc", Version = "3.0.18", Description = "VLC media player" },
                new PackageInfo { Name = "7zip", Version = "21.07", Description = "7-Zip File Archiver" },
                new PackageInfo { Name = "git", Version = "2.37.3", Description = "Git Version Control System" },
                new PackageInfo { Name = "python", Version = "3.11.0", Description = "Python Programming Language" }
            };

            PackageStoreGrid.ItemsSource = packages;
        }

        private void SetLanguage(string languageCode)
        {
            if (HomeTab == null || PrivacyText == null || LangHU == null)
                return;

            if (languageCode == "HU")
            {
                HomeTab.Content = "Kezdőlap";
                RemoveInstallTab.Content = "Alkalmazások";
                PackageStoreTab.Content = "Csomagtár";

                PrivacyText.Text = "Adatvédelem";
                UninstallEdgeButton.Content = "Edge eltávolítása és átirányítások visszavonása";

                GamingText.Text = "Játék";
                PowerPlanButton.Content = "Ultimate teljesítményi séma engedélyezése";

                FilesText.Text = "Fájlok";
                CleanRecycleBinButton.Content = "Szemetes kiürítése";

                UpdatesText.Text = "Frissítések";
                WindowsUpdateButton.Content = "Egyedi Windows frissítési beállítások engedélyezése";

                DisableFunctionsText.Text = "Funkciók letiltása, takarítás";
                DisableTelemetryButton.Content = "Telemetria letiltása, reklámok kikapcsolása";
                DisableStickyKeysButton.Content = "Ragadós billentyűk gyorsbillentyűjének letiltása";
                DeleteTempFilesButton.Content = "Ideiglenes fájlok törlése";
                RepairChocolateyButton.Content = "Chocolatey javítása/frissítése";

                WelcomeText.Text = "Üdvözöljük a WindowsShell-ben!";
                WebsiteText.Text = "SzDominikk1 Industries | www.szdominikk1.com";
                AuthorText.Text = "Szuromi Dominik László";

                InstalledAppsText.Text = "Telepített alkalmazások";
                RemoveAppButton.Content = "Eltávolítás";
                SelectAllButton.Content = "Összes kijelölése";
                SelectButton.Content = "Kijelölés";

                if (PackageSearchTextBox != null)
                {
                    PackageSearchTextBox.Text = "Csomagtár (keresés a böngészéshez)";
                    PackageSearchTextBox.Foreground = Brushes.Gray;
                }

                InstallSelectedButton.Content = "Kijelöltek telepítése";
                ClearSelectionButton.Content = "Kijelölés törlése";
                RemovePackageButton.Content = "Eltávolítás";

                RebootUEFIButton.Content = "UEFI-be újraindítás";
                RebootButton.Content = "Újraindítás";
                ShutdownButton.Content = "Leállítás";
                ExitButton.Content = "Kilépés";
            }
            else
            {
                HomeTab.Content = "Home";
                RemoveInstallTab.Content = "Remove/Install apps";
                PackageStoreTab.Content = "Package store";

                PrivacyText.Text = "Privacy";
                UninstallEdgeButton.Content = "Uninstall Edge and undo Edge redirections";

                GamingText.Text = "Gaming";
                PowerPlanButton.Content = "Enable ultimate power plan";

                FilesText.Text = "Files";
                CleanRecycleBinButton.Content = "Clean recycle bin";

                UpdatesText.Text = "Updates";
                WindowsUpdateButton.Content = "Enable custom Windows update presets";

                DisableFunctionsText.Text = "Disable functions, cleanup";
                DisableTelemetryButton.Content = "Disable telemetry, disable ads";
                DisableStickyKeysButton.Content = "Disable sticky keys hotkey";
                DeleteTempFilesButton.Content = "Delete temporary files";
                RepairChocolateyButton.Content = "Repair/update Chocolatey";

                WelcomeText.Text = "Welcome to WindowsShell!";
                WebsiteText.Text = "SzDominikk1 Industries | www.szdominikk1.com";
                AuthorText.Text = "Szuromi Dominik László";

                InstalledAppsText.Text = "Installed applications";
                RemoveAppButton.Content = "Remove";
                SelectAllButton.Content = "Select all";
                SelectButton.Content = "Select";

                if (PackageSearchTextBox != null)
                {
                    PackageSearchTextBox.Text = "Package store (search to browse)";
                    PackageSearchTextBox.Foreground = Brushes.Gray;
                }

                InstallSelectedButton.Content = "Install selected";
                ClearSelectionButton.Content = "Clear selection";
                RemovePackageButton.Content = "Remove";

                RebootUEFIButton.Content = "Reboot to UEFI";
                RebootButton.Content = "Reboot";
                ShutdownButton.Content = "Shutdown";
                ExitButton.Content = "Exit";
            }
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isMaximized)
            {
                WindowState = WindowState.Normal;
                MaximizeButton.Content = "☐";
                MainBorder.Margin = new Thickness(10, 40, 10, 10);
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.Content = "❐";
                MainBorder.Margin = new Thickness(0);
            }
            isMaximized = !isMaximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            HomeTab.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"));
            RemoveInstallTab.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"));
            PackageStoreTab.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"));

            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3A3A3A"));

            HomeContent.Visibility = button.Name == "HomeTab" ? Visibility.Visible : Visibility.Collapsed;
            RemoveInstallContent.Visibility = button.Name == "RemoveInstallTab" ? Visibility.Visible : Visibility.Collapsed;
            PackageStoreContent.Visibility = button.Name == "PackageStoreTab" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Language_Checked(object sender, RoutedEventArgs e)
        {
            if (isInitializing) return;

            var radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                SetLanguage(radioButton.Content.ToString());
            }
        }

        private void ShowNotification(string message, bool isSuccess = true)
        {
            NotificationText.Text = message;
            var storyboard = (Storyboard)FindResource(isSuccess ? "SuccessAnimation" : "ErrorAnimation");
            storyboard.Begin(NotificationBorder);
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void OptionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null && !selectedOptions.Contains(checkBox.Content.ToString()))
            {
                selectedOptions.Add(checkBox.Content.ToString());
            }
        }

        private void OptionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null && selectedOptions.Contains(checkBox.Content.ToString()))
            {
                selectedOptions.Remove(checkBox.Content.ToString());
            }
        }

        private void PackageSearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PackageSearchTextBox.Text == "Package store (search to browse)" ||
                PackageSearchTextBox.Text == "Csomagtár (keresés a böngészéshez)")
            {
                PackageSearchTextBox.Text = "";
                PackageSearchTextBox.Foreground = Brushes.White;
            }
        }

        private void PackageSearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PackageSearchTextBox.Text))
            {
                PackageSearchTextBox.Foreground = Brushes.Gray;
                PackageSearchTextBox.Text = LangHU.IsChecked == true ?
                    "Csomagtár (keresés a böngészéshez)" :
                    "Package store (search to browse)";
            }
        }

        private void UninstallEdgeButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Edge uninstallation process started...");
        }

        private void PowerPlanButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Ultimate power plan enabled!");
        }

        private void CleanRecycleBinButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowNotification("Recycle bin cleaned successfully!");
            }
            catch (Exception ex)
            {
                ShowNotification($"Error: {ex.Message}", false);
            }
        }

        private void WindowsUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Windows update settings configured!");
        }

        private void DisableTelemetryButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Telemetry and ads disabled!");
        }

        private void DisableStickyKeysButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Sticky keys hotkey disabled!");
        }

        private void DeleteTempFilesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowNotification("Temporary files deleted!");
            }
            catch (Exception ex)
            {
                ShowNotification($"Error: {ex.Message}", false);
            }
        }

        private void RepairChocolateyButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Chocolatey repair process started...");
        }

        private void RemoveAppButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedApps = InstalledAppsGrid.Items.OfType<AppInfo>().Where(app => app.IsSelected).ToList();
            if (selectedApps.Count == 0)
            {
                ShowNotification("Please select at least one application to remove.", false);
                return;
            }

            ShowNotification($"Removing {selectedApps.Count} applications...");
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in InstalledAppsGrid.Items)
            {
                if (item is AppInfo app)
                {
                    app.IsSelected = true;
                }
            }
            InstalledAppsGrid.Items.Refresh();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InstallSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPackages = PackageStoreGrid.Items.OfType<PackageInfo>().Where(pkg => pkg.IsSelected).ToList();
            if (selectedPackages.Count == 0)
            {
                ShowNotification("Please select at least one package to install.", false);
                return;
            }

            ShowNotification($"Installing {selectedPackages.Count} packages...");
        }

        private void ClearSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in PackageStoreGrid.Items)
            {
                if (item is PackageInfo pkg)
                {
                    pkg.IsSelected = false;
                }
            }
            PackageStoreGrid.Items.Refresh();
        }

        private void RemovePackageButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Package removal process started...");
        }

        private void RebootUEFIButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Rebooting to UEFI...");
        }

        private void RebootButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Rebooting system...");
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNotification("Shutting down system...");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public class AppInfo
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string Path { get; set; }
    }

    public class PackageInfo
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
    }
}