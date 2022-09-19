using Microsoft.Extensions.Options;
using WebApi.Common.Extensions.EfServices;

namespace WebApi.Common.Extensions.GridifyServices;

public class GridifyOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private readonly IConfiguration _configuration;
    internal const string ConfigurationSectionName = "GridifyOptions";

    public GridifyOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    
    public void Configure(DatabaseOptions options)
    {
        _configuration
            .GetSection(ConfigurationSectionName)
            .Bind(options);
    }
}