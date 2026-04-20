using Microsoft.Win32;
using System.Globalization;
using System.Resources;
using SeeThroughWindows.Models; // 引用独立的 LanguageInfo 模型

namespace SeeThroughWindows.Services
{
    /// <summary>
    /// Interface for localization service providing multi-language support
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Event fired when the current language is changed
        /// </summary>
        event EventHandler? LanguageChanged;

        /// <summary>
        /// Gets the current culture name (e.g., "en-US", "zh-Hans")
        /// </summary>
        string CurrentCultureName { get; }

        /// <summary>
        /// Sets the current UI language
        /// </summary>
        /// <param name="cultureName">Culture name (e.g., "en-US", "zh-Hans")</param>
        void SetLanguage(string cultureName);

        /// <summary>
        /// Gets a localized string for the specified resource key
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <returns>Localized string</returns>
        string GetString(string key);

        /// <summary>
        /// Gets a list of available languages
        /// </summary>
        /// <returns>Collection of LanguageInfo objects</returns>
        IEnumerable<LanguageInfo> GetAvailableLanguages();

        /// <summary>
        /// Saves the language preference to the registry
        /// </summary>
        /// <param name="cultureName">Culture name to save</param>
        void SaveLanguagePreference(string cultureName);

        /// <summary>
        /// Loads the saved language preference from the registry
        /// </summary>
        /// <returns>Saved culture name, or current system culture if not found</returns>
        string LoadLanguagePreference();
    }

    /// <summary>
    /// Implementation of the localization service using .resx resource files
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        // Registry path should match existing application settings path
        private const string REGROOT = "Software\\MOBZystems\\MOBZXRay\\";
        private const string LANGUAGE_KEY = "Language";

        private ResourceManager? _resourceManager;
        private CultureInfo _currentCulture;

        public event EventHandler? LanguageChanged;

        public string CurrentCultureName => _currentCulture.Name;

        public LocalizationService()
        {
            // Initialize resource manager with the base name of the resource files
            // Note: The resource files should be placed in the "Resources" folder
            // and have root namespace "SeeThroughWindows.Resources.Strings"
            _resourceManager = new ResourceManager("SeeThroughWindows.Resources.Strings", typeof(LocalizationService).Assembly);
            
            // Start with the current system UI culture
            _currentCulture = CultureInfo.CurrentUICulture;
        }

        public void SetLanguage(string cultureName)
        {
            if (string.IsNullOrEmpty(cultureName))
                return;

            try
            {
                var newCulture = new CultureInfo(cultureName);
                
                // Only update if culture actually changed
                if (_currentCulture.Name.Equals(newCulture.Name, StringComparison.OrdinalIgnoreCase))
                    return;

                _currentCulture = newCulture;
                
                // Apply to current thread
                CultureInfo.CurrentUICulture = _currentCulture;
                CultureInfo.CurrentCulture = _currentCulture;

                // Notify subscribers that language has changed
                LanguageChanged?.Invoke(this, EventArgs.Empty);
            }
            catch (CultureNotFoundException)
            {
                // If culture is not supported, fallback to invariant or keep current
                // Log error if needed, but silently ignore for production
            }
        }

        public string GetString(string key)
        {
            if (_resourceManager == null)
                return key;

            try
            {
                string? result = _resourceManager.GetString(key, _currentCulture);
                return result ?? key;
            }
            catch
            {
                // Return the key itself as fallback
                return key;
            }
        }

        public IEnumerable<LanguageInfo> GetAvailableLanguages()
        {
            // The display names are themselves localized strings
            yield return new LanguageInfo("en-US", GetString("LanguageEnglish"));
            yield return new LanguageInfo("zh-Hans", GetString("LanguageChineseSimplified"));
            // Additional languages can be added here
        }

        public void SaveLanguagePreference(string cultureName)
        {
            try
            {
                using var key = Registry.CurrentUser.CreateSubKey(REGROOT);
                key?.SetValue(LANGUAGE_KEY, cultureName);
            }
            catch
            {
                // Silently fail if registry access is denied
            }
        }

        public string LoadLanguagePreference()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(REGROOT);
                if (key != null)
                {
                    string? savedLanguage = key.GetValue(LANGUAGE_KEY) as string;
                    if (!string.IsNullOrEmpty(savedLanguage))
                    {
                        // Verify the culture name is valid
                        try
                        {
                            _ = new CultureInfo(savedLanguage);
                            return savedLanguage;
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
                // Registry access error, fallback
            }

            // Return current system UI culture as default
            return CultureInfo.CurrentUICulture.Name;
        }
    }
}