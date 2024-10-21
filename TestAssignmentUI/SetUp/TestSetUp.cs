using NUnit.Framework;
using TestAssignmentUI.Model;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.SetUp;

/// <summary>
/// TestSetUp is responsible for retrieving the configurations, creating browser instance
/// It inherits from GetPage class
/// </summary>
public class TestSetUp : GetPage
{
    public PlaywrightConfig PlaywrightConfig;
    public static string? Url;

    /// <summary>
    /// This method is executed once before every Test Fixture
    /// It sets up the necessary configuration, credentials, and browser session.
    /// </summary>
    [OneTimeSetUp] 
    public async Task Setup()
    {
        // Create an instance of UiTestConfiguration to load UI configuration settings.
        var configuration = new UiTestConfiguration();

        Url = configuration.GetUrlForTesting();
        PlaywrightConfig = configuration.LoadConfiguration();

        // Create a browser instance based on the Playwright configuration.
        await CreateBrowser( PlaywrightConfig );
    }
}