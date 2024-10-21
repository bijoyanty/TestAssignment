using Bogus;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the PersonalInformation page of the application and provides methods to interact with the elements in it.
/// Inherits from PageTest, allowing it to use Playwright's testing utilities.
/// </summary>
public class PersonalInformationPage : PageTest
{
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    //Selectors for various elements on the PersonalInformation page.

    private const string SalutationSelector = "button[value='dhr']";
    private const string NextButtonSelector = "button[type='submit']";
    private const string FirstNameInputSelector = "input[name='firstName']";
    private const string InitialInputSelector = "input[name='initials']";
    private const string LastNameInputSelector = "input[name='surname']";
    private const string DayInputSelector = "input[name='day']";
    private const string MonthInputSelector = "input[name='month']";
    private const string YearInputSelector = "input[name='year']";

    /// <summary>
    /// Initializes a new instance of the PersonalInformation class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    public PersonalInformationPage(IPage page)
    {
        _currentPage = page;
        _getLocator = new GetLocator( _currentPage );
    }

    /// <summary>
    /// Gets the locator for the  First Name input fields
    /// </summary>
    private ILocator FirstNameInputLocator => _getLocator.GetILocator( FirstNameInputSelector );

    /// <summary>
    /// Gets the locator for the  Last Name input fields
    /// </summary>
    private ILocator LastNameInputLocator => _getLocator.GetILocator( LastNameInputSelector );



    /// <summary>
    /// Gets the locator for the salutation radio button
    /// </summary>
    private ILocator SalutationLocator => _getLocator.GetILocator( SalutationSelector );

    /// <summary>
    /// Gets the locator for the initials input field
    /// </summary>
    private ILocator InitialInputLocator => _getLocator.GetILocator( InitialInputSelector );

    /// <summary>
    /// Gets the locator for the  day input fields 
    /// </summary>
    private ILocator DayInputLocator => _getLocator.GetILocator( DayInputSelector );

    /// <summary>
    /// Gets the locator for the  month input fields 
    /// </summary>
    private ILocator MonthInputLocator => _getLocator.GetILocator( MonthInputSelector );


    /// <summary>
    /// Gets the locator for the  Year input fields 
    /// </summary>
    private ILocator YearInputLocator => _getLocator.GetILocator( YearInputSelector );

    /// <summary>
    /// Gets the locator for the  submit button to navigate to next page
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator( NextButtonSelector );


    /// <summary>
    /// Clicks on next button to navigate to next page
    /// </summary>
    private async Task NavigateToNextPage()
    {
        await NextButtonLocator.ClickAsync();
    }

    /// <summary>
    /// Fills the initials
    /// </summary>
    private async Task FillInitials()
    {
        var prefix = new Faker().Name.Prefix();
        await InitialInputLocator.FillAsync( prefix );
    }

    /// <summary>
    /// Selects the salutation of the user
    /// </summary>
    private async Task SelectSalutation()
    {
        await SalutationLocator.ClickAsync();
    }

    /// <summary>
    /// Fills the first name of the user
    /// </summary>
    private async Task FillFirstName()
    {
        var firstName = new Faker().Name.FirstName();
        await FirstNameInputLocator.FillAsync( firstName );
    }

    /// <summary>
    /// Fills the last name of the user
    /// </summary>
    private async Task FillLastName()
    {
        var lastName = new Faker().Name.LastName();
        await LastNameInputLocator.FillAsync( lastName );
    }

    /// <summary>
    /// Fills the day from the DOB value of user
    /// </summary>
    private async Task FillDay()
    {
        var date = new Faker().Date.Random.Number( 1, 28 ).ToString();
        await DayInputLocator.FillAsync( date );
    }

    /// <summary>
    /// Fills the month from the DOB value of user
    /// </summary>
    private async Task FillMonth()
    {
        var month = new Faker().Date.Random.Number( 1, 12 ).ToString();
        await MonthInputLocator.FillAsync( month );
    }

    /// <summary>
    /// Fills the year from the DOB value of user
    /// </summary>
    private async Task FillYear()
    {
        var year = new Faker().Random.Number( 1960, 1998 ).ToString();
        await YearInputLocator.FillAsync( year );
    }


    /// <summary>
    /// Enter personal information of the user
    /// </summary>
    public async Task EnterPersonalDetailsAndNavigateToNextPage()
    {
        await WaitTillPageIsLoaded();
        await SelectSalutation();
        await FillFirstName();
        await FillLastName();
        await FillInitials();
        await FillDay();
        await FillMonth();
        await FillYear();
        await NavigateToNextPage();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        await _currentPage.WaitForURLAsync( $"{TestSetUp.Url}/duurzame-energie/bestellen2/je-gegevens/persoonlijke-gegevens/" );
    }
}