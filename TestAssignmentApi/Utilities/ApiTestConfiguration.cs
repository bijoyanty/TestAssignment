using TestAssignmentApi.Models;
using TestAssignmentShared.Utilities;

namespace TestAssignmentApi.Utilities;

/// <summary>
/// The ApiTestConfiguration class is responsible for reading and providing configuration settings
/// from the appsettings file.
/// </summary>
public class ApiTestConfiguration
{
    public ApiConfiguration Configuration { get; }

    /// <summary>
    /// Static constructor to initialize the Configuration object by reading the appropriate appsettings file.
    /// </summary>
    public ApiTestConfiguration()
    {
        var configuration = GetConfiguration.ReadAppSettingFile();
        Configuration = new ApiConfiguration
        {
            BaseUrl = LoadOptions.LoadRequiredOptions<string>(configuration, "BaseUrl"),
            AccessToken = LoadOptions.LoadRequiredOptions<string>(configuration, "AccessToken"),
        };
    }
}