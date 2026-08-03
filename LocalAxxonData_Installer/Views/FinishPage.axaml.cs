using Avalonia;
using Avalonia.Automation;
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
        HostAddressText.Text = LocStrings.FinishUrl;
        ToolTip.SetTip(CopyAddressButton, LocStrings.FinishCopyAddress);
        AutomationProperties.SetName(CopyAddressButton, LocStrings.FinishCopyAddress);
        PasswordInfo.Header = LocStrings.FinishInstallPasswordHeader;
        PasswordInfo.Message = LocStrings.FinishInstallPasswordBody;
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
                HostCard.IsVisible = true;
                ErrorCard.IsVisible = false;
                PasswordInfo.IsVisible = true;
                OpenBrowserButton.IsVisible = true;
                break;

            case FinishMode.Restore:
                IconText.Text = "\u2713";
                IconText.Foreground = (IBrush)(Application.Current!.FindResource("PageHeaderGreenBrush")!);
                TitleText.Text = LocStrings.FinishRestoreTitle;
                DescriptionText.Text = LocStrings.FinishRestoreDesc;
                HostCard.IsVisible = true;
                ErrorCard.IsVisible = false;
                PasswordInfo.IsVisible = false;
                OpenBrowserButton.IsVisible = true;
                break;

            case FinishMode.Uninstall:
                IconText.Text = "\u2715";
                IconText.Foreground = (IBrush)(Application.Current!.FindResource("PageHeaderRedBrush")!);
                TitleText.Text = LocStrings.FinishUninstallTitle;
                DescriptionText.Text = LocStrings.FinishUninstallDesc;
                HostCard.IsVisible = false;
                ErrorCard.IsVisible = true;
                ErrorCard.Message = LocStrings.FinishUninstallCard;
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

    private async void OnCopyAddressClick(object? sender, RoutedEventArgs e)
    {
        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
        if (clipboard is null)
        {
            return;
        }

        await clipboard.SetTextAsync(LocStrings.FinishUrl);
        ToolTip.SetTip(CopyAddressButton, LocStrings.FinishAddressCopied);
        AutomationProperties.SetName(CopyAddressButton, LocStrings.FinishAddressCopied);
    }

    private void OnFinishClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.CloseWindow();
        }
    }
}


