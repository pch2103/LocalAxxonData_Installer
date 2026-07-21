using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocalAxxonData_Installer.Views;

public partial class ResumeProgressPage : UserControl
{
    private CancellationTokenSource? _cts;
    private bool _isRunning;

    public ResumeProgressPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        ColorBand.HeaderText = LocStrings.ResumeHeader;
        ColorBand.BodyText = LocStrings.ResumeSubtitle;
        StatusText.Text = LocStrings.ResumeBody;
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
                await Task.Delay(80, token);
            }
            catch (TaskCanceledException)
            {
                break;
            }
            InstallResumeBar.Value = i;
            PercentText.Text = $"{i}%";
        }

        _isRunning = false;

        if (!token.IsCancellationRequested && VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowFinishPage();
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
