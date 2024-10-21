using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the ChoseContractType page of the application. 
/// Provides methods to interact with elements like contract options and buttons, enabling the automation of contract selection.
/// Inherits from <see cref="PageTest"/>, making Playwright testing tools available.
/// </summary>
public class ChoseContractTypePage : PageTest
{
    // Private fields for the current browser page and the locator utility.
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    // CSS selectors for elements on the ChoseContractTypePage page.
    private const string FixedContractOptionSelector = "button[value='fixed']";  
    private const string OneYearFixedContractOptionSelector = "button[data-label='1 jaar stroom en gas']"; 
    private const string NextButtonSelector = "button[type='submit']";  

    /// <summary>
    /// Initializes a new instance of the <see cref="ChoseContractTypePage"/> class with the specified <see cref="IPage"/> instance.
    /// </summary>
    /// <param name="page">The <see cref="IPage"/> instance representing the current browser page.</param>
    public ChoseContractTypePage(IPage page)
    {
        _currentPage = page;  
        _getLocator = new GetLocator(_currentPage);
    }

    /// <summary>
    /// Locator for the fixed contract option radio button.
    /// </summary>
    private ILocator FixedContractOptionSelectorRadioLocator => _getLocator.GetILocator(FixedContractOptionSelector);

    /// <summary>
    /// Locator for the one-year fixed contract option radio button.
    /// </summary>
    private ILocator OneYearFixedContractOptionRadioLocator => _getLocator.GetILocator(OneYearFixedContractOptionSelector);

    /// <summary>
    /// Locator for the "Next" button to navigate to the next page.
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator(NextButtonSelector);

    /// <summary>
    /// Selects the "Fixed Contract" option by clicking the corresponding button.
    /// </summary>
    private async Task SelectFixedContractOption()
    {
        await FixedContractOptionSelectorRadioLocator.ClickAsync();  
    }

    /// <summary>
    /// Selects the "One-Year Fixed Contract" option by clicking the corresponding button.
    /// Also ensures that the option is visible before interacting with it.
    /// </summary>
    private async Task SelectOneYearFixedContractOption()
    {
        await Expect(OneYearFixedContractOptionRadioLocator).ToBeVisibleAsync();
        await OneYearFixedContractOptionRadioLocator.ClickAsync();  
    }

    /// <summary>
    /// Clicks the "Next" button to navigate to the next page.
    /// </summary>
    private async Task NavigateToNextPage()
    {
        await NextButtonLocator.ClickAsync();
    }

    /// <summary>
    /// Selects a contract type (fixed and one-year) and navigates to the next page by clicking the "Next" button.
    /// </summary>
    public async Task ChoseContractTypeAndNavigateToNextPage()
    {
        await WaitTillPageIsLoaded();  
        await SelectFixedContractOption();  
        await SelectOneYearFixedContractOption(); 
        await NavigateToNextPage(); 
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the URL.
    /// This ensures that the content is fully available before interaction.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForURLAsync($"{TestSetUp.Url}/duurzame-energie/bestellen2/je-gegevens/verhuizen/"); 
    }
}
