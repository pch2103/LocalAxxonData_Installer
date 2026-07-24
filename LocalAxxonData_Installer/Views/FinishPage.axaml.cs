using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public enum FinishMode { Install, Restore, Uninstall }

public partial class FinishPage : UserControl
{
    private readonly FinishMode _mode;

    public FinishPage() : this(FinishMode.Install) { }

    public FinishPage(FinishMode mode)
    {
        InitializeComponent();
        _mode = mode;
        UpdateUI();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        OpenBrowserButton.Content = LocStrings.FinishOpenBrowser;
        PasswordInfo.HeaderText = LocStrings.FinishInstallPasswordHeader;
        PasswordInfo.BodyText = LocStrings.FinishInstallPasswordBody;
        FinishButton.Content = LocStrings.Close;
    }

    private void UpdateUI()
    {
        switch (_mode)
        {
            case FinishMode.Install:
                IconText.Text = "\u2713";
                IconText.Foreground = (IBrush)(Application.Current!.FindResource("PageHeaderGreenBrush")!);
                TitleText.Text = LocStrings.FinishInstallTitle;
                DescriptionText.Text = LocStrings.FinishInstallDesc;
                SuccessCard.IsVisible = true;
                SuccessCardText.Text = LocStrings.FinishUrl;
                ErrorCard.IsVisible = false;
                FootnoteText.Text = LocStrings.FinishInstallFootnote;
                PasswordInfo.IsVisible = true;
                OpenBrowserButton.IsVisible = true;
                break;

            case FinishMode.Restore:
                IconText.Text = "\u2713";
                IconText.Foreground = (IBrush)(Application.Current!.FindResource("PageHeaderGreenBrush")!);
                TitleText.Text = LocStrings.FinishRestoreTitle;
                DescriptionText.Text = LocStrings.FinishRestoreDesc;
                SuccessCard.IsVisible = true;
                SuccessCardText.Text = LocStrings.FinishUrl;
                ErrorCard.IsVisible = false;
                FootnoteText.Text = LocStrings.FinishRestoreFootnote;
                PasswordInfo.IsVisible = false;
                OpenBrowserButton.IsVisible = true;
                break;

            case FinishMode.Uninstall:
                IconText.Text = "\u2715";
                IconText.Foreground = (IBrush)(Application.Current!.FindResource("PageHeaderRedBrush")!);
                TitleText.Text = LocStrings.FinishUninstallTitle;
                DescriptionText.Text = LocStrings.FinishUninstallDesc;
                SuccessCard.IsVisible = false;
                ErrorCard.IsVisible = true;
                ErrorCardText.Text = LocStrings.FinishUninstallCard;
                FootnoteText.Text = LocStrings.FinishUninstallFootnote;
                PasswordInfo.IsVisible = false;
                OpenBrowserButton.IsVisible = false;
                break;
        }
    }

    private void OnOpenBrowserClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.CloseWindow();
        }
    }

    private void OnFinishClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.CloseWindow();
        }
    }
}


