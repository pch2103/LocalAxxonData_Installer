using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace LocalAxxonData_Installer.Views;

public enum MessageSeverity { Info, Warning, Error }

public partial class MessageBlockView : UserControl
{
    public static readonly StyledProperty<string> HeaderTextProperty =
        AvaloniaProperty.Register<MessageBlockView, string>(nameof(HeaderText), "");

    public static readonly StyledProperty<string> BodyTextProperty =
        AvaloniaProperty.Register<MessageBlockView, string>(nameof(BodyText), "");

    public static readonly StyledProperty<MessageSeverity> SeverityProperty =
        AvaloniaProperty.Register<MessageBlockView, MessageSeverity>(nameof(Severity), MessageSeverity.Info);

    public string HeaderText
    {
        get => GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public string BodyText
    {
        get => GetValue(BodyTextProperty);
        set => SetValue(BodyTextProperty, value);
    }

    public MessageSeverity Severity
    {
        get => GetValue(SeverityProperty);
        set => SetValue(SeverityProperty, value);
    }

    public MessageBlockView()
    {
        InitializeComponent();
        SeverityProperty.Changed.AddClassHandler<MessageBlockView>((view, e) => view.ApplySeverity());
        HeaderTextProperty.Changed.AddClassHandler<MessageBlockView>((view, e) => view.HeaderBlock.Text = view.HeaderText);
        BodyTextProperty.Changed.AddClassHandler<MessageBlockView>((view, e) => view.BodyBlock.Text = view.BodyText);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        HeaderBlock.Text = HeaderText;
        BodyBlock.Text = BodyText;
        ApplySeverity();
    }

    private void ApplySeverity()
    {
        BlockBorder.Classes.RemoveAll(new[] { "info-block", "warning-block", "error-block" });
        StreamGeometry? icon = null;
        IBrush? foreground = null;

        switch (Severity)
        {
            case MessageSeverity.Info:
                BlockBorder.Classes.Add("info-block");
                icon = this.FindResource("InfoIcon") as StreamGeometry;
                foreground = this.FindResource("PrimaryColorTint20") as IBrush;
                break;
            case MessageSeverity.Warning:
                BlockBorder.Classes.Add("warning-block");
                icon = this.FindResource("WarningIcon") as StreamGeometry;
                foreground = this.FindResource("OrangeColor") as IBrush;
                break;
            case MessageSeverity.Error:
                BlockBorder.Classes.Add("error-block");
                icon = this.FindResource("ErrorIcon") as StreamGeometry;
                foreground = this.FindResource("RedColor") as IBrush;
                break;
        }

        if (icon != null) BlockIcon.Data = icon;
        if (foreground != null) BlockIcon.Foreground = foreground;
    }
}
