using Avalonia.Controls;
using Avalonia.Input;
using Eremex.AvaloniaUI.Controls.Common;

namespace LocalAxxonData_Installer.Views;

public partial class MainWindow : MxWindow
{
    private UserControl? _previousContent;

    public MainWindow()
    {
        InitializeComponent();
    }

    public void ShowLanguagePage()
    {
        MainContentControl.Content = new LanguagePage();
        ResetSizeToContent();
    }
    public void ShowWelcomePage()
    {
        MainContentControl.Content = new WelcomePage();
        ResetSizeToContent();
    }
    public void ShowInstallDirPage()
    {
        MainContentControl.Content = new InstallDirPage();
        ResetSizeToContent();
    }
    public void ShowSmtpPage()
    {
        MainContentControl.Content = new SmtpPage();
        ResetSizeToContent();
    }
    public void ShowSummaryPage()
    {
        MainContentControl.Content = new SummaryPage();
        ResetSizeToContent();
    }
    public void ShowProgressPage()
    {
        MainContentControl.Content = new ProgressPage();
        ResetSizeToContent();
    }
    public void ShowRebootPage()
    {
        MainContentControl.Content = new RebootPage();
        ResetSizeToContent();
    }
    public void ShowResumeProgressPage()
    {
        MainContentControl.Content = new ResumeProgressPage();
        ResetSizeToContent();
    }
    public void ShowRestoreProgressPage()
    {
        MainContentControl.Content = new RestoreProgressPage();
        ResetSizeToContent();
    }
    public void ShowFinishPage()
    {
        MainContentControl.Content = new FinishPage(FinishMode.Install);
        ResetSizeToContent();
    }
    public void ShowFinishPage(FinishMode mode)
    {
        MainContentControl.Content = new FinishPage(mode);
        ResetSizeToContent();
    }
    public void ShowAlreadyInstalledPage()
    {
        MainContentControl.Content = new AlreadyInstalledPage();
        ResetSizeToContent();
    }
    public void ShowUninstallPage()
    {
        MainContentControl.Content = new UninstallPage();
        ResetSizeToContent();
    }
    public void ShowExitConfirmPage()
    {
        _previousContent = MainContentControl.Content as UserControl;
        MainContentControl.Content = new ExitConfirmPage();
        ResetSizeToContent();
    }
    public void RestorePreviousPage()
    {
        if (_previousContent != null)
        {
            MainContentControl.Content = _previousContent;
            _previousContent = null;
        }
        else
        {
            ShowSummaryPage();
        }
        ResetSizeToContent();
    }
    public void ShowErrorPage()
    {
        MainContentControl.Content = new ErrorPage();
        ResetSizeToContent();
    }

    private void ResetSizeToContent()
    {
        SizeToContent = SizeToContent.Manual;
        SizeToContent = SizeToContent.WidthAndHeight;
    }

    public void CloseWindow()
    {
        Close();
    }

    public void BeginWindowDrag(PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            BeginMoveDrag(e);
        }
    }
}
