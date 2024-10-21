using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the Offer page of the application and provides methods to interact with the elements in it.
/// Inherits from PageTest, allowing it to use Playwright's testing utilities.
/// </summary>
public class OfferPage : PageTest
{
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;
    private readonly Task<IResponse> _waitForResponseTask;

    //Selectors for various elements on the OfferPage page.

    private const string ToYourDataSelector = "button[data-label='Naar je gegevens']";


    /// <summary>
    /// Initializes a new instance of the OfferPage class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    public OfferPage(IPage page)
    {
        _currentPage = page;
        _waitForResponseTask = page.WaitForResponseAsync( new Regex( "/eneco/shoppingbasket/" ) );
        _getLocator = new GetLocator( _currentPage );
    }


    /// <summary>
    /// Gets the locator for the  ToYourData button to navigate to next page
    /// </summary>
    private ILocator ToYourDataLocator => _getLocator.GetILocator( ToYourDataSelector );


    /// <summary>
    /// Clicks on next button to navigate to next page
    /// </summary>
    private async Task NavigateToNextPage()
    {
        await ToYourDataLocator.ClickAsync();
    }

 

    /// <summary>
    /// select enter my consumption myself and navigates to next page
    /// </summary>
    public async Task ValidateOfferAndNavigateToNextPage()
    {
        await WaitTillPageIsLoaded();
        await NavigateToNextPage();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await _currentPage.WaitForURLAsync( $"{TestSetUp.Url}/duurzame-energie/bestellen2/overzicht/");
    }
}