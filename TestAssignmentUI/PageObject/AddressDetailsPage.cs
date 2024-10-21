using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUI.SetUp;
using TestAssignmentUI.Utilities;

namespace TestAssignmentUI.PageObject
{
    /// <summary>
    /// Represents the AddressDetails page of the application and provides methods to interact with its elements.
    /// Inherits from <see cref="PageTest"/>, enabling the use of Playwright's testing utilities.
    /// </summary>
    public class AddressDetailsPage : PageTest
    {
        // Private field to hold the current browser page instance
        private readonly IPage _currentPage;

        // Utility class instance to fetch locators
        private readonly GetLocator _getLocator;

        // CSS selectors for elements on the AddressDetails page
        private const string IsLivingAtGivenAddressRadioSelector = "label[data-label='Ja']";
        private const string NextButtonSelector = "button[type='submit']";
        private const string NotificationBoxSelector = "div[class*='notification-box']";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressDetailsPage"/> class with the provided <see cref="IPage"/> instance.
        /// </summary>
        /// <param name="page">The current browser page instance.</param>
        public AddressDetailsPage(IPage page)
        {
            _currentPage = page;
            _getLocator = new GetLocator( _currentPage ); // Initialize GetLocator utility with the current page
        }

        // Locators for elements on the page, obtained using the GetLocator utility

        /// <summary>
        /// Locator for the "Is Living At Given Address" radio button.
        /// </summary>
        private ILocator IsLivingAtGivenAddressRadioButtonLocator => _getLocator.GetILocator( IsLivingAtGivenAddressRadioSelector );

        /// <summary>
        /// Locator for the "Next" button to navigate to the next page.
        /// </summary>
        private ILocator NextButtonLocator => _getLocator.GetILocator( NextButtonSelector );

        /// <summary>
        /// Locator for the notification box that appears after an action.
        /// </summary>
        private ILocator NotificationBoxLocator => _getLocator.GetILocator( NotificationBoxSelector );


        /// <summary>
        /// Clicks the "Next" button to navigate to the next page.
        /// </summary>
        private async Task NavigateToNextPage()
        {
            await NextButtonLocator.ClickAsync();
        }

        /// <summary>
        /// Selects the "Is Living At Given Address" radio button.
        /// </summary>
        private async Task SelectIsLivingAtGivenAddressRadioButton()
        {
            await IsLivingAtGivenAddressRadioButtonLocator.Locator( "button" ).ClickAsync();
        }

        /// <summary>
        /// Validates that the notification box is displayed on the page.
        /// </summary>
        private async Task ValidateNotificationBoxIsDisplayed()
        {
            await Expect( NotificationBoxLocator ).ToBeVisibleAsync();
        }

        /// <summary>
        /// Validates the address details and navigates to the next page.
        /// </summary>
        public async Task ValidateAddressDetailsAreCorrect()
        {
            await WaitTillPageIsLoaded();
            await SelectIsLivingAtGivenAddressRadioButton();
            await ValidateNotificationBoxIsDisplayed();
            await NavigateToNextPage();
        }

        /// <summary>
        /// Waits until the page is fully loaded by listening for the DOMContentLoaded event.
        /// Ensures that the content is ready for interaction.
        /// </summary>
        private async Task WaitTillPageIsLoaded()
        {
            await _currentPage.WaitForLoadStateAsync( LoadState.DOMContentLoaded );
            await _currentPage.WaitForURLAsync( $"{TestSetUp.Url}/duurzame-energie/bestellen2/je-gegevens/adresgegevens/" );
        }
    }
}