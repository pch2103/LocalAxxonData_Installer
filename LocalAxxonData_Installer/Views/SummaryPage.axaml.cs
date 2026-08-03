using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class SummaryPage : UserControl
{
    public SummaryPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        HeadingText.Text = LocStrings.SummaryHeading;
        SettingsBlock.Header = LocStrings.SummaryInfoHeader;
        SettingsBlock.Message = string.Join(
            Environment.NewLine,
            string.Format(LocStrings.SummaryDirFmt, @"C:\Program Files\Local AxxonData"),
            LocStrings.SummarySmtpConfigured,
            LocStrings.SummaryPasswordNote);
        WarningBlock.Header = LocStrings.Warning;
        WarningBlock.Message = LocStrings.SummaryRebootWarning;
        InfoBlock.Header = LocStrings.SummaryInfoHeader2;
        InfoBlock.Message = LocStrings.SummaryInfoBody;
        BackText.Text = LocStrings.Back;
        InstallButton.Content = LocStrings.SummaryInstall;
        CancelButton.Content = LocStrings.Cancel;
    }

    private void OnBackClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowSmtpPage();
        }
    }

    private void OnInstallClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowProgressPage();
        }
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowExitConfirmPage();
        }
    }
}
