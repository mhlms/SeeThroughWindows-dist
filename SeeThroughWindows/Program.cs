using Microsoft.Win32;
using SeeThroughWindows.Infrastructure;
using SeeThroughWindows.Services;
using System.Globalization;

namespace SeeThroughWindows;

static class Program
{
    // Registry root path (same as used in SettingsManager and LocalizationService)
    private const string REGROOT = "Software\\MOBZystems\\MOBZXRay\\";

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            // --- Load language preference before any UI is created ---
            LoadLanguagePreference();

            // --- Single instance enforcement ---
            bool ok;
            Mutex m = new Mutex(true, "MOBZystems.SeeThroughWindows", out ok);

            if (!ok)
            {
                // Use localization service for the message if possible, 
                // but at this point it's not yet fully initialized.
                // We'll fallback to hardcoded English which is acceptable for this rare case.
                MessageBox.Show(null,
                    "See Through Windows is already active in the system tray!",
                    "See Through Windows",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Initialize application configuration (high DPI, etc.)
            ApplicationConfiguration.Initialize();

            // --- Set up dependency injection ---
            var serviceContainer = new ServiceContainer();
            ConfigureServices(serviceContainer);
            ServiceLocator.Initialize(serviceContainer);

            // --- Run the application ---
            var mainForm = ServiceLocator.Resolve<SeeThrougWindowsForm>();
            Application.Run(mainForm);

            GC.KeepAlive(m); // important!
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Application failed to start:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                            "SeeThroughWindows Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Loads the saved language preference from registry and applies it to the current thread.
    /// </summary>
    private static void LoadLanguagePreference()
    {
        try
        {
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(REGROOT))
            {
                string? savedLanguage = key?.GetValue("Language") as string;
                if (!string.IsNullOrEmpty(savedLanguage))
                {
                    try
                    {
                        var culture = new CultureInfo(savedLanguage);
                        CultureInfo.CurrentUICulture = culture;
                        CultureInfo.CurrentCulture = culture;
                    }
                    catch (CultureNotFoundException)
                    {
                        // Invalid culture, fallback to system default
                    }
                }
            }
        }
        catch
        {
            // Silently ignore registry errors; keep system default culture
        }
    }

    /// <summary>
    /// Configure dependency injection services
    /// </summary>
    private static void ConfigureServices(ServiceContainer container)
    {
        // --- Register localization service first (singleton) ---
        container.RegisterSingleton<ILocalizationService, LocalizationService>();

        // --- Register core services ---
        container.RegisterTransient<ISettingsManager, RegistrySettingsManager>();
        container.RegisterTransient<IWindowManager, WindowManager>();
        container.RegisterTransient<IHotkeyManager, HotkeyManager>();
        container.RegisterTransient<IUpdateChecker, GitHubUpdateChecker>();

        // Register auto-apply service with factory
        container.RegisterFactory<IAutoApplyService>(() =>
        {
            var windowManager = container.Resolve<IWindowManager>();
            return new AutoApplyService(windowManager);
        });

        // Register application service
        container.RegisterFactory<IApplicationService>(() =>
        {
            var windowManager = container.Resolve<IWindowManager>();
            var settingsManager = container.Resolve<ISettingsManager>();
            var autoApplyService = container.Resolve<IAutoApplyService>();
            return new ApplicationService(windowManager, settingsManager, autoApplyService);
        });

        // Register main form
        container.RegisterFactory<SeeThrougWindowsForm>(() =>
        {
            var applicationService = container.Resolve<IApplicationService>();
            var hotkeyManager = container.Resolve<IHotkeyManager>();
            var settingsManager = container.Resolve<ISettingsManager>();
            var updateChecker = container.Resolve<IUpdateChecker>();
            var localizationService = container.Resolve<ILocalizationService>();

            return new SeeThrougWindowsForm(
                applicationService,
                hotkeyManager,
                settingsManager,
                updateChecker,
                localizationService);
        });
    }
}