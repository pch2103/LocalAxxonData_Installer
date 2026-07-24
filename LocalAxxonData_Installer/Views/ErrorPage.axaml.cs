using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class ErrorPage : UserControl
{
    public ErrorPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        HeadingText.Text = LocStrings.ErrorHeading;
        ErrorTitleText.Text = LocStrings.ErrorTitle;
        ErrorDescText.Text = LocStrings.ErrorDesc;
        ErrorBlock.HeaderText = LocStrings.ErrorCodeBlock;
        ErrorBlock.BodyText = LocStrings.ErrorLogInfo;
        ShowLogButton.Content = LocStrings.ErrorShowLog;
        CloseButton.Content = LocStrings.ErrorClose;
    }

    private void OnCloseClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.CloseWindow();
        }
    }
}
