using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the ContractStartDate page of the application and provides methods to interact with its elements.
/// Inherits from <see cref="PageTest"/>, allowing usage of Playwright's testing utilities.
/// </summary>
public class ContractStartDatePage : PageTest
{
    // Private fields for current page and locator helper.
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    // CSS selectors for various elements on the ContractStartDate page.
    private const string CalenderSelector = "button[data-label='Open kalender']"; 
    private const string SelectNextMonthSelector = "button[data-label='Volgende maand']";  
    private const string NextButtonSelector = "button[type='submit']"; 
    private const string NotificationBoxSelector = "div[class*='notification-box']";  

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractStartDatePage"/> class with the specified <see cref="IPage"/> instance.
    /// </summary>
    /// <param name="page">The <see cref="IPage"/> instance representing the current browser page.</param>
    public ContractStartDatePage(IPage page)
    {
        _currentPage = page;  
        _getLocator = new GetLocator(_currentPage);
    }

    /// <summary>
    /// Locator for the submit button to navigate to the next page.
    /// </summary>
    private ILocator NextButtonLocator => _getLocator.GetILocator(NextButtonSelector);

    /// <summary>
    /// Locator for the notification box on the page.
    /// </summary>
    private ILocator NotificationBoxLocator => _getLocator.GetILocator(NotificationBoxSelector);

    /// <summary>
    /// Locator for the calendar button to open the date picker.
    /// </summary>
    private ILocator CalenderLocator => _getLocator.GetILocator(CalenderSelector);

    /// <summary>
    /// Locator for the next month button on the calendar.
    /// </summary>
    private ILocator SelectNextMonthLocator => _getLocator.GetILocator(SelectNextMonthSelector);

    /// <summary>
    /// Clicks on the next button to navigate to the next page.
    /// </summary>
    private async Task NavigateToNextPage()
    {
        await NextButtonLocator.ClickAsync();  
    }

    /// <summary>
    /// Opens the calendar to select a start date.
    /// </summary>
    private async Task OpenCalender()
    {
        await CalenderLocator.ClickAsync();  
    }

    /// <summary>
    /// Clicks on the "Next Month" button in the calendar.
    /// </summary>
    private async Task ClickOnNextMonth()
    {
        await SelectNextMonthLocator.ClickAsync(); 
    }

    /// <summary>
    /// Selects a future date (28 days from now) in the calendar.
    /// </summary>
    private async Task ClickOnDate()
    {
        var date = DateTime.Now.AddDays(28).ToString("dd");  
        var dateElement = _currentPage.Locator($"button[aria-label*='{date}']"); 
        await dateElement.ClickAsync();  
    }

    /// <summary>
    /// Fills the start date by selecting a date in the future from the calendar.
    /// </summary>
    private async Task FillDeliveryStartDateInFuture()
    {
        await OpenCalender();  
        await ClickOnNextMonth();  
        await ClickOnDate(); 
    }

    /// <summary>
    /// Validates that the notification box is displayed on the page.
    /// </summary>
    private async Task ValidateNotificationBoxIsDisplayed()
    {
        await Expect(NotificationBoxLocator).ToBeVisibleAsync();  
    }

    /// <summary>
    /// Selects a delivery start date and navigates to the next page.
    /// </summary>
    public async Task FillDeliveryStartDateAndNavigateToNextPage()
    {
        await WaitTillPageIsLoaded();  
        await FillDeliveryStartDateInFuture();  
        await ValidateNotificationBoxIsDisplayed();  
        await NavigateToNextPage();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the URL.
    /// Ensures that all page content is fully available before interacting.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForURLAsync($"{TestSetUp.Url}/duurzame-energie/bestellen2/je-gegevens/start-leverdatum/");  // Wait for the page to fully load.
    }
}
