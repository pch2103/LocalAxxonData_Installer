using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Eremex.AvaloniaUI.Controls.Utils;

namespace LocalAxxonData_Installer.Views;

public partial class InstallDirPage : UserControl
{
    public InstallDirPage()
    {
        InitializeComponent();
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
            Title = "Выберите папку для установки",
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
