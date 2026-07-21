namespace LocalAxxonData_Installer.Localization;

public enum AppLanguage
{
    Russian,
    English
}

public static class AppLanguageManager
{
    public static AppLanguage Current { get; set; } = AppLanguage.Russian;
}
