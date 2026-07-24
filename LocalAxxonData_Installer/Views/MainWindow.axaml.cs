using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Eremex.AvaloniaUI.Controls.Common;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class MainWindow : MxWindow
{
    private UserControl? _previousContent;

    public MainWindow()
    {
        InitializeComponent();
        ShowLanguagePage();
    }

    private void SetPage(UserControl page, string brushKey, string? header = null, string? body = null, string? bgImage = null)
    {
        PageColorBand.HeaderBrush = (IBrush)this.FindResource(brushKey)!;
        PageColorBand.HeaderText = header ?? "";
        PageColorBand.BodyText = body ?? "";
        PageColorBand.BackgroundImageSource = bgImage ?? "";
        PageColorBand.ShowLogo = true;
        MainContentControl.Content = page;
    }

    public void ShowLanguagePage()
    {
        SetPage(new LanguagePage(), "PageHeaderBlueBrush", "Local AxxonData", null, "header_back_blue.png");
    }
    public void ShowWelcomePage()
    {
        SetPage(new WelcomePage(), "PageHeaderBlueBrush", LocStrings.ProductName, LocStrings.WelcomeSubtitle, "header_back_blue.png");
    }
    public void ShowInstallDirPage()
    {
        SetPage(new InstallDirPage(), "PageHeaderBlueBrush", LocStrings.InstallDirHeader, null, "header_back_blue.png");
    }
    public void ShowSmtpPage()
    {
        SetPage(new SmtpPage(), "PageHeaderBlueBrush", LocStrings.SmtpHeader, null, "header_back_blue.png");
    }
    public void ShowSummaryPage()
    {
        SetPage(new SummaryPage(), "PageHeaderBlueBrush", LocStrings.SummaryHeader, null, "header_back_blue.png");
    }
    public void ShowProgressPage(ProgressMode mode = ProgressMode.Phase1)
    {
        string header = mode switch
        {
            ProgressMode.Phase1 => LocStrings.ProgressHeader,
            ProgressMode.Phase2 => LocStrings.ResumeHeader,
            _ => LocStrings.RestoreHeader
        };
        string body = mode switch
        {
            ProgressMode.Phase1 => LocStrings.ProgressSubtitle,
            ProgressMode.Phase2 => LocStrings.ResumeSubtitle,
            _ => LocStrings.RestoreSubtitle
        };
        SetPage(new ProgressPage(mode), "PageHeaderBlueBrush", header, body, "header_back_blue.png");
    }
    public void ShowRebootPage()
    {
        SetPage(new RebootPage(), "PageHeaderOrangeBrush", LocStrings.RebootHeader, LocStrings.RebootSubtitle, "header_back_orange.png");
    }
    public void ShowFinishPage()
    {
        ShowFinishPage(FinishMode.Install);
    }
    public void ShowFinishPage(FinishMode mode)
    {
        (string brush, string header, string body, string bg) = mode switch
        {
            FinishMode.Install => ("PageHeaderGreenBrush", LocStrings.FinishInstallHeader, LocStrings.FinishInstallSubtitle, "header_back_green.png"),
            FinishMode.Restore => ("PageHeaderGreenBrush", LocStrings.FinishRestoreHeader, LocStrings.FinishRestoreSubtitle, "header_back_green.png"),
            _ => ("PageHeaderRedBrush", LocStrings.FinishUninstallHeader, LocStrings.FinishUninstallSubtitle, "header_back_red.png")
        };
        SetPage(new FinishPage(mode), brush, header, body, bg);
    }
    public void ShowAlreadyInstalledPage()
    {
        SetPage(new AlreadyInstalledPage(), "PageHeaderDarkBlueBrush", LocStrings.ProductName, LocStrings.AlreadyInstalledSubtitle, "header_back_darkblue.png");
    }
    public void ShowUninstallPage()
    {
        SetPage(new UninstallPage(), "PageHeaderRedBrush", LocStrings.UninstallHeader, null, "header_back_red.png");
    }
    public void ShowExitConfirmPage()
    {
        _previousContent = MainContentControl.Content as UserControl;
        SetPage(new ExitConfirmPage(), "PageHeaderGrayBrush", LocStrings.ExitConfirmHeader, null, "header_back_gray.png");
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
    }
    public void ShowErrorPage()
    {
        SetPage(new ErrorPage(), "PageHeaderRedBrush", LocStrings.ErrorHeader, LocStrings.ErrorSubtitle, "header_back_red.png");
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
