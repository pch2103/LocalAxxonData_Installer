using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace ONE.PSIM.DesignSystem.Controls;

public enum MessageSeverity
{
    Info,
    Success,
    Warning,
    Error
}

public sealed class MessageBlock : TemplatedControl
{
    public static readonly StyledProperty<string?> HeaderProperty =
        AvaloniaProperty.Register<MessageBlock, string?>(nameof(Header));

    public static readonly StyledProperty<string?> MessageProperty =
        AvaloniaProperty.Register<MessageBlock, string?>(nameof(Message));

    public static readonly StyledProperty<MessageSeverity> SeverityProperty =
        AvaloniaProperty.Register<MessageBlock, MessageSeverity>(
            nameof(Severity),
            MessageSeverity.Info);

    static MessageBlock()
    {
        SeverityProperty.Changed.AddClassHandler<MessageBlock>(
            static (control, _) => control.UpdateSeverity());
    }

    public MessageBlock()
    {
        UpdateSeverity();
    }

    public string? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public string? Message
    {
        get => GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public MessageSeverity Severity
    {
        get => GetValue(SeverityProperty);
        set => SetValue(SeverityProperty, value);
    }

    private void UpdateSeverity()
    {
        PseudoClasses.Set(":info", Severity == MessageSeverity.Info);
        PseudoClasses.Set(":success", Severity == MessageSeverity.Success);
        PseudoClasses.Set(":warning", Severity == MessageSeverity.Warning);
        PseudoClasses.Set(":error", Severity == MessageSeverity.Error);
    }
}
