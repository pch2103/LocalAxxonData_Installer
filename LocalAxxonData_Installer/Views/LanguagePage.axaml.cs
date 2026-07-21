using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class LanguagePage : UserControl
{
    public LanguagePage()
    {
        InitializeComponent();
        UpdateLanguage();
        SelectRussian();
    }

    private void UpdateLanguage()
    {
        HeadingText.Text = LocStrings.LanguagePageHeading;
        RussianButton.Content = LocStrings.LanguagePageRussian;
        EnglishButton.Content = LocStrings.LanguagePageEnglish;
        TestButton.Content = LocStrings.LanguagePageTestBtn;
        NextText.Text = LocStrings.Next;
        CancelButton.Content = LocStrings.Cancel;
    }

    private void SelectRussian()
    {
        RussianButton.Classes.Remove("Secondary");
        RussianButton.Classes.Add("Primary");
        EnglishButton.Classes.Remove("Primary");
        EnglishButton.Classes.Add("Secondary");
        NextButton.IsEnabled = true;
        AppLanguageManager.Current = AppLanguage.Russian;
    }

    private void OnRussianClick(object? sender, RoutedEventArgs e)
    {
        RussianButton.Classes.Remove("Secondary");
        RussianButton.Classes.Add("Primary");
        EnglishButton.Classes.Remove("Primary");
        EnglishButton.Classes.Add("Secondary");
        NextButton.IsEnabled = true;
        AppLanguageManager.Current = AppLanguage.Russian;
        UpdateLanguage();
    }

    private void OnEnglishClick(object? sender, RoutedEventArgs e)
    {
        EnglishButton.Classes.Remove("Secondary");
        EnglishButton.Classes.Add("Primary");
        RussianButton.Classes.Remove("Primary");
        RussianButton.Classes.Add("Secondary");
        NextButton.IsEnabled = true;
        AppLanguageManager.Current = AppLanguage.English;
        UpdateLanguage();
    }

    private void OnNextClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowWelcomePage();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowExitConfirmPage();
    }

    private void OnTestAlreadyInstalledClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowAlreadyInstalledPage();
    }
}
