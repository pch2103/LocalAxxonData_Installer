using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class WelcomePage : UserControl
{
    public WelcomePage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        ColorBand.HeaderText = "Local AxxonData";
        ColorBand.BodyText = LocStrings.WelcomeSubtitle;
        HeadingText.Text = LocStrings.WelcomeHeading;
        DescText.Text = LocStrings.WelcomeBody;
        WarningHeadingText.Text = LocStrings.WelcomeWarningHeading;
        WarningBodyText.Text = LocStrings.WelcomeWarningBody;
        InfoBlock.HeaderText = LocStrings.WelcomeInfoHeader;
        InfoBlock.BodyText = LocStrings.WelcomeInfoBody;
        NextText.Text = LocStrings.Next;
        CancelButton.Content = LocStrings.Cancel;
    }

    private void OnNextClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowInstallDirPage();
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
