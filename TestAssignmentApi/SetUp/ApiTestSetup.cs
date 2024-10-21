using Microsoft.Playwright;
using Polly;
using TestAssignmentApi.Utilities;

namespace TestAssignmentApi.SetUp;

/// <summary>
/// ApiTestSetup is responsible for retrieving the configurations, creating request context 
/// </summary>
public class ApiTestSetup
{
    protected static ResiliencePipeline ResiliencePipeline = null!;
    private readonly RequestContext _requestContext = new();
    public static IAPIRequestContext RequestContext { get; private set; } = null!;

    /// <summary>
    /// This method is executed once for every Test Fixture
    /// It sets up the necessary configuration
    /// </summary>
    [OneTimeSetUp] 
    public async Task TestSetUp()
    {
        var configuration = new ApiTestConfiguration();
        var baseUrl = configuration.Configuration.BaseUrl;
        var accessToken = configuration.Configuration.AccessToken;
        RequestContext = await _requestContext.CreateContext(baseUrl, accessToken );
        ResiliencePipeline=RetryMechanism.InitiateResiliencePipeline();
    }

    [OneTimeTearDown] public async Task TearDown()
    {
        await RequestContext.DisposeAsync();
    }
}