using System.Net;
using System.Text.Json;
using Microsoft.Playwright;

namespace TestAssignmentApi.Utilities;

/// <summary>
/// This class contains custom methods which gives deserialized response for api calls
/// </summary>
public class ApiMethods
{
    private const int RequestTimeoutInMs = 3000;

    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };

    public static async Task<T> Get<T>(IAPIRequestContext requestContext, string endPoint, APIRequestContextOptions? payLoad = null, int timeout = RequestTimeoutInMs)
    {
        var jsonData = GetJsonStringFromDataObject( payLoad );
        var response = await requestContext.GetAsync( endPoint, new() { DataString = jsonData, Timeout = timeout } );

        await LogResponseForNegativeResponses( response );
        return await GetDeserializedResponse<T>( response );
    }


    private static string? GetJsonStringFromDataObject(APIRequestContextOptions? data)
    {
        if (data?.DataObject is not null)
        {
            return JsonSerializer.Serialize( data.DataObject, JsonSerializerOptions );
        }

        else if (data?.DataString is not null)
        {
            return data.DataString;
        }

        else if (data?.Data is not null)
        {
            return JsonSerializer.Serialize( data.Data, JsonSerializerOptions );
        }
        else if (data?.DataByte is not null)
        {
            return JsonSerializer.Serialize( data.DataByte, JsonSerializerOptions );
        }

        return null;
    }

    private static async Task<T> GetDeserializedResponse<T>(IAPIResponse response)
    {
        var responseText = await response.TextAsync();
        try
        {
            return JsonSerializer.Deserialize<T>( responseText, JsonSerializerOptions )!;
        }

        catch
        {
            throw new InvalidOperationException( responseText );
        }
    }

    private static async Task LogResponseForNegativeResponses(IAPIResponse response)
    {
        if (response.Status is (int) HttpStatusCode.ServiceUnavailable or (int) HttpStatusCode.BadRequest)
        {
            Console.WriteLine( await response.TextAsync() );
        }
    }
}