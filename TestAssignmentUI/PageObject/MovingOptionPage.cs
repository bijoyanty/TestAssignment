using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the MovingOption page of the application and provides methods to interact with the elements in it.
/// Inherits from PageTest, allowing it to use Playwright's testing utilities.
/// </summary>
public class MovingOptionPage : PageTest
{
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    //Selectors for various elements on the MovingOption page.
    private const string NotMovingOptionSelector = "button[value='false']";
    private const string NextButtonSelector = "button[type='submit']";


    /// <summary>
    /// Initializes a new instance of the MovingOptionPage class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    public MovingOptionPage(IPage page)
    {
        _currentPage = page;
        _getLocator = new GetLocator( _currentPage );
    }

    /// <summary>
    /// Gets the locator for the Not Moving Option Radio button
    /// </summary>
    private ILocator NotMovingOptionSelectorRadioLocator => _getLocator.GetILocator( NotMovingOptionSelector );

    /// <summary>
    /// Gets the locator for the  submit button to navigate to next page
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator( NextButtonSelector );

    /// <summary>
    /// selects teh not moving radio button
    /// </summary>
    private async Task SelectNotMovingOption()
    {
        await NotMovingOptionSelectorRadioLocator.ClickAsync();
    }

    /// <summary>
    /// Clicks on next button to navigate to next page
    /// </summary>
    private async Task NavigateToNextPage()

    {
        await NextButtonLocator.ClickAsync();
    }

    /// <summary>
    /// select not moving option and navigate to next page
    /// </summary>
    public async Task SelectNotMovingOptionAndNavigateToNextPage()
    {
        await WaitTillPageIsLoaded();
        await SelectNotMovingOption();
        await NavigateToNextPage();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForLoadStateAsync( LoadState.DOMContentLoaded );
        await _currentPage.WaitForURLAsync( $"{TestSetUp.Url}/duurzame-energie/bestellen2/je-gegevens/verhuizen/" );
    }
}