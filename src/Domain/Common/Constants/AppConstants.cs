namespace Domain.Common.Constants;

public static class AppConstants
{
    public const string AppName = "Dosya Bank";

    public static readonly string[] SupportedLanguages = { "en-us", "ru-ru" };

    public static readonly string DefaultLanguage = "en";

    /// <summary>
    /// Update this value when you throw new Exceptions
    /// </summary>
    public const int CurrentMaxErrorCode = 9;
}