using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the home page of the application and provides methods to interact with the elements in it.
/// Inherits from PageTest, allowing it to use Playwright's testing utilities.
/// </summary>
public class HomePage : PageTest
{
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    //Selectors for various elements on the home page.
    private const string PostCodeInputSelector = "input[name='postalCode']";
    private const string HouseNumberInputSelector = "input[name='houseNumber']";
    private const string CalculateMonthlyAmountButtonSelector = "button[type='submit']";
    private const string AcceptCookiesSelector = "button[data-label='Accepteren']";


    /// <summary>
    /// Initializes a new instance of the HomePage class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    public HomePage(IPage page)
    {
        _currentPage = page;
        _getLocator = new GetLocator( _currentPage );
    }

    /// <summary>
    /// Gets the locator for the  input field to enter PostCode
    /// </summary>
    private ILocator PostCodeInputLocator => _getLocator.GetILocator( PostCodeInputSelector );

    /// <summary>
    /// Gets the locator for the  input field to enter House number
    /// </summary>
    private ILocator HouseNumberInputLocator => _getLocator.GetILocator( HouseNumberInputSelector );

    /// <summary>
    /// Gets the locator for the  submit button to submit the address values
    /// </summary>
    private ILocator CalculateMonthlyAmountButtonLocator => _getLocator.GetILocator( CalculateMonthlyAmountButtonSelector );


    /// <summary>
    /// Gets the locator for the  submit button to submit the address values
    /// </summary>
    private ILocator AcceptCookiesLocator => _getLocator.GetILocator(AcceptCookiesSelector);

    /// <summary>
    /// Enters the postal code
    /// </summary>
    private async Task EnterPostCode(string postCode)
    {
        await PostCodeInputLocator.FillAsync( postCode );
    }

    /// <summary>
    /// Enters house number
    /// </summary>
    private async Task EnterHouseNumber(string houseNumber)
    {
        await HouseNumberInputLocator.FillAsync( houseNumber );
    }

    /// <summary>
    /// Submits the address for starting monthly calculation
    /// </summary>
    private async Task ClickOnCalculateMonthlyAmountButtonLocator()

    {
        await CalculateMonthlyAmountButtonLocator.ClickAsync();
    }

    /// <summary>
    /// Validate street Name is displayed
    /// </summary>
    private async Task ValidateStreetNameIsDisplayed(string streetName)

    {
        await Expect( _currentPage.GetByText( streetName ) ).ToBeVisibleAsync();
    }

    /// <summary>
    /// Enter the address and submits the details to start monthly calculation
    /// </summary>
    public async Task EnterAddressAndStartCalculationOfMonthlyExpense(string[] address)
    {
        await WaitTillPageIsLoaded();
        await EnterPostCode( address.First() );
        await EnterHouseNumber( address.ElementAt( 1 ) );
        await ValidateStreetNameIsDisplayed( address.Last() );
        await ClickOnCalculateMonthlyAmountButtonLocator();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForLoadStateAsync( LoadState.DOMContentLoaded );
        await AcceptCookiesLocator.ClickAsync();
    }
}