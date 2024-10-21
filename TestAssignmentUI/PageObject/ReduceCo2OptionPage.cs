using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the ReduceCo2Option page of the application and provides methods to interact with the elements in it.
/// Inherits from PageTest, allowing it to use Playwright's testing utilities.
/// </summary>
public class ReduceCo2OptionPage : PageTest
{
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    //Selectors for various elements on the ReduceCo2Option page.
    private const string SelectCo2CompensatedGasSelector = "#hasUpsellGas";
    private const string NextButtonSelector = "button[type='submit']";


    /// <summary>
    /// Initializes a new instance of the ReduceCo2OptionPage class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    public ReduceCo2OptionPage(IPage page)
    {
        _currentPage = page;
        _getLocator = new GetLocator( _currentPage );
    }

    /// <summary>
    /// Gets the locator for the Co2 Compensated GasSwitch
    /// </summary>
    private ILocator SelectCo2CompensatedGasSwitchLocator => _getLocator.GetILocator( SelectCo2CompensatedGasSelector );

    /// <summary>
    /// Gets the locator for the  submit button to navigate to next page
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator( NextButtonSelector );

    /// <summary>
    ///  This method validates that Reduce Co2 Option is displayed 
    /// </summary>
    private async Task ValidateReduceCo2OptionIsDisplayed()
    {
        await Expect( SelectCo2CompensatedGasSwitchLocator ).ToBeVisibleAsync();
    }

    /// <summary>
    /// Clicks on next button to navigate to next page
    /// </summary>
    private async Task NavigateToNextPage()
    {
        await NextButtonLocator.ClickAsync();
    }

    /// <summary>
    /// This method validates that Reduce Co2 Option is displayed and navigates to next screen 
    /// </summary>
    public async Task ValidateReduceCo2OptionIsDisplayedAndNavigateToNextPage()
    {
        await WaitTillPageIsLoaded();
        await ValidateReduceCo2OptionIsDisplayed();
        await NavigateToNextPage();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForLoadStateAsync( LoadState.DOMContentLoaded );
        await _currentPage.WaitForURLAsync( $"{TestSetUp.Url}/duurzame-energie/bestellen2/productkeuze/co2-gas/" );
    }
}