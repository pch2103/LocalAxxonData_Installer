using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocalAxxonData_Installer.Views;

public partial class ProgressPage : UserControl
{
    private CancellationTokenSource? _cts;
    private bool _isRunning;

    public ProgressPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        ColorBand.HeaderText = LocStrings.ProgressHeader;
        ColorBand.BodyText = LocStrings.ProgressSubtitle;
        StatusText.Text = LocStrings.ProgressBody;
        CancelButton.Content = LocStrings.Cancel;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        StartInstallation();
    }

    private async void StartInstallation()
    {
        if (_isRunning) return;
        _isRunning = true;
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
            InstallProgressBar.Value = i;
            PercentText.Text = $"{i}%";
        }

        _isRunning = false;

        if (!token.IsCancellationRequested && VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowRebootPage();
        }
    }

    private void OnCancelClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _cts?.Cancel();
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowExitConfirmPage();
        }
    }
}
