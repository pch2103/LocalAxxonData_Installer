using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocalAxxonData_Installer.Views;

public partial class RestoreProgressPage : UserControl
{
    private CancellationTokenSource? _cts;
    private bool _isRunning;

    public RestoreProgressPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        ColorBand.HeaderText = LocStrings.RestoreHeader;
        ColorBand.BodyText = LocStrings.RestoreSubtitle;
        StatusText.Text = LocStrings.RestoreBody;
        CancelButton.Content = LocStrings.Cancel;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        StartRestore();
    }

    private async void StartRestore()
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
            RestoreProgressBar.Value = i;
            PercentText.Text = $"{i}%";
        }

        _isRunning = false;

        if (!token.IsCancellationRequested && VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowFinishPage(FinishMode.Restore);
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
