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
        HeadingText.Text = LocStrings.WelcomeHeading;
        DescText.Text = LocStrings.WelcomeBody;
        WarningBlock.Header = LocStrings.WelcomeWarningHeading;
        WarningBlock.Message = LocStrings.WelcomeWarningBody;
        InfoBlock.Header = LocStrings.WelcomeInfoHeader;
        InfoBlock.Message = LocStrings.WelcomeInfoBody;
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
