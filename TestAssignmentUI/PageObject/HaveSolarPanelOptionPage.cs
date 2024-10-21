using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the HaveSolarPanelOption page of the application and provides methods to interact with the elements in it.
/// Inherits from PageTest, allowing it to use Playwright's testing utilities.
/// </summary>
public class HaveSolarPanelOptionPage : PageTest
{
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    //Selectors for various elements on the EnergyConsumption page.
    private const string NoSolarPanelsOptionSelector = "button[value='noPanels']";
    private const string NextButtonSelector = "button[type='submit']";


    /// <summary>
    /// Initializes a new instance of the HaveSolarPanelOptionPage class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    public HaveSolarPanelOptionPage(IPage page)
    {
        _currentPage = page;
        _getLocator = new GetLocator(_currentPage);
    }

    /// <summary>
    /// Gets the locator for the checked Smart meter radio button option
    /// </summary>
    private ILocator NoSolarPanelsOptionSelectorRadioLocator => _getLocator.GetILocator(NoSolarPanelsOptionSelector);

    /// <summary>
    /// Gets the locator for the  submit button to navigate to next page
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator(NextButtonSelector);

    /// <summary>
    /// Validates that by default "I have a smart meter" option is selected
    /// </summary>
    private async Task SelectNoSolarPanelOption()
    {
        await NoSolarPanelsOptionSelectorRadioLocator.ClickAsync();
    }

    /// <summary>
    /// Clicks on next button to navigate to next page
    /// </summary>
    private async Task NavigateToNextPage()

    {
        await NextButtonLocator.ClickAsync();
    }

    /// <summary>
    /// select enter my consumption myself and navigates to next page
    /// </summary>
    public async Task SelectNoSolarPanelOptionAndNavigateToNextPage()
    {
        await WaitTillPageIsLoaded();
        await SelectNoSolarPanelOption();
        await NavigateToNextPage();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForLoadStateAsync( LoadState.DOMContentLoaded );
        await _currentPage.WaitForURLAsync($"{TestSetUp.Url}/duurzame-energie/bestellen2/zonnepanelen/");
    }
}