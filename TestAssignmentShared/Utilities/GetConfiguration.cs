using Microsoft.Extensions.Configuration;

namespace TestAssignmentShared.Utilities;

/// <summary>
/// The GetConfiguration class provides methods to read configuration settings from appsettings files.
/// </summary>
public class GetConfiguration
{
    /// <summary>
    /// Reads the appsettings files returns the configuration as an IConfigurationRoot object.
    /// </summary>
    /// <exception cref="Exception">
    /// Thrown if the main appsettings.json file is not found
    /// </exception>
    public static IConfiguration ReadAppSettingFile()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath( Directory.GetCurrentDirectory() )
            .AddJsonFile( "appsettings.json", optional: false, reloadOnChange: true );
        try
        {
            var configuration = builder.Build();
            return configuration;
        }
        catch (FileNotFoundException ex)
        {
            throw new Exception( "No appsettings.json found! Make sure a file called 'appsettings.json' is present in the " +
                                 "same folder as your project's csproj file, NOT the solution file ", ex );
        }
    }
}