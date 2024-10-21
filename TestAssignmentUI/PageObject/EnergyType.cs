using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the EnergyType selection page of the application.
/// Provides methods to interact with the elements on the page for selecting the energy type.
/// Inherits from <see cref="PageTest"/> to use Playwright's testing utilities.
/// </summary>
public class EnergyTypePage : PageTest
{
    // Private fields for the current page and locator helper.
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    // CSS selectors for elements on the energy type selection page.
    private const string ElectricityAndGasRadioSelector = "button[value='electricityAndGas']";
    private const string NextButtonSelector = "button[type='submit']";

    /// <summary>
    /// Initializes a new instance of the <see cref="EnergyTypePage"/> class with the provided <paramref name="page"/>.
    /// </summary>
    /// <param name="page">The <see cref="IPage"/> instance representing the current browser page.</param>
    public EnergyTypePage(IPage page)
    {
        _currentPage = page;
        _getLocator = new GetLocator( _currentPage );
    }

    /// <summary>
    /// Locator for the "Electricity and Gas" radio button.
    /// </summary>
    private ILocator ElectricityAndGasRadioLocator => _getLocator.GetILocator( ElectricityAndGasRadioSelector );

    /// <summary>
    /// Locator for the "Next" button that navigates to the next page.
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator( NextButtonSelector );

    /// <summary>
    /// Selects the "Electricity and Gas" option.
    /// </summary>
    private async Task SelectElectricityAndGasEnergyType()
    {
        await ElectricityAndGasRadioLocator.ClickAsync();
    }

    /// <summary>
    /// Clicks the "Next" button to navigate to the next page.
    /// </summary>
    private async Task NavigateToNextPage()
    {
        await NextButtonLocator.ClickAsync();
    }

    /// <summary>
    /// Selects the energy type and then navigates to the next page.
    /// </summary>
    public async Task SelectEnergyTypeAndSubmit()
    {
        await WaitTillPageIsLoaded();
        await SelectElectricityAndGasEnergyType();
        await NavigateToNextPage();
    }

    /// <summary>
    /// Waits for the page to fully load by monitoring the URL to ensure the content is available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForURLAsync( $"{TestSetUp.Url}/duurzame-energie/bestellen2/energiekeuze/" );
    }
}