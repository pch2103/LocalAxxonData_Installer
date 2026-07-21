namespace LocalAxxonData_Installer.Localization;

public static class LocStrings
{
    private static AppLanguage L => AppLanguageManager.Current;

    private static string T(string ru, string en) => L == AppLanguage.Russian ? ru : en;

    public static string Next => T("Далее", "Next");
    public static string Back => T("Назад", "Back");
    public static string Cancel => T("Отмена", "Cancel");
    public static string Close => T("Закрыть", "Close");
    public static string Exit => T("Выйти", "Exit");
    public static string Warning => T("Внимание!", "Warning!");

    // StageTitleBar
    public static string StageTitleBarText => T("Local AxxonData — Мастер установки", "Local AxxonData — Install Wizard");

    // LanguagePage
    public static string LanguagePageHeading => T("Выберите язык / Select language", "Select language / Выберите язык");
    public static string LanguagePageRussian => T("Русский", "Russian");
    public static string LanguagePageEnglish => T("English", "English");
    public static string LanguagePageTestBtn => T("Тест — продукт уже установлен", "Test — product already installed");

    // AlreadyInstalledPage
    public static string AlreadyInstalledHeading => T("Local AxxonData уже установлен", "Local AxxonData is already installed");
    public static string AlreadyInstalledBody => T("Вы можете восстановить существующую установку или удалить продукт.", "You can restore the existing installation or remove the product.");
    public static string AlreadyInstalledRestore => T("Восстановление", "Restore");
    public static string AlreadyInstalledUninstall => T("Удаление", "Remove");
    public static string AlreadyInstalledSubtitle => T("Продукт уже установлен", "Product already installed");

    // ExitConfirmPage
    public static string ExitConfirmHeader => T("Прервать установку?", "Abort installation?");
    public static string ExitConfirmHeading => T("Вы уверены, что хотите выйти?", "Are you sure you want to exit?");
    public static string ExitConfirmBody => T("Установка будет прервана.", "The installation will be aborted.");
    public static string ExitConfirmContinue => T("Продолжить установку", "Continue installation");

    // WelcomePage
    public static string WelcomeSubtitle => T("Мастер установки", "Install Wizard");
    public static string WelcomeHeading => T("Добро пожаловать!", "Welcome!");
    public static string WelcomeBody => T("Этот мастер установит Local AxxonData на ваш компьютер. Перед началом убедитесь, что в BIOS включена виртуализация (Intel VT-x / AMD-V).", "This wizard will install Local AxxonData on your computer. Before proceeding, make sure virtualization is enabled in BIOS (Intel VT-x / AMD-V).");
    public static string WelcomeWarningHeading => T("Требуется перезагрузка", "Reboot required");
    public static string WelcomeWarningBody => T("Установка выполняется в две фазы. После первой фазы компьютер будет перезагружен, и мастер продолжит работу автоматически.", "Installation runs in two phases. After the first phase, the computer will reboot and the wizard will continue automatically.");
    public static string WelcomeInfoHeader => T("Что вы получите", "What you get");
    public static string WelcomeInfoBody => T("После установки Local AxxonData будет запущен на локальном Docker-окружении и доступен через браузер.", "After installation, Local AxxonData will run on a local Docker environment and be accessible through a browser.");

    // InstallDirPage
    public static string InstallDirHeader => T("Директория установки", "Installation Directory");
    public static string InstallDirHeading => T("Выберите папку для установки", "Choose installation folder");
    public static string InstallDirBrowse => T("Обзор…", "Browse…");
    public static string InstallDirFreeFmt => T("Доступно: {0}", "Available: {0}");
    public static string InstallDirRequired => T("Требуется: 10 ГБ свободного места.", "Required: 10 GB free space.");
    public static string InstallDirInfoHeader => T("Директория установки", "Installation Directory");
    public static string InstallDirInfoBody => T("Выберите папку, в которую будет установлен Local AxxonData и все зависимые компоненты.", "Choose the folder where Local AxxonData and all dependent components will be installed.");
    public static string InstallDirPickerTitle => T("Выберите папку для установки", "Choose installation folder");

