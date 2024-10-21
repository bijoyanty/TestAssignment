using Bogus;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject;

/// <summary>
/// Represents the ContactDetails page of the application and provides methods to interact with the elements on it.
/// Inherits from <see cref="PageTest"/>, enabling Playwright's testing utilities.
/// </summary>
public class ContactDetailsPage : PageTest
{
    // Private fields for the current browser page and locator utility.
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    // CSS selectors for elements on the ContactDetails page.
    private const string PhoneNumberSelector = "input[name='phoneNumber']";  
    private const string EmailAddressSelector = "input[name='emailAddress']";  
    private const string CheckOrderSelector = "button[type='submit']";  

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactDetailsPage"/> class with the specified <see cref="IPage"/> instance.
    /// </summary>
    /// <param name="page">The <see cref="IPage"/> instance representing the current browser page.</param>
    public ContactDetailsPage(IPage page)
    {
        _currentPage = page;  
        _getLocator = new GetLocator(_currentPage);  
    }

    /// <summary>
    /// Locator for the phone number input field.
    /// </summary>
    private ILocator PhoneNumberInputLocator => _getLocator.GetILocator(PhoneNumberSelector);

    /// <summary>
    /// Locator for the email address input field.
    /// </summary>
    private ILocator EmailAddressLocator => _getLocator.GetILocator(EmailAddressSelector);

    /// <summary>
    /// Locator for the "Check Order" button, which is used to proceed to the next page.
    /// </summary>
    private ILocator CheckOrderLocator => _getLocator.GetILocator(CheckOrderSelector);

    /// <summary>
    /// Validates that the "Check Order" button is displayed on the page.
    /// Ensures that the button is visible before any interaction.
    /// </summary>
    private async Task ValidateCheckOrderButtonIsDisplayed()
    {
        await Expect(CheckOrderLocator).ToBeVisibleAsync(); 
    }

    /// <summary>
    /// Fills the phone number input field with a generated fake phone number.
    /// Uses the Bogus library to generate the phone number.
    /// </summary>
    private async Task FillPhoneNumber()
    {
        var phoneNumber = new Faker().Person.Phone;  // Generate a fake phone number.
        await PhoneNumberInputLocator.FillAsync(phoneNumber); 
    }

    /// <summary>
    /// Fills the email address input field with a generated fake email address.
    /// Uses the Bogus library to generate the email address.
    /// </summary>
    private async Task FillEmailId()
    {
        var emailId = new Faker().Person.Email;  // Generate a fake email address.
        await EmailAddressLocator.FillAsync(emailId);
    }

    /// <summary>
    /// Fills the contact details (phone number and email address), then validates that the "Check Order" button is displayed.
    /// </summary>
    public async Task EnterContactDetailsAndValidateCheckOrderButtonIsDisplayed()
    {
        await WaitTillPageIsLoaded();  
        await FillPhoneNumber();  
        await FillEmailId(); 
        await ValidateCheckOrderButtonIsDisplayed(); 
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the URL.
    /// Ensures that the content is fully available before interacting with any elements.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForURLAsync($"{TestSetUp.Url}/duurzame-energie/bestellen2/je-gegevens/contactgegevens/");
    }
}
