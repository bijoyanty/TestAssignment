using Bogus;
using FluentAssertions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the EnterConsumption page of the application.
/// Provides methods to interact with the fields for entering consumption data.
/// Inherits from <see cref="PageTest"/> to utilize Playwright's testing utilities.
/// </summary>
public class EnterConsumptionPage : PageTest
{
    // Private fields for the current page and locator helper.
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    // XPaths and CSS selectors for various elements on the EnterConsumption page.
    private const string DefaultSmartMeterOptionSelector = "//button[@data-state='checked']//ancestor::label"; 
    private const string NormalAnnualConsumptionInputSelector = "input[name='usageElectricityHigh']";
    private const string OffPeakAnnualConsumptionInputSelector = "input[name='usageElectricityLow']";
    private const string GasConsumptionInputSelector = "input[name='usageGas']"; 
    private const string NextButtonSelector = "button[type='submit']";

    /// <summary>
    /// Initializes a new instance of the <see cref="EnterConsumptionPage"/> class with the provided <paramref name="page"/>.
    /// </summary>
    /// <param name="page">The <see cref="IPage"/> instance representing the current browser page.</param>
    public EnterConsumptionPage(IPage page)
    {
        _currentPage = page;
        _getLocator = new GetLocator(_currentPage);
    }

    /// <summary>
    /// Locator for the checked Smart meter radio button option.
    /// </summary>
    private ILocator DefaultSmartMeterOptionRadioLocator => _getLocator.GetILocator(DefaultSmartMeterOptionSelector);

    /// <summary>
    /// Locator for the input field to enter normal annual electricity consumption.
    /// </summary>
    private ILocator NormalAnnualConsumptionInputLocator => _getLocator.GetILocator(NormalAnnualConsumptionInputSelector);

    /// <summary>
    /// Locator for the input field to enter off-peak annual electricity consumption.
    /// </summary>
    private ILocator OffPeakAnnualConsumptionInputLocator => _getLocator.GetILocator(OffPeakAnnualConsumptionInputSelector);

    /// <summary>
    /// Locator for the input field to enter annual gas consumption.
    /// </summary>
    private ILocator GasConsumptionInputLocator => _getLocator.GetILocator(GasConsumptionInputSelector);

    /// <summary>
    /// Locator for the "Next" button that navigates to the next page.
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator(NextButtonSelector);

    /// <summary>
    /// Enters a random normal annual electricity consumption using <see cref="Bogus"/> to generate random numbers.
    /// </summary>
    private async Task EnterNormalAnnualConsumptionOfElectricity()
    {
        var normalAnnualConsumption = new Faker().Random.Number(1000, 2000).ToString(); // Generates a random value between 1000 and 2000.
        await NormalAnnualConsumptionInputLocator.FillAsync(normalAnnualConsumption);
    }

    /// <summary>
    /// Validates that the default selection for the smart meter option is "Yes, I have a smart meter".
    /// </summary>
    private async Task ValidateIHaveSmartMeterOptionIsSelectedByDefault()
    {
        var selectedOption = await DefaultSmartMeterOptionRadioLocator.TextContentAsync();
        selectedOption.Should().ContainEquivalentOf("Ja,");
    }

    /// <summary>
    /// Enters a random off-peak annual electricity consumption using <see cref="Bogus"/>.
    /// </summary>
    private async Task EnterOffPeakAnnualConsumptionOfElectricity()
    {
        var offPeakAnnualConsumption = new Faker().Random.Number(1000, 2000).ToString(); // Generates a random value for off-peak consumption.
        await OffPeakAnnualConsumptionInputLocator.FillAsync(offPeakAnnualConsumption); 
    }

    /// <summary>
    /// Enters a random annual gas consumption using <see cref="Bogus"/>.
    /// </summary>
    private async Task EnterAnnualConsumptionOfGas()
    {
        var annualGasConsumption = new Faker().Random.Number(1000, 2000).ToString(); // Generates a random value for gas consumption.
        await GasConsumptionInputLocator.FillAsync(annualGasConsumption);
    }

    /// <summary>
    /// Clicks the "Next" button to navigate to the next page.
    /// </summary>
    private async Task NavigateToNextPage()
    {
        await NextButtonLocator.ClickAsync();
    }

    /// <summary>
    /// Enters consumption data for electricity and gas and navigates to the next page.
    /// </summary>
    public async Task EnterConsumptionOfElectricityAndGas()
    {
        await WaitTillPageIsLoaded(); 
        await ValidateIHaveSmartMeterOptionIsSelectedByDefault(); 
        await EnterNormalAnnualConsumptionOfElectricity(); 
        await EnterOffPeakAnnualConsumptionOfElectricity(); 
        await EnterAnnualConsumptionOfGas(); 
        await NavigateToNextPage();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForURLAsync($"{TestSetUp.Url}/duurzame-energie/bestellen2/verbruik/"); 
    }
}
