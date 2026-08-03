# AGENTS.md — Local AxxonData Installer

## Quick start

```powershell
dotnet build .\LocalAxxonData_Installer.csproj -c Debug
dotnet run --project .\LocalAxxonData_Installer.csproj
```

The Eremex packages require the configured commercial NuGet feed and license.
There are no test or lint projects; verify changes with Debug/Release builds
and a focused visual walkthrough.

## Application architecture

- .NET 8, Avalonia 11.3.8 and Eremex 1.3.62 desktop `WinExe`.
- Code-behind wizard only: do not introduce MVVM, DI or a navigation service.
- `MainWindow` extends `MxWindow` and swaps pages in
  `MainContentControl`. Pages call `MainWindow.ShowXxxPage()` through
  `VisualRoot`.
- Compiled bindings are enabled. Every binding must have a valid
  `x:DataType`.
- `Program.cs` must keep `.UseEMXServices()`.
- The fixed 700×500 borderless window, custom drag title bar and imperative
  navigation are intentional product decisions.

## Design-system ownership

The UI foundation is a vendored snapshot of `ONE.PSIM.DesignSystem`:

- source: <https://github.com/pch2103/ONE.PSIM.DesignSystem>;
- installed version: `0.1.0-preview.27`;
- snapshot path: `DesignSystem/`;
- manifest: `DesignSystem/manifest.json`;
- application entry point:
  `/DesignSystem/Eremex/Themes/ONEEremexTheme.axaml`.

`DesignSystem/` is managed generated content. Never patch it locally. A
generic correction belongs in the source design-system repository; export a
new portable snapshot and replace this directory in full afterward. The app
must not depend on a sibling checkout at build or runtime.

Do not recreate local `Styles/` or `Resources/` directories for shared UI.
There is currently no `AppTheme/`: the design system covers all standard
Avalonia/Eremex styling and the phase header resources. If a genuine
product-only override becomes necessary, create `AppTheme/AppTheme.axaml`,
load it after the design-system include, and document every key. Never copy a
system `ControlTheme`, palette or compatibility alias into it.

## Themes and reusable components

- Startup variant is Dark; Light and Dark are equally supported.
- `StageTitleBar` hosts the system `ThemeToggleButton`.
- Use `DynamicResource` for every theme-sensitive colour.
- Use `ONE.PSIM.DesignSystem.Controls.MessageBlock` for normal Info, Warning,
  Error and Success header/body feedback.
- Use `ONE.PSIM.DesignSystem.Controls.Eremex.PasswordEditorAssist` for Eremex
  password editors. Do not add `Avalonia.Xaml.Behaviors` or a local password
  behavior.
- Use system button roles such as `Primary`, `Secondary`, `Danger`, `Subtle`,
  `Large` and `IconOnlyLarge`; do not invent colour-named roles.
- Use the typography and editor contracts supplied by the snapshot. Do not
  hardcode fonts, hex colours or control-state brushes.

Application-owned UI is limited to product composition and behavior:

- `StageTitleBar` and `ColorBandView`;
- page layouts and code-behind workflow;
- localization and assets;
- the `RebootPage` countdown card, which is intentionally more complex than a
  normal `MessageBlock`.

## Screen flow

`MainWindow.axaml.cs` is the navigation hub. `SetPage()` configures the shared
title/header composition and replaces page content.

| Path | Sequence |
|---|---|
| Clean install | Language → Welcome → InstallDir → SMTP → Summary → Progress (Phase 1) → Reboot → Progress (Phase 2) → Finish |
| Existing install | AlreadyInstalled → Restore or Uninstall |
| Cross-cutting | ExitConfirm and Error |

`ProgressPage` with `ProgressMode` handles Phase1, Phase2 and Restore.
`FinishPage` with `FinishMode` handles Install, Restore and Uninstall. Preserve
the existing cancellation-token and auto-advance behavior.

## Coding conventions

- Keep filenames, classes and namespaces in PascalCase and aligned.
- Wire events in AXAML and implement private handlers in the corresponding
  code-behind file.
- Page cancel actions go through `ShowExitConfirmPage()`; the title-bar close
  action must not close the window directly.
- Prefer Eremex controls for editors and other rich controls, using the 1.3.62
  API. The Avalonia `ProgressBar` is an intentional exception.
- Keep changes focused and preserve the current code-behind architecture.
- `app.manifest` is required for the Windows window behavior and must remain.

## Updating the design system

1. Update or download `ONE.PSIM.DesignSystem`.
2. Run `scripts/Export-PortableThemes.ps1` there.
3. Replace this entire `DesignSystem/` directory with
   `portable/Eremex/DesignSystem`.
4. Confirm `manifest.json` versions match the `.csproj` package versions.
5. Build Debug and Release.
6. Walk through Dark → Light → Dark, password reveal, message severities,
   progress, uninstall/error, reboot and exit-confirm scenarios.
7. Search for forbidden legacy items: `PasswordBoxBehavior`,
   `MessageBlockView`, `Avalonia.Xaml.Behaviors`, `Classes="Red"`, `/Styles/`
   and `/Resources/ModifiedResources`.

Do not merge snapshots by copying only changed files: files removed by a newer
release would survive and produce a mixed, unsupported version.
