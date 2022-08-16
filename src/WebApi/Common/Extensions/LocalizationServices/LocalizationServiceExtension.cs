using Domain.Common.Constants;

namespace WebApi.Common.Extensions.LocalizationServices;

public static class LocalizationServiceExtension
{
    internal static void AddLocalizationService(this IServiceCollection services)
    {
        services.AddLocalization();
    }
    
    internal static void UseLocalization(this IApplicationBuilder app)
    {
        var supportedCultures = AppConstants.SupportedLanguages;
        var localizationOptions =
            new RequestLocalizationOptions().SetDefaultCulture(AppConstants.DefaultLanguage)
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
    }
}