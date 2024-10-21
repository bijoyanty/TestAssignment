using Microsoft.Playwright;

namespace TestAssignmentApi.Utilities;

/// <summary>
/// The RequestContext class is responsible for creating api context required for api calls
/// </summary>
public class RequestContext
{
    /// <summary>
    /// This method gets the Header values as a dictionary which also includes the access token
    /// </summary>
    private Dictionary<string, string> GetHeader(string accessToken)
    {
        var headers = new Dictionary<string, string> { { "Content-Type", System.Net.Mime.MediaTypeNames.Application.Json }, { "Authorization", accessToken } };

        return headers;
    }

    /// <summary>
    /// This method creates the context using the base url and has a header with access token
    /// </summary>
    public async Task<IAPIRequestContext> CreateContext(string baseUrl, string accessToken)
    {
        var playwright = await Playwright.CreateAsync();
        var headers = GetHeader( accessToken );

        var request = await playwright.APIRequest.NewContextAsync( new()
        {
            // All requests we send go to this API endpoint.
            BaseURL = baseUrl,
            ExtraHTTPHeaders = headers
        } );

        return request;
    }
}