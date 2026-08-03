using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Eremex.AvaloniaUI.Controls.Editors;
using Eremex.AvaloniaUI.Controls.Utils;
using ONE.PSIM.DesignSystem.Controls;

namespace ONE.PSIM.DesignSystem.Eremex;

public sealed class PasswordEditorAssist : AvaloniaObject
{
    public static readonly AttachedProperty<bool> IsPasswordProperty =
        AvaloniaProperty.RegisterAttached<PasswordEditorAssist, TextEditor, bool>(
            "IsPassword");

    public static readonly AttachedProperty<char> PasswordCharProperty =
        AvaloniaProperty.RegisterAttached<PasswordEditorAssist, TextEditor, char>(
            "PasswordChar",
            '●');

    public static readonly AttachedProperty<bool> ShowRevealButtonProperty =
        AvaloniaProperty.RegisterAttached<PasswordEditorAssist, TextEditor, bool>(
            "ShowRevealButton",
            true);

    static PasswordEditorAssist()
    {
        IsPasswordProperty.Changed.AddClassHandler<TextEditor>(OnIsPasswordChanged);
        PasswordCharProperty.Changed.AddClassHandler<TextEditor>(
            static (editor, _) => Apply(editor));
        ShowRevealButtonProperty.Changed.AddClassHandler<TextEditor>(
            static (editor, _) => Apply(editor));
    }

    private PasswordEditorAssist()
    {
    }

    public static bool GetIsPassword(TextEditor editor) =>
        editor.GetValue(IsPasswordProperty);

    public static void SetIsPassword(TextEditor editor, bool value) =>
        editor.SetValue(IsPasswordProperty, value);

    public static char GetPasswordChar(TextEditor editor) =>
        editor.GetValue(PasswordCharProperty);

    public static void SetPasswordChar(TextEditor editor, char value) =>
        editor.SetValue(PasswordCharProperty, value);

    public static bool GetShowRevealButton(TextEditor editor) =>
        editor.GetValue(ShowRevealButtonProperty);

    public static void SetShowRevealButton(TextEditor editor, bool value) =>
        editor.SetValue(ShowRevealButtonProperty, value);

    private static void OnIsPasswordChanged(TextEditor editor, AvaloniaPropertyChangedEventArgs change)
    {
        editor.Loaded -= OnEditorLoaded;

        if (change.NewValue is true)
        {
            editor.Loaded += OnEditorLoaded;
            Apply(editor);
        }
        else
        {
            Reset(editor);
        }
    }

    private static void OnEditorLoaded(object? sender, RoutedEventArgs e)
    {
        if (sender is TextEditor editor)
        {
            Apply(editor);
        }
    }

    private static void Apply(TextEditor editor)
    {
        if (!GetIsPassword(editor))
        {
            return;
        }

        var textBox = editor.FindVisualChild<TextBox>();
        if (textBox is null)
        {
            return;
        }

        PasswordBoxAssist.SetPasswordChar(textBox, GetPasswordChar(editor));
        PasswordBoxAssist.SetShowRevealButton(textBox, GetShowRevealButton(editor));
        PasswordBoxAssist.SetIsPassword(textBox, true);
    }

    private static void Reset(TextEditor editor)
    {
        var textBox = editor.FindVisualChild<TextBox>();
        if (textBox is null)
        {
            return;
        }

        PasswordBoxAssist.SetIsPassword(textBox, false);
    }
}
