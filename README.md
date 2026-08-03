# Local AxxonData Installer

Настольный установщик Local AxxonData на .NET 8, Avalonia 11.3.8 и Eremex
1.3.62. Интерфейс поддерживает светлую и тёмную темы и использует автономную
копию `ONE.PSIM.DesignSystem`.

- Репозиторий приложения: <https://github.com/pch2103/LocalAxxonData_Installer>
- Актуальная дизайн-система: <https://github.com/pch2103/ONE.PSIM.DesignSystem>
- Установленная версия ядра: `0.1.0-preview.27`

## Архитектура интерфейса

Приложение сохраняет code-behind архитектуру мастера без MVVM и DI. Общие
темы, ресурсы и контролы находятся в каталоге
`LocalAxxonData_Installer/DesignSystem/` и подключаются одной строкой в
`App.axaml`:

```xml
<StyleInclude Source="/DesignSystem/Eremex/Themes/ONEEremexTheme.axaml" />
```

Это копируемое ядро, а не ссылка на соседний репозиторий или NuGet-пакет.
Приложение можно собирать независимо от исходного репозитория дизайн-системы.

Вне ядра остаются только части самого продукта:

- экраны, навигация и сценарии установщика в `Views/`;
- локализация в `Localization/`;
- логотип и фоновые изображения в `Assets/`;
- композиционные контролы `StageTitleBar` и `ColorBandView`;
- специальная карточка таймера перезагрузки, собранная из системных ресурсов.

Локальных каталогов `Styles/`, `Resources/` и `AppTheme/` нет: текущему
приложению не потребовались собственные стили стандартных Avalonia/Eremex
контролов.

## Темы и системные компоненты

Стартовая тема — Dark. Кнопка в заголовке переключает Dark/Light без
перезапуска. Все зависящие от темы цвета берутся через `DynamicResource`.

Приложение использует системные компоненты:

- `MessageBlock` для Info, Warning, Error и Success сообщений;
- `ThemeToggleButton` для переключения темы;
- `PasswordEditorAssist` для Eremex-поля пароля с кнопкой показа;
- системные роли кнопок, редакторов, типографики и progress bar.

## Обновление дизайн-системы

1. Скачать нужный релиз или ветку `master` репозитория
   [ONE.PSIM.DesignSystem](https://github.com/pch2103/ONE.PSIM.DesignSystem).
2. В дизайн-системе выполнить `scripts/Export-PortableThemes.ps1` либо взять
   уже подготовленный каталог `portable/Eremex/DesignSystem`.
3. Полностью заменить каталог
   `LocalAxxonData_Installer/DesignSystem/` содержимым этого снимка. Не
   накладывать новую версию поверх старой выборочным копированием.
4. Проверить версии в `DesignSystem/manifest.json` и совместимость package
   references в `.csproj`.
5. Собрать Debug и Release, затем визуально проверить обе темы и сценарии
   мастера.

Файлы внутри `DesignSystem/` не изменяются локально. Универсальное исправление
сначала вносится в `ONE.PSIM.DesignSystem`, после чего снимок экспортируется и
копируется повторно. Необходимая продуктовая кастомизация должна находиться
вне `DesignSystem/` и быть явно документирована.

## Структура проекта

```text
LocalAxxonData_Installer/
├── Assets/                 продуктовые изображения
├── DesignSystem/           полностью заменяемое ядро preview.27
│   ├── Core/
│   ├── Eremex/
│   └── manifest.json
├── Localization/           RU/EN строки
├── Views/                  экраны и code-behind навигация
├── App.axaml
└── LocalAxxonData_Installer.csproj
```

## Сборка и запуск

```powershell
dotnet build .\LocalAxxonData_Installer\LocalAxxonData_Installer.csproj -c Debug
dotnet run --project .\LocalAxxonData_Installer\LocalAxxonData_Installer.csproj
```

Для восстановления Eremex-пакетов должен быть настроен коммерческий NuGet
feed и лицензия Eremex.
