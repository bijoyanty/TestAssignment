using Microsoft.Playwright;
using NUnit.Framework;

namespace TestAssignmentUI.Utilities;

/// <summary>
/// CaptureScreenShot method is responsible for capturing screenshot for failed scenarios
/// </summary>
public class CaptureScreenShot
{
    private readonly IPage _currentPage;
    private static string _folderPath = null!;
    private readonly DirectoryInfo _screenShotDirectoryInfo;

    /// <summary>
    /// Initializes a new instance of the CaptureScreenShot class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    /// <param name="path">Path of the current directory</param>
    public CaptureScreenShot(IPage page, string path)
    {
        _currentPage = page;

        _folderPath = GetLocationOfScreenShotFolder( path ) ?? throw new InvalidOperationException();
        _screenShotDirectoryInfo = new DirectoryInfo( _folderPath );
    }

    /// <summary>
    /// Captures the screenshot and attaches the same to the test result
    /// </summary>
    public async Task Capture()
    {
        if (!_screenShotDirectoryInfo.Exists && _currentPage.IsClosed is not true)
        {
            _screenShotDirectoryInfo.Create();
        }

        var filePath = $"{_folderPath}\\{GetFileName()}.png";

        await _currentPage.ScreenshotAsync( new() { Path = filePath, FullPage = true } );
        TestContext.AddTestAttachment( filePath );
    }

    /// <summary>
    /// Returns the file name of the screenshot file
    /// </summary>
    private static string GetFileName()
    {
        var filename = TestContext.CurrentContext.Test.MethodName;
        filename = filename?.Replace( "(\"", string.Empty );
        filename = filename?.Replace( "_", string.Empty );
        filename = filename?.Replace( ")", string.Empty );
        filename = filename?.Replace( ",", string.Empty );
        filename = filename?.Replace( @"""", "_" );

        return filename ?? throw new InvalidOperationException( "File name for screenshot is incorrect" );
    }

    /// <summary>
    /// Returns the location of the screenshot Folder when tests are being debugged locally
    /// </summary>
    public static string GetLocationOfScreenShotFolder(string path)
    {
        var solutionDirectory = PathFinder.UpToSolutionPath( path );
        var screenShotFolder = "\\ScreenShot";
        var finalPath = Path.GetFullPath( solutionDirectory + screenShotFolder );

        if (!Directory.Exists( finalPath ))
        {
            Directory.CreateDirectory( finalPath );
        }

        return finalPath;
    }
}