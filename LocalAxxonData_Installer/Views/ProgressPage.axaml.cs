using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocalAxxonData_Installer.Views;

public enum ProgressMode { Phase1, Phase2, Restore }

public partial class ProgressPage : UserControl
{
    private readonly ProgressMode _mode;
    private CancellationTokenSource? _cts;
    private bool _isRunning;

    public ProgressPage() : this(ProgressMode.Phase1) { }

    public ProgressPage(ProgressMode mode)
    {
        InitializeComponent();
        _mode = mode;
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        switch (_mode)
        {
            case ProgressMode.Phase1:
                StatusText.Text = LocStrings.ProgressBody;
                break;
            case ProgressMode.Phase2:
                StatusText.Text = LocStrings.ResumeBody;
                break;
            case ProgressMode.Restore:
                StatusText.Text = LocStrings.RestoreBody;
                break;
        }
        CancelButton.Content = LocStrings.Cancel;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        StartProgress();
    }

    private async void StartProgress()
    {
        if (_isRunning) return;
        _isRunning = true;
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        int delay = _mode switch
        {
            ProgressMode.Phase2 => 80,
            _ => 50
        };

        for (int i = 0; i <= 100; i++)
        {
            if (token.IsCancellationRequested)
                break;
            try
            {
                await Task.Delay(delay, token);
            }
            catch (TaskCanceledException)
            {
                break;
            }
            ProgressBarControl.Value = i;
            PercentText.Text = $"{i}%";
        }

        _isRunning = false;

        if (token.IsCancellationRequested)
            return;

        if (VisualRoot is MainWindow mainWindow)
        {
            switch (_mode)
            {
                case ProgressMode.Phase1:
                    mainWindow.ShowRebootPage();
                    break;
                case ProgressMode.Phase2:
                    mainWindow.ShowFinishPage();
                    break;
                case ProgressMode.Restore:
                    mainWindow.ShowFinishPage(FinishMode.Restore);
                    break;
            }
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
