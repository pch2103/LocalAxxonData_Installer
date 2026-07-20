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
- **Reusable controls** exist in `Views/`: `StageTitleBar`, `ColorBandView`, `InfoBlockView`, `ErrorBlockView`, `WarningBlockView`.
- Entrypoint: `Program.cs` → `App.axaml.cs` → `MainWindow`.

## Screen flow

`MainWindow` creates each page in code-behind. Navigation is imperative.

### Main path — clean install

| Phase | Page | Header | Next → | ← Prev |
|-------|------|--------|--------|--------|
| A | `LanguagePage` | #1A73E8 blue | WelcomePage | — |
| A | `WelcomePage` | #1A73E8 blue | InstallDirPage | LanguagePage |
| A | `InstallDirPage` | #1A73E8 blue | SmtpPage | WelcomePage |
| A | `SmtpPage` | #1A73E8 blue | SummaryPage | InstallDirPage |
| A | `SummaryPage` | #1A73E8 blue | ProgressPage | SmtpPage |
| B | `ProgressPage` | #1A73E8 blue | RebootPage (auto) | — |
| B | `RebootPage` | #F57C00 orange | ResumeProgressPage (auto) | — |
| — | *(computer reboots — installer resumes automatically)* ||||
| C | `ResumeProgressPage` | #1A73E8 blue | FinishPage (auto) | — |
| C | `FinishPage` | #2E7D32 green | — | — |

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
- Styles loaded in `App.axaml` in order: DeltaDesignTheme → CustomDarkTheme → ControlsCustomStyles → WindowStyles
- Resource overrides: `ModifiedResourcesColor.axaml` before `ModifiedResources.axaml`
- Window: 700×500 fixed, borderless (`SystemDecorations="None"`), `SizeToContent="WidthAndHeight"`, custom drag title bar
- Font: Segoe UI (desktop default)
- Typography tokens: `ONE-H2-Bold` (headings), `ONE-H3-Regular` (body), `ONE-Paragraph-Regular` (info blocks), `One-Subs-Bold/Regular` (field labels)
- All colours in XAML use `{DynamicResource}` — never raw hex

## Conventions

- **Compiled bindings enabled** (`AvaloniaUseCompiledBindingsByDefault=true`) — use `x:DataType` and avoid reflection-based binding.
- **Unified naming convention** — all page files use PascalCase: `WelcomePage`, `InstallDirPage`, `SmtpPage`, `SummaryPage`, `ProgressPage`, `RebootPage`, etc. Class names match filenames.
- Navigation is imperative — do not introduce a ViewModel or navigation service.
- Each page's `OnCancelClick` navigates to `ExitConfirmPage` via `mainWindow.ShowExitConfirmPage()`.
- `ExitConfirmPage` stores `_previousContent` and restores it on "Продолжить".
- `StageTitleBar` ✕ button calls `ShowExitConfirmPage()` (not `CloseWindow()` directly).

## Gotchas

- Eremex NuGet packages require a commercial license — `dotnet restore` may fail without a configured Eremex NuGet feed or license.
- Only **Debug** configuration includes `Avalonia.Diagnostics` (see csproj conditional).
- `app.manifest` is required on Windows for window transparency — do not remove.
- Password field uses `Avalonia.Xaml.Behaviors` (`PasswordBoxBehavior`) + Eremex `TextEditor` with built-in reveal button class `revealPasswordButton`.
- Progress pages (`ProgressPage`, `ResumeProgressPage`) auto-advance on completion — use `CancellationTokenSource` to cancel on exit.
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
