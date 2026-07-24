# AGENTS.md — Local AxxonData Installer

## Quick start

```powershell
dotnet build
dotnet run --project LocalAxxonData_Installer
```

No test, lint, or typecheck projects exist. The only verification is building.

## Architecture

- **Single** .NET 8 / Avalonia 11.3.8 desktop app (`WinExe`), with **Eremex commercial controls** (v1.3.x).
- **Code-behind wizard** — no MVVM, no DI, no navigation service. `MainWindow` (extends Eremex `MxWindow`) holds a named `ContentControl` (`MainContentControl`). Each Page `UserControl` references `VisualRoot` to call `ShowXxxPage()` methods directly on `MainWindow`.
- **No `ViewModels/`, `Models/`, or `Services/`** directories. All logic is in `Views/*.axaml.cs`.
- **Reusable controls** exist in `Views/`: `StageTitleBar`, `ColorBandView`, `MessageBlockView` (with `MessageSeverity` enum: Info/Warning/Error), `StringNotEmptyConverter`.
- Entrypoint: `Program.cs` → `App.axaml.cs` → `MainWindow`.

## Screen flow

`MainWindow` creates each page in code-behind. Navigation is imperative.

### Main path — clean install

| Phase | Page | Header | Mode | Next → | ← Prev |
|-------|------|--------|------|--------|--------|
| A | `LanguagePage` | #1A73E8 blue | — | WelcomePage | — |
| A | `WelcomePage` | #1A73E8 blue | — | InstallDirPage | LanguagePage |
| A | `InstallDirPage` | #1A73E8 blue | — | SmtpPage | WelcomePage |
| A | `SmtpPage` | #1A73E8 blue | — | SummaryPage | InstallDirPage |
| A | `SummaryPage` | #1A73E8 blue | — | ProgressPage | SmtpPage |
| B | `ProgressPage` | #1A73E8 blue | Phase1 | RebootPage (auto) | — |
| B | `RebootPage` | #F57C00 orange | — | ProgressPage(Phase2, auto) | — |
| — | *(computer reboots — installer resumes automatically)* |||||
| C | `ProgressPage` | #1A73E8 blue | Phase2 | FinishPage (auto) | — |
| C | `FinishPage` | #2E7D32 green | Install/Restore/Uninstall | — | — |

### Reinstall path

| Page | Header | Purpose |
|------|--------|---------|
| `AlreadyInstalledPage` | #1565C0 dark blue | Entry for reinstall — offers Restore or Uninstall |
| `UninstallPage` | #C62828 red | Confirmation before uninstall |

### Cross-cutting (appear from any page)

| Page | Header | Purpose |
|------|--------|---------|
| `ExitConfirmPage` | #616161 gray | Confirm exit dialog (via ✕ or Cancel) — returns to previous page or closes |
| `ErrorPage` | #C62828 red | Error display with log path |

## Styling

- Theme: `Eremex.Avalonia.Themes.DeltaDesign` (Dark variant)
- Styles loaded in `App.axaml` in order: DeltaDesignTheme → Colors → Brushes → Typography → Controls → Layout
- Resource overrides: `ModifiedResourcesColor.axaml` before `ModifiedResources.axaml`
- Window: 700×500 fixed, `WindowStartupLocation="CenterScreen"`, borderless (`SystemDecorations="None"`), custom drag title bar
- Font: Segoe UI (desktop default)
- Typography tokens: `ONE-H0-Bold` (ColorBand), `ONE-H2-Bold` (headings), `ONE-H3-Bold`/`Regular` (buttons/body), `ONE-Paragraph-Bold`/`Regular` (info blocks), `One-Subs-Bold/Regular` (field labels) — see full table in DeltaDesign section
- All colours in XAML use `{DynamicResource}` — never raw hex

## Conventions

- **Compiled bindings enabled** (`AvaloniaUseCompiledBindingsByDefault=true`) — use `x:DataType` and avoid reflection-based binding.
- **Unified naming convention** — all page files use PascalCase: `WelcomePage`, `InstallDirPage`, `SmtpPage`, `SummaryPage`, `ProgressPage`, `RebootPage`, etc. Class names match filenames.
- Navigation is imperative — do not introduce a ViewModel or navigation service.
- Each page's `OnCancelClick` navigates to `ExitConfirmPage` via `mainWindow.ShowExitConfirmPage()`.
- `ExitConfirmPage` stores `_previousContent` and restores it on "Продолжить".
- `StageTitleBar` ✕ button calls `ShowExitConfirmPage()` (not `CloseWindow()` directly).
- **Navigation hub** is `MainWindow.axaml.cs` — all `ShowXxxPage()` methods call `SetPage(page, brushKey, header, body, bgImage)` which sets `MainContentControl.Content` and configures the shared `StageTitleBar` + `ColorBandView`.
- **Page header colours** use `{DynamicResource PageHeaderXxxBrush}` resource keys (`Blue`, `Orange`, `Green`, `DarkBlue`, `Red`, `Gray`) defined in `Styles/Colors.axaml` + `Styles/Brushes.axaml`.
- **Single `ProgressPage`** with `ProgressMode` enum (`Phase1`/`Phase2`/`Restore`) handles all progress scenarios.
- **Single `FinishPage`** with `FinishMode` enum (`Install`/`Restore`/`Uninstall`) handles all finish scenarios.
- **Unified message block**: use `MessageBlockView` with `Severity="Info|Warning|Error"` instead of separate `InfoBlockView`/`ErrorBlockView`/`WarningBlockView` (these no longer exist).