    // SmtpPage
    public static string SmtpHeader => T("Настройки SMTP", "SMTP Settings");
    public static string SmtpHeading => T("Настройка почтового сервера (необязательно)", "Mail server configuration (optional)");
    public static string SmtpServerLabel => T("SMTP-сервер", "SMTP server");
    public static string SmtpPortLabel => T("Порт", "Port");
    public static string SmtpEncryptionLabel => T("Шифрование", "Encryption");
    public static string SmtpUsernameLabel => T("Имя пользователя", "Username");
    public static string SmtpPasswordLabel => T("Пароль", "Password");
    public static string SmtpSenderLabel => T("Email отправителя", "Sender email");
    public static string SmtpNoEncryption => T("Без шифрования", "No encryption");

    // SummaryPage
    public static string SummaryHeader => T("Готово к установке", "Ready to Install");
    public static string SummaryHeading => T("Проверьте параметры установки", "Review installation settings");
    public static string SummaryInfoHeader => T("Параметры установки", "Installation settings");
    public static string SummaryDirFmt => T("Директория: {0}", "Directory: {0}");
    public static string SummarySmtpConfigured => T("SMTP: настроен", "SMTP: configured");
    public static string SummarySmtpNotConfigured => T("SMTP: не настроен", "SMTP: not configured");
    public static string SummaryPasswordNote => T("Начальный пароль: будет запрошен после установки", "Initial password: will be prompted after installation");
    public static string SummaryRebootWarning => T("Установка потребует перезагрузки компьютера. Сохраните все открытые документы.", "Installation requires a computer reboot. Save all open documents.");
    public static string SummaryInfoHeader2 => T("Готово к началу установки", "Ready to start installation");
    public static string SummaryInfoBody => T("Нажмите «Установить», чтобы начать. Первая фаза подготовит систему, после чего потребуется перезагрузка.", "Click 'Install' to start. The first phase will prepare the system, then a reboot will be required.");
    public static string SummaryInstall => T("Установить", "Install");

    // ProgressPage
    public static string ProgressHeader => T("Установка: Фаза 1", "Installation: Phase 1");
    public static string ProgressSubtitle => T("Выполняется установка компонентов…", "Installing components…");
    public static string ProgressBody => T("Пожалуйста, не выключайте компьютер.", "Please do not turn off your computer.");

    // RebootPage
    public static string RebootHeader => T("Требуется перезагрузка", "Reboot Required");
    public static string RebootSubtitle => T("Первая фаза завершена", "Phase 1 complete");
    public static string RebootHeading => T("Фаза 1 завершена!", "Phase 1 complete!");
    public static string RebootCountdownLabel => T("Перезагрузка через", "Reboot in");
    public static string RebootSecFmt => T("{0} сек.", "{0} sec.");
    public static string RebootDeferred => T("отложено", "deferred");
    public static string RebootInfo => T("После перезагрузки мастер продолжит работу автоматически.", "After reboot, the wizard will continue automatically.");
    public static string RebootDefer => T("Отложить", "Defer");
    public static string RebootNow => T("Перезагрузить", "Reboot now");

    // ResumeProgressPage
    public static string ResumeHeader => T("Установка: Фаза 2", "Installation: Phase 2");
    public static string ResumeSubtitle => T("Продолжение установки после перезагрузки…", "Continuing installation after reboot…");
    public static string ResumeBody => T("Загрузка Docker-образов может занять несколько минут.", "Downloading Docker images may take a few minutes.");

    // RestoreProgressPage
    public static string RestoreHeader => T("Восстановление", "Restore");
    public static string RestoreSubtitle => T("Восстановление компонентов…", "Restoring components…");
    public static string RestoreBody => T("Восстановление…", "Restoring…");

