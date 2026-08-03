using System;
using Avalonia;
using Avalonia.Automation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using IconPath = Avalonia.Controls.Shapes.Path;

namespace ONE.PSIM.DesignSystem.Controls;

public sealed class PasswordBoxAssist : AvaloniaObject
{
    public static readonly AttachedProperty<bool> IsPasswordProperty =
        AvaloniaProperty.RegisterAttached<PasswordBoxAssist, TextBox, bool>(
            "IsPassword");

    public static readonly AttachedProperty<char> PasswordCharProperty =
        AvaloniaProperty.RegisterAttached<PasswordBoxAssist, TextBox, char>(
            "PasswordChar",
            '●');

    public static readonly AttachedProperty<bool> ShowRevealButtonProperty =
        AvaloniaProperty.RegisterAttached<PasswordBoxAssist, TextBox, bool>(
            "ShowRevealButton",
            true);

    public static readonly AttachedProperty<string> ShowPasswordLabelProperty =
        AvaloniaProperty.RegisterAttached<PasswordBoxAssist, TextBox, string>(
            "ShowPasswordLabel",
            "Show password");

    public static readonly AttachedProperty<string> HidePasswordLabelProperty =
        AvaloniaProperty.RegisterAttached<PasswordBoxAssist, TextBox, string>(
            "HidePasswordLabel",
            "Hide password");

    private static readonly AttachedProperty<PasswordBoxState?> StateProperty =
        AvaloniaProperty.RegisterAttached<PasswordBoxAssist, TextBox, PasswordBoxState?>(
            "State");

    static PasswordBoxAssist()
    {
        IsPasswordProperty.Changed.AddClassHandler<TextBox>(static (textBox, _) => Apply(textBox));
        PasswordCharProperty.Changed.AddClassHandler<TextBox>(static (textBox, _) => Apply(textBox));
        ShowRevealButtonProperty.Changed.AddClassHandler<TextBox>(static (textBox, _) => Apply(textBox));
        ShowPasswordLabelProperty.Changed.AddClassHandler<TextBox>(static (textBox, _) => UpdatePresentation(textBox));
        HidePasswordLabelProperty.Changed.AddClassHandler<TextBox>(static (textBox, _) => UpdatePresentation(textBox));
    }

    private PasswordBoxAssist()
    {
    }

    public static bool GetIsPassword(TextBox textBox) => textBox.GetValue(IsPasswordProperty);
    public static void SetIsPassword(TextBox textBox, bool value) => textBox.SetValue(IsPasswordProperty, value);

    public static char GetPasswordChar(TextBox textBox) => textBox.GetValue(PasswordCharProperty);
    public static void SetPasswordChar(TextBox textBox, char value) => textBox.SetValue(PasswordCharProperty, value);

    public static bool GetShowRevealButton(TextBox textBox) => textBox.GetValue(ShowRevealButtonProperty);
    public static void SetShowRevealButton(TextBox textBox, bool value) => textBox.SetValue(ShowRevealButtonProperty, value);

    public static string GetShowPasswordLabel(TextBox textBox) => textBox.GetValue(ShowPasswordLabelProperty);
    public static void SetShowPasswordLabel(TextBox textBox, string value) => textBox.SetValue(ShowPasswordLabelProperty, value);

    public static string GetHidePasswordLabel(TextBox textBox) => textBox.GetValue(HidePasswordLabelProperty);
    public static void SetHidePasswordLabel(TextBox textBox, string value) => textBox.SetValue(HidePasswordLabelProperty, value);

    private static void Apply(TextBox textBox)
    {
        if (!GetIsPassword(textBox))
        {
            Reset(textBox);
            return;
        }

        var state = textBox.GetValue(StateProperty);
        if (state is null)
        {
            state = CreateState(textBox);
            textBox.SetValue(StateProperty, state);
        }

        state.MaskCharacter = GetPasswordChar(textBox);
        textBox.Classes.Remove("revealPasswordButton");
        textBox.PasswordChar = state.RevealButton.IsChecked == true
            ? '\0'
            : state.MaskCharacter;

        if (GetShowRevealButton(textBox))
        {
            textBox.InnerRightContent = state.RevealButton;
        }
        else if (ReferenceEquals(textBox.InnerRightContent, state.RevealButton))
        {
            textBox.InnerRightContent = state.OriginalInnerRightContent;
        }

        UpdatePresentation(textBox);
    }

    private static PasswordBoxState CreateState(TextBox textBox)
    {
        var icon = new IconPath();
        icon.Classes.Add("PasswordRevealIcon");

        var button = new ToggleButton
        {
            Focusable = false,
            Content = icon
        };
        button.Classes.Add("IconOnlyLarge");
        button.Classes.Add("PasswordRevealButton");

        var state = new PasswordBoxState(textBox.InnerRightContent, button, icon);
        state.ClickHandler = (_, _) =>
        {
            textBox.PasswordChar = button.IsChecked == true
                ? '\0'
                : state.MaskCharacter;
            UpdatePresentation(textBox);
        };
        button.Click += state.ClickHandler;
        button.AttachedToVisualTree += (_, _) => UpdatePresentation(textBox);

        return state;
    }

    private static void UpdatePresentation(TextBox textBox)
    {
        var state = textBox.GetValue(StateProperty);
        if (state is null)
        {
            return;
        }

        var isRevealed = state.RevealButton.IsChecked == true;
        var iconKey = isRevealed ? "EyeClosedIcon" : "EyeOpenIcon";
        var label = isRevealed
            ? GetHidePasswordLabel(textBox)
            : GetShowPasswordLabel(textBox);

        if (Application.Current?.TryFindResource(iconKey, out var icon) == true &&
            icon is StreamGeometry geometry)
        {
            state.Icon.Data = geometry;
        }

        ToolTip.SetTip(state.RevealButton, label);
        AutomationProperties.SetName(state.RevealButton, label);
    }

    private static void Reset(TextBox textBox)
    {
        var state = textBox.GetValue(StateProperty);
        if (state is not null)
        {
            if (ReferenceEquals(textBox.InnerRightContent, state.RevealButton))
            {
                textBox.InnerRightContent = state.OriginalInnerRightContent;
            }

            state.RevealButton.Click -= state.ClickHandler;
            textBox.SetValue(StateProperty, null);
        }

        textBox.PasswordChar = '\0';
        textBox.Classes.Remove("revealPasswordButton");
    }

    private sealed class PasswordBoxState(
        object? originalInnerRightContent,
        ToggleButton revealButton,
        IconPath icon)
    {
        public object? OriginalInnerRightContent { get; } = originalInnerRightContent;
        public ToggleButton RevealButton { get; } = revealButton;
        public IconPath Icon { get; } = icon;
        public char MaskCharacter { get; set; } = '●';
        public EventHandler<RoutedEventArgs> ClickHandler { get; set; } = null!;
    }
}
