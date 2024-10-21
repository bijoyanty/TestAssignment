using Microsoft.Extensions.Configuration;

namespace TestAssignmentShared.Utilities;

/// <summary>
/// The LoadOptions class provides methods for retrieving the configuration from specified IConfiguration based on the class provided
/// </summary>
public class LoadOptions
{
    /// <summary>
    /// Returns a section from the IConfiguration based on the specified class
    /// </summary>
    public static T LoadRequiredOptions<T>(IConfiguration configuration, string? name = null)
    {
        name ??= typeof(T).Name;

        return (configuration ?? throw new InvalidOperationException())
               .GetRequiredSection( name )
               .Get<T>() ??
               throw new InvalidOperationException( $"Section '{name}' expected in config holding type {typeof(T)}" );
    }
}