## Gotchas

- Eremex NuGet packages require a commercial license — `dotnet restore` may fail without a configured Eremex NuGet feed or license.
- Only **Debug** configuration includes `Avalonia.Diagnostics` (see csproj conditional).
- `app.manifest` is required on Windows for window transparency — do not remove.
- Password field uses `Avalonia.Xaml.Behaviors` (`PasswordBoxBehavior`) + Eremex `TextEditor` with built-in reveal button class `revealPasswordButton`.
- `ProgressPage` (single, with `ProgressMode` Phase1/Phase2/Restore) auto-advances on completion — use `CancellationTokenSource` to cancel on exit.
- `RebootPage` has a 60-second countdown timer with "Отложить"/"Перезагрузить" buttons.

## AI Behavior Rules

- **CRITICAL: NEVER introduce MVVM or Dependency Injection (DI).** Keep all logic strictly inside Code-behind (`*.axaml.cs`).
- **Do not rename or refactor** existing files, classes, or namespaces.
- **Default to Small Diffs.** Do not rewrite whole files to change a single behavior or UI property.
- **Preserve semantic colours** (`#FF0000` / `#20D300` / `#FF8A00`) — these are intentional project decisions.

## Avalonia & Eremex Controls Coding Rules

- **Strict Compiled Bindings:** Since `AvaloniaUseCompiledBindingsByDefault=true` is enabled, you MUST always specify `x:DataType` when using any `{Binding ...}` in AXAML files.
- **Eremex UI Controls:** When modifying data trees, tables, or complex editors, prioritize using **Eremex controls** over standard Avalonia controls. Match the `v1.3.x` API.
- **Event Handlers:** Add UI event handlers in AXAML (e.g., `Click="OnNextClick"`) and implement their private methods inside the corresponding `.axaml.cs` file.
- **Standard Avalonia `ProgressBar`** is used (not Eremex) because the reference project does not use an Eremex progress control.

## Build & Validation Workflow

- **Only Use `dotnet build`:** To verify changes, use ONLY `dotnet build`. Do not try to run tests (`dotnet test`) or linters, as they do not exist in this project.

## Eremex Controls Quick Reference (v1.3.x)

### MxWindow (fixed 700×500, centered)
```xml
<mx:MxWindow xmlns:mx="using:Eremex.Avalonia.Controls"
             SystemDecorations="None"
             WindowStartupLocation="CenterScreen"
             Width="700" Height="500">
    <ContentControl Name="MainContentControl" />
</mx:MxWindow>
```

### TextEditor (single-line text)
```xml
<mx:TextEditor Name="Editor" Watermark="placeholder" Text="{Binding Value}" />
```
Password mode: use `PasswordBoxBehavior` from `Avalonia.Xaml.Behaviors`.

### ButtonEditor / CheckEditor / ComboBoxEditor / DateEditor / SpinEditor / MemoEditor

| Control | Key Property | Notes |
|---------|-------------|-------|
| `ButtonEditor` | `Content`, `Click` | Button with configurable look |
| `CheckEditor` | `IsChecked`, `Content` | Checkbox |
| `ComboBoxEditor` | `ItemsSource`, `SelectedItem` | Dropdown |
| `DateEditor` | `DateTime`, `MaskType` | Date picker |
| `SpinEditor` | `Value`, `MinValue`, `MaxValue` | Numeric up-down |
| `MemoEditor` | `Text`, `AcceptsReturn` | Multi-line text |

### MxMessageBox
```csharp
await MxMessageBox.ShowAsync("text", "title",
    MxMessageBoxButtons.YesNo, MxMessageBoxIcon.Question);
```

### Program.cs — required setup
```csharp
BuildAvaloniaApp()
    .UsePlatformDetect()
    .UseEMXServices()                // Eremex service registration
    .LogToTrace()
    .StartWithClassicDesktopLifetime(args);
```

## DeltaDesign Theme Resource Overrides

### Style loading order in App.axaml
```
DeltaDesignTheme (base) → Colors → Brushes → Typography → Controls → Layout
```

### Resource dictionaries
```xml
<ResourceInclude Source="avares://.../Resources/ModifiedResourcesColor.axaml" />
<ResourceInclude Source="avares://.../Resources/ModifiedResources.axaml" />
```

### Typography tokens (Segoe UI)
| Token | Size | Weight | Usage |
|-------|------|--------|-------|
| `ONE-H0-Bold` | 18px | Bold | ColorBand header text |
| `ONE-H2-Bold` | 18px | Bold | Page headings |
| `ONE-H3-Bold` | 14px | Bold | Button labels, section headers |
| `ONE-H3-Regular` | 14px | Regular | Body text, descriptions |
| `ONE-Paragraph-Bold` | 13px | Bold | Message block headers |
| `ONE-Paragraph-Regular` | 13px | Regular | Info/error/warning body text |
| `One-Subs-Bold` | 13px | Bold | Field labels |
| `One-Subs-Regular` | 13px | Regular | Field values |

### Semantic colours (project decisions — differ from Figma)
| Resource Key | Hex | Usage |
|-------------|-----|-------|
| `SemanticColors/DangerBrush` | `#FF0000` | Errors, destructive actions |
| `SemanticColors/SuccessBrush` | `#20D300` | Success, completion |
| `SemanticColors/WarningBrush` | `#FF8A00` | Warnings, countdowns |

### Page header colours by phase
| Phase | Hex | Pages |
|-------|-----|-------|
| A (setup) | `#1A73E8` blue | Language → Summary |
| B (reboot) | `#F57C00` orange | Reboot |
| C (finish) | `#2E7D32` green | Finish |
| Reinstall | `#1565C0` dark blue | AlreadyInstalled |
| Uninstall/Error | `#C62828` red | Uninstall, Error |
| Exit confirm | `#616161` gray | ExitConfirm |

### Common DeltaDesign resource keys (for ModifiedResources)
- `DefaultFont`, `WindowBackground`, `ContentBackground`, `BorderBrush`
- `TextBrush`, `DisabledTextBrush`, `AccentBrush`, `ErrorBrush`

### Rule
**All colours in XAML must use `{DynamicResource}` — never raw hex.**

## Navigation & Wizard Patterns

```
MainWindow (MxWindow, 700×500)
  ├── StageTitleBar (drag + close → ExitConfirm)
  ├── ColorBandView (colored header + logo + bg image)
  └── ContentControl (MainContentControl)  ← Content swapped in code-behind
        └── [Page] (UserControl)
              ├── [page content with MessageBlockView]
              └── footer (Next/Back/Cancel buttons)
```

Navigation is driven by `SetPage()` helper in `MainWindow.axaml.cs`:

```csharp
private void SetPage(UserControl page, string brushKey,
    string? header = null, string? body = null, string? bgImage = null)
```

Each `ShowXxxPage()` method creates the page and calls `SetPage()` with the correct colour key, header text, subtitle, and background image. See `MainWindow.axaml.cs` for the full per-page config.

### Adding a new page
1. Create `NewPage.axaml` + `NewPage.axaml.cs` (UserControl, content + footer only — no StageTitleBar/ColorBandView)
2. Add `ShowNewPage()` to `MainWindow.axaml.cs` that calls `SetPage(new NewPage(), brushKey, header, body, bgImage)`
3. Wire navigation from previous page's `OnNextClick`
4. Add localization strings to `LocStrings.cs`

### Page code-behind pattern
```csharp
public partial class SomePage : UserControl
{
    public SomePage() { InitializeComponent(); UpdateLanguage(); }
    private void UpdateLanguage() { /* populate from LocStrings */ }
    private void OnNextClick(object? s, RoutedEventArgs e)
        { if (VisualRoot is MainWindow mw) mw.ShowXxxPage(); }  // calls SetPage() internally
    private void OnCancelClick(object? s, RoutedEventArgs e)
        { if (VisualRoot is MainWindow mw) mw.ShowExitConfirmPage(); }
}
```

### ExitConfirmPage restores previous content
```csharp
_previousContent = MainContentControl.Content;
MainContentControl.Content = new ExitConfirmPage();
// On continue:
MainContentControl.Content = _previousContent;
```

### Progress auto-advance with CancellationTokenSource
```csharp
private CancellationTokenSource? _cts;
// On cancel:
_cts?.Cancel(); if (VisualRoot is MainWindow mw) mw.ShowExitConfirmPage();
```
