using Microsoft.Playwright;

namespace TestAssignmentUI.Utilities;

/// <summary>
/// BrowserContext class is responsible  for creating, closing nad disposing instance of IBrowserContext
/// </summary>
public class BrowserContext

{
    /// <summary>
    /// Returns a browser context instance 
    /// </summary>
    public async Task<IBrowserContext> GetBrowserContext(IBrowser browser)
    {
        return await browser.NewContextAsync();
    }

    /// <summary>
    /// Closes browser Context instance
    /// </summary>
    public async Task CloseBrowserContext(IBrowserContext browserContext)
    {
        await browserContext.CloseAsync();
    }
}