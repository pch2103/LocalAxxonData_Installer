using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Eremex.AvaloniaUI.Controls.Utils;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class InstallDirPage : UserControl
{
    public InstallDirPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        HeadingText.Text = LocStrings.InstallDirHeading;
        BrowseButton.Content = LocStrings.InstallDirBrowse;
        SpaceBlock.Header = string.Format(LocStrings.InstallDirFreeFmt, "45.2 ГБ");
        SpaceBlock.Message = LocStrings.InstallDirRequired;
        InfoBlock.Header = LocStrings.InstallDirInfoHeader;
        InfoBlock.Message = LocStrings.InstallDirInfoBody;
        BackText.Text = LocStrings.Back;
        NextText.Text = LocStrings.Next;
        CancelButton.Content = LocStrings.Cancel;
    }

    private void OnBackClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowWelcomePage();
    }

    private void OnNextClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowSmtpPage();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowExitConfirmPage();
    }

    private async void OnBrowseClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;
        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = LocStrings.InstallDirPickerTitle,
            AllowMultiple = false
        });
        if (folders.Count >= 1)
        {
            var textBox = PathEditor.FindVisualChild<TextBox>();
            if (textBox != null)
                textBox.Text = folders[0].TryGetLocalPath() ?? folders[0].Name;
        }
    }
}
