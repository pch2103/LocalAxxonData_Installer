using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

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
    }

    private static readonly IBrush GreenBrush = Brush.Parse("#2E7D32");
    private static readonly IBrush RedBrush = Brush.Parse("#C62828");

    private void UpdateUI()
    {
        switch (_mode)
        {
            case FinishMode.Install:
                HeaderBand.HeaderBrush = GreenBrush;
                HeaderBand.HeaderText = "Установка завершена";
                HeaderBand.BodyText = "Local AxxonData готов к работе";
                HeaderBand.BackgroundImageSource = "header_back_green.png";
                IconText.Text = "\u2713";
                IconText.Foreground = GreenBrush;
                TitleText.Text = "Успешно установлено!";
                DescriptionText.Text = "Local AxxonData установлен и готов к использованию.";
                SuccessCard.IsVisible = true;
                SuccessCardText.Text = "http://localhost:8080";
                ErrorCard.IsVisible = false;
                FootnoteText.Text = "Зарегистрируйте пользователя и подтвердите email.";
                PasswordInfo.IsVisible = true;
                OpenBrowserButton.IsVisible = true;
                break;

            case FinishMode.Restore:
                HeaderBand.HeaderBrush = GreenBrush;
                HeaderBand.HeaderText = "Восстановление завершено";
                HeaderBand.BodyText = "Local AxxonData восстановлен";
                HeaderBand.BackgroundImageSource = "header_back_green.png";
                IconText.Text = "\u2713";
                IconText.Foreground = GreenBrush;
                TitleText.Text = "Восстановление завершено!";
                DescriptionText.Text = "Local AxxonData восстановлен и готов к работе.";
                SuccessCard.IsVisible = true;
                SuccessCardText.Text = "http://localhost:8080";
                ErrorCard.IsVisible = false;
                FootnoteText.Text = "Восстановление прошло успешно.";
                PasswordInfo.IsVisible = false;
                OpenBrowserButton.IsVisible = true;
                break;

            case FinishMode.Uninstall:
                HeaderBand.HeaderBrush = RedBrush;
                HeaderBand.HeaderText = "Удаление завершено";
                HeaderBand.BodyText = "Local AxxonData удалён";
                HeaderBand.BackgroundImageSource = "header_back_red.png";
                IconText.Text = "\u2715";
                IconText.Foreground = RedBrush;
                TitleText.Text = "Удаление завершено";
                DescriptionText.Text = "Local AxxonData удалён с компьютера.";
                SuccessCard.IsVisible = false;
                ErrorCard.IsVisible = true;
                ErrorCardText.Text = "Продукт удалён";
                FootnoteText.Text = "Все данные удалены.";
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
