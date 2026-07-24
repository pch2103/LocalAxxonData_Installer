using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class SmtpPage : UserControl
{
    public SmtpPage()
    {
        InitializeComponent();
        UpdateLanguage();
        EncryptionCombo.SelectedItem = "STARTTLS";
    }

    private void UpdateLanguage()
    {
        HeadingText.Text = LocStrings.SmtpHeading;
        ServerLabel.Text = LocStrings.SmtpServerLabel;
        PortLabel.Text = LocStrings.SmtpPortLabel;
        EncryptionLabel.Text = LocStrings.SmtpEncryptionLabel;
        UsernameLabel.Text = LocStrings.SmtpUsernameLabel;
        PasswordLabel.Text = LocStrings.SmtpPasswordLabel;
        SenderLabel.Text = LocStrings.SmtpSenderLabel;
        BackText.Text = LocStrings.Back;
        NextText.Text = LocStrings.Next;
        CancelButton.Content = LocStrings.Cancel;
        EncryptionCombo.ItemsSource = new List<string>
        {
            LocStrings.SmtpNoEncryption,
            "STARTTLS",
            "SSL/TLS"
        };
    }

    private void OnBackClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowInstallDirPage();
    }

    private void OnNextClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowSummaryPage();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowExitConfirmPage();
    }
}
