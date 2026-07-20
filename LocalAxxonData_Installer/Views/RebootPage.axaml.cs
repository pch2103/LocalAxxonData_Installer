using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocalAxxonData_Installer.Views;

public partial class RebootPage : UserControl
{
    private int _countdown = 60;
    private CancellationTokenSource? _cts;

    public RebootPage()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        StartCountdown();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _cts?.Cancel();
    }

    private async void StartCountdown()
    {
        _cts = new CancellationTokenSource();
        _countdown = 60;

        try
        {
            for (int i = _countdown; i >= 0; i--)
            {
                _cts.Token.ThrowIfCancellationRequested();
                TimerText.Text = $"{i} сек.";
                await Task.Delay(1000, _cts.Token);
            }

            if (!_cts.Token.IsCancellationRequested)
            {
                NavigateToPhase2();
            }
        }
        catch (OperationCanceledException)
        {
        }
    }

    private void OnDeferClick(object? sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        DeferButton.IsEnabled = false;
        RebootButton.IsEnabled = true;
        TimerText.Text = "отложено";
    }

    private void OnRebootClick(object? sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        NavigateToPhase2();
    }

    private void NavigateToPhase2()
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowResumeProgressPage();
        }
    }
}
