namespace SeeThroughWindows.Models
{
    /// <summary>
    /// Represents an available language for the UI localization
    /// </summary>
    public class LanguageInfo
    {
        /// <summary>
        /// Gets the culture name (e.g., "en-US", "zh-Hans")
        /// </summary>
        public string CultureName { get; }

        /// <summary>
        /// Gets the localized display name of the language
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Initializes a new instance of the LanguageInfo class
        /// </summary>
        /// <param name="cultureName">The culture identifier</param>
        /// <param name="displayName">The display text shown to users</param>
        public LanguageInfo(string cultureName, string displayName)
        {
            CultureName = cultureName;
            DisplayName = displayName;
        }

        /// <summary>
        /// Returns the display name of the language
        /// </summary>
        public override string ToString()
        {
            return DisplayName;
        }
    }
}