using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the EnergyConsumptionPage page of the application and provides methods to interact with the elements on it.
/// Inherits from <see cref="PageTest"/>, allowing usage of Playwright's testing utilities.
/// </summary>
public class EnergyConsumptionPage : PageTest
{
    // Private fields for storing the current page and locator helper.
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    // CSS selectors for various elements on the EnergyConsumption page.
    private const string EnterConsumptionMyselfSelector = "button[value='exactUsage']";  
    private const string NextButtonSelector = "button[type='submit']"; 
    /// <summary>
    /// Initializes a new instance of the <see cref="EnergyConsumptionPage"/> class with the specified <see cref="IPage"/> instance.
    /// </summary>
    /// <param name="page">The <see cref="IPage"/> instance representing the current browser page.</param>
    public EnergyConsumptionPage(IPage page)
    {
        _currentPage = page;  
        _getLocator = new GetLocator(_currentPage);  
    }

    /// <summary>
    /// Locator for the radio button to select "Enter my consumption manually."
    /// </summary>
    private ILocator EnterConsumptionMyselfSelectorRadioLocator => _getLocator.GetILocator(EnterConsumptionMyselfSelector);

    /// <summary>
    /// Locator for the submit button to navigate to the next page.
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator(NextButtonSelector);

    /// <summary>
    /// Selects the "Enter my consumption manually" option by clicking the corresponding radio button.
    /// </summary>
    private async Task SelectEnterConsumptionManually()
    {
        await EnterConsumptionMyselfSelectorRadioLocator.ClickAsync(); 
    }

    /// <summary>
    /// Clicks on the next button to navigate to the next page.
    /// </summary>
    private async Task NavigateToNextPage()
    {
        await NextButtonLocator.ClickAsync();
    }

    /// <summary>
    /// Selects the "Enter my consumption manually" option and navigates to the next page.
    /// </summary>
    public async Task SelectEnterConsumptionManuallyAndSubmit()
    {
        await WaitTillPageIsLoaded();  
        await SelectEnterConsumptionManually();  
        await NavigateToNextPage();  
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interacting.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForLoadStateAsync( LoadState.DOMContentLoaded );
        await Expect( NextButtonLocator ).ToBeVisibleAsync();
    }
}