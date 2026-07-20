# Local AxxonData Installer

Avalonia desktop installer for the Local AxxonData product. Windows **700×500** fixed window, dark theme, two-phase installation with automatic reboot.

## Tech Stack

- **.NET 8** / **Avalonia 11.3.8** + **Eremex DeltaDesign** v1.3.x
- Code-behind wizard architecture (no MVVM, no DI)
- Dark theme only — Segoe UI font

## Screens Overview

13 screens grouped by phase. Header color indicates the screen purpose.

### Phase A — Settings Collection (blue `#1A73E8`)

User enters installation parameters before setup begins.

| Screen | Description |
|--------|-------------|
| **LanguagePage** | Language selection (RU / EN). Shown only on clean install. |
| **WelcomePage** | Welcome message with BIOS virtualization reminder and two-phase install note. |
| **InstallDirPage** | Target directory picker with free space check. |
| **SmtpPage** | SMTP server configuration (optional). Scrollable form with host, port, encryption, credentials. Password field with reveal button. |
| **SummaryPage** | Parameter review before installation starts. Shows install path, SMTP status, initial password info. Includes reboot warning. |

### Phase B — System Preparation (blue → orange)

First install phase followed by mandatory reboot.

| Screen | Description |
|--------|-------------|
| **ProgressPage** | Phase 1 progress bar. Simulated installation with 5‑second animation, auto‑advances to RebootPage. Cancel button aborts installation. |
| **RebootPage** | 60‑second countdown to reboot (orange `#F57C00` header). "Отложить" pauses, "Перезагрузить" triggers immediate restart. Auto‑resumes to Phase 2 after reboot. |

### Phase C — Final Setup (blue → green)

Resumes automatically after reboot.

| Screen | Description |
|--------|-------------|
| **ResumeProgressPage** | Phase 2 progress bar. Docker image download simulation, 8‑second animation. |
| **FinishPage** | Success screen (green `#2E7D32` header). Large checkmark icon, product URL. "Закрыть" button exits the installer. |

### Reinstall & Recovery

| Screen | Header | Description |
|--------|--------|-------------|
| **AlreadyInstalledPage** | Dark blue `#1565C0` | Entry point when product is already detected. Offers Restore (re‑runs ProgressPage) or Uninstall. |
| **UninstallPage** | Red `#C62828` | Confirmation dialog with irreversible‑action warning. "Удалить" removes everything, "← Назад" returns to options. |

### Cross‑cutting Dialogs

| Screen | Header | Description |
|--------|--------|-------------|
| **ExitConfirmPage** | Gray `#616161` | Confirm‑exit overlay triggered by ✕ button or Cancel on any page. "Продолжить" returns to previous screen, "Выйти" closes the window. |
| **ErrorPage** | Red `#C62828` | Error display with description and log file path. "Закрыть" exits the installer. |

## Project Structure

```
LocalAxxonData_Installer/
├── Assets/
│   ├── axxon-logo.svg
│   └── header_back_*.png        (6 header background images)
├── Resources/
│   ├── ModifiedResourcesColor.axaml
│   └── ModifiedResources.axaml
├── Styles/
│   ├── WindowStyles.axaml
│   ├── CustomDarkTheme.axaml
│   └── ControlsCustomStyles.axaml
├── Views/
│   ├── MainWindow.axaml / .cs   — navigation hub, 13 ShowXxxPage methods
│   ├── StageTitleBar.axaml / .cs
│   ├── ColorBandView.axaml / .cs
│   ├── InfoBlockView.axaml / .cs
│   ├── ErrorBlockView.axaml / .cs
│   ├── WarningBlockView.axaml / .cs
│   ├── PasswordBoxBehavior.cs
│   ├── LanguagePage.axaml / .cs
│   ├── WelcomePage.axaml / .cs
│   ├── InstallDirPage.axaml / .cs
│   ├── SmtpPage.axaml / .cs
│   ├── SummaryPage.axaml / .cs
│   ├── ProgressPage.axaml / .cs
│   ├── RebootPage.axaml / .cs
│   ├── ResumeProgressPage.axaml / .cs
│   ├── FinishPage.axaml / .cs
│   ├── AlreadyInstalledPage.axaml / .cs
│   ├── UninstallPage.axaml / .cs
│   ├── ExitConfirmPage.axaml / .cs
│   └── ErrorPage.axaml / .cs
├── App.axaml / .cs
├── Program.cs
├── app.manifest
├── LocalAxxonData_Installer.csproj
├── LocalAxxonData_Installer.sln
└── AGENTS.md
```

## Build & Run

```powershell
dotnet build
dotnet run --project LocalAxxonData_Installer
```
