using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Threading.Tasks;

namespace LocalAxxonData_Installer.Views;

public partial class ResumeProgressPage : UserControl
{
    private bool _isRunning;

    public ResumeProgressPage()
    {
        InitializeComponent();
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

        for (int i = 0; i <= 100; i++)
        {
            await Task.Delay(80);
            InstallResumeBar.Value = i;
            PercentText.Text = $"{i}%";
        }

        _isRunning = false;

        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowFinishPage();
        }
    }
}
