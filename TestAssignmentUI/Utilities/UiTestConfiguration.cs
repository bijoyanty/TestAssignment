using Microsoft.Extensions.Configuration;
using TestAssignmentShared.Utilities;
using TestAssignmentUI.Model;

namespace TestAssignmentUI.Utilities;

/// <summary>
/// The UiTestConfiguration class is responsible for reading and providing configuration settings
/// from the appsettings file.
/// </summary>
public class UiTestConfiguration
{
    private readonly UiConfiguration _uiConfiguration;

    /// <summary>
    /// Static constructor to initialize the Configuration object by reading the appropriate appsettings file.
    /// </summary>
    public UiTestConfiguration()
    {
        IConfiguration configuration = GetConfiguration.ReadAppSettingFile();
        _uiConfiguration = new UiConfiguration()
        {
            PlaywrightConfig = LoadOptions.LoadRequiredOptions<PlaywrightConfig>( configuration ),
            TestUrls = LoadOptions.LoadRequiredOptions<TestUrls>( configuration, "TestUrls" )
        };
    }


    /// <summary>
    /// Extracts the configuration from the IConfiguration object
    /// </summary>
    public string GetUrlForTesting()
    {
        return _uiConfiguration.TestUrls.TestSiteUrl;
    }

    public PlaywrightConfig LoadConfiguration()
    {
        return _uiConfiguration.PlaywrightConfig;
    }
}