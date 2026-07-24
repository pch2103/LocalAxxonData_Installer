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
        InfoHeaderText.Text = LocStrings.SummaryInfoHeader;
        DirText.Text = string.Format(LocStrings.SummaryDirFmt, @"C:\Program Files\Local AxxonData");
        SmtpText.Text = LocStrings.SummarySmtpConfigured;
        PasswordText.Text = LocStrings.SummaryPasswordNote;
        WarningHeadingText.Text = LocStrings.Warning;
        WarningBodyText.Text = LocStrings.SummaryRebootWarning;
        InfoBlock.HeaderText = LocStrings.SummaryInfoHeader2;
        InfoBlock.BodyText = LocStrings.SummaryInfoBody;
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
