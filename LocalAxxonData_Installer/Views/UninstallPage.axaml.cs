using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocalAxxonData_Installer.Views;

public partial class UninstallPage : UserControl
{
    private CancellationTokenSource? _cts;

    public UninstallPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        HeadingText.Text = LocStrings.UninstallHeading;
        ConfirmText.Text = LocStrings.UninstallConfirmText;
        WarningHeaderText.Text = LocStrings.UninstallWarningHeader;
        WarningBodyText.Text = LocStrings.UninstallWarningBody;
        ProgressText.Text = LocStrings.UninstallProgressLabel;
        BackText.Text = LocStrings.Back;
        UninstallButton.Content = LocStrings.UninstallAction;
        CancelButton1.Content = LocStrings.Cancel;
        CancelButton2.Content = LocStrings.Cancel;
    }

    private void OnBackClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowAlreadyInstalledPage();
        }
    }

    private async void OnUninstallClick(object? sender, RoutedEventArgs e)
    {
        ConfirmationPanel.IsVisible = false;
        ConfirmationFooter.IsVisible = false;
        ProgressPanel.IsVisible = true;
        ProgressFooter.IsVisible = true;

        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        for (int i = 0; i <= 100; i++)
        {
            if (token.IsCancellationRequested)
                break;
            try
            {
                await Task.Delay(50, token);
            }
            catch (TaskCanceledException)
            {
                break;
            }
            UninstallProgressBar.Value = i;
            UninstallPercentText.Text = $"{i}%";
        }

        if (!token.IsCancellationRequested && VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowFinishPage(FinishMode.Uninstall);
        }
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowExitConfirmPage();
        }
    }
}
