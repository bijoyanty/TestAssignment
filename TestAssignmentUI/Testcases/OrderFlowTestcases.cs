using Microsoft.Playwright;
using NUnit.Framework;
using TestAssignmentUI.PageObject;
using TestAssignmentUI.SetUp;

namespace TestAssignmentUI.Testcases;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class OrderFlowTestcases : TestSetUp
{
    /// <summary>
    /// This test checks the order flow but does not submit it
    /// </summary>
    [TestCase(new object[] {"9713RD","63", "Irislaan" })]
    [Test, Retry(2)]
    public async Task? ValidateOrderFlowScreens(params string[] address)
    {
        await Page.GotoAsync(Url ?? throw new InvalidOperationException($"Could not navigate to {Url}"), new() { WaitUntil = WaitUntilState.Commit });

        var homepage = new HomePage( Page );
        await homepage.EnterAddressAndStartCalculationOfMonthlyExpense(address);

        var energyTypePage = new EnergyTypePage(Page);
        await energyTypePage.SelectEnergyTypeAndSubmit();

        var energyConsumptionPage = new EnergyConsumptionPage(Page);
        await energyConsumptionPage.SelectEnterConsumptionManuallyAndSubmit();

        var enterConsumptionPage = new EnterConsumptionPage(Page);
        await enterConsumptionPage.EnterConsumptionOfElectricityAndGas();

        var haveSolarPanelOptionPage = new HaveSolarPanelOptionPage(Page);
        await haveSolarPanelOptionPage.SelectNoSolarPanelOptionAndNavigateToNextPage();

        var movingOptionPage = new MovingOptionPage(Page);
        await movingOptionPage.SelectNotMovingOptionAndNavigateToNextPage();


        var choseContractTypePage = new ChoseContractTypePage(Page);
        await choseContractTypePage.ChoseContractTypeAndNavigateToNextPage();

        var reduceCo2OptionPage = new ReduceCo2OptionPage(Page);
        await reduceCo2OptionPage.ValidateReduceCo2OptionIsDisplayedAndNavigateToNextPage();

        var offerPage = new OfferPage(Page);
        await offerPage.ValidateOfferAndNavigateToNextPage();


        var contractStartDatePage = new ContractStartDatePage(Page);
        await contractStartDatePage.FillDeliveryStartDateAndNavigateToNextPage();

        var addressDetailsPage = new AddressDetailsPage(Page);
        await addressDetailsPage.ValidateAddressDetailsAreCorrect();

        var personalInformationPage = new PersonalInformationPage(Page);
        await personalInformationPage.EnterPersonalDetailsAndNavigateToNextPage();

        var contactDetailsPage = new ContactDetailsPage(Page);
        await contactDetailsPage.EnterContactDetailsAndValidateCheckOrderButtonIsDisplayed();
    }

}