    // FinishPage
    public static string FinishInstallHeader => T("Установка завершена", "Installation Complete");
    public static string FinishInstallSubtitle => T("Local AxxonData готов к работе", "Local AxxonData is ready");
    public static string FinishInstallTitle => T("Успешно установлено!", "Successfully installed!");
    public static string FinishInstallDesc => T("Local AxxonData установлен и готов к использованию.", "Local AxxonData is installed and ready to use.");
    public static string FinishInstallFootnote => T("Зарегистрируйте пользователя и подтвердите email.", "Register a user and confirm your email.");
    public static string FinishInstallPasswordHeader => T("Начальный пароль", "Initial password");
    public static string FinishInstallPasswordBody => T("При первом входе используйте логин admin и пароль, указанный в окне терминала.", "On first login, use admin and the password shown in the terminal window.");
    public static string FinishOpenBrowser => T("Открыть в браузере", "Open in browser");
    public static string FinishUrl => T("http://localhost:8080", "http://localhost:8080");

    // FinishPage - Restore mode
    public static string FinishRestoreHeader => T("Восстановление завершено", "Restore Complete");
    public static string FinishRestoreSubtitle => T("Local AxxonData восстановлен", "Local AxxonData restored");
    public static string FinishRestoreTitle => T("Восстановление завершено!", "Restore complete!");
    public static string FinishRestoreDesc => T("Local AxxonData восстановлен и готов к работе.", "Local AxxonData restored and ready to use.");
    public static string FinishRestoreFootnote => T("Восстановление прошло успешно.", "Restore completed successfully.");

    // FinishPage - Uninstall mode
    public static string FinishUninstallHeader => T("Удаление завершено", "Uninstall Complete");
    public static string FinishUninstallSubtitle => T("Local AxxonData удалён", "Local AxxonData removed");
    public static string FinishUninstallTitle => T("Удаление завершено", "Uninstall complete");
    public static string FinishUninstallDesc => T("Local AxxonData удалён с компьютера.", "Local AxxonData has been removed from your computer.");
    public static string FinishUninstallCard => T("Продукт удалён", "Product removed");
    public static string FinishUninstallFootnote => T("Все данные удалены.", "All data has been removed.");

    // ErrorPage
    public static string ErrorHeader => T("Ошибка установки", "Installation Error");
    public static string ErrorSubtitle => T("Произошла критическая ошибка", "A critical error occurred");
    public static string ErrorHeading => T("Установка прервана", "Installation aborted");
    public static string ErrorTitle => T("Ошибка: не удалось запустить Docker-демон", "Error: failed to start Docker daemon");
    public static string ErrorDesc => T("Убедитесь, что Docker Desktop установлен и запущен. После исправления проблемы запустите установку заново.", "Make sure Docker Desktop is installed and running. After fixing the issue, restart the installation.");
    public static string ErrorCodeBlock => T("Код ошибки: 0x80070001", "Error code: 0x80070001");
    public static string ErrorLogInfo => T("Проверьте журнал установки для получения подробной информации.", "Check the installation log for details.");
    public static string ErrorShowLog => T("Показать журнал", "Show log");
    public static string ErrorClose => T("Закрыть мастер", "Close wizard");

    // UninstallPage
    public static string UninstallHeader => T("Удаление Local AxxonData", "Remove Local AxxonData");
    public static string UninstallHeading => T("Удаление программы", "Remove program");
    public static string UninstallConfirmText => T("Вы действительно хотите удалить Local AxxonData и все связанные компоненты? Это действие удалит все данные, включая конфигурации и базы данных.", "Are you sure you want to remove Local AxxonData and all related components? This action will delete all data, including configurations and databases.");
    public static string UninstallWarningHeader => T("Внимание!", "Warning!");
    public static string UninstallWarningBody => T("Это действие необратимо. Все данные будут безвозвратно удалены.", "This action is irreversible. All data will be permanently deleted.");
    public static string UninstallProgressLabel => T("Выполняется удаление…", "Uninstalling…");
    public static string UninstallAction => T("Удалить", "Remove");
}
