using Microsoft.Playwright;
using PlaywrightSpecflowProject.Pages;
using TechTalk.SpecFlow;

namespace PlaywrightSpecflowProject.StepDefinitions;

[Binding]
public class HomePageStepDefs
{
    private readonly ScenarioContext _scenarioContext;
    private IPage page;
    private HomePage homePage = null!;

    public HomePageStepDefs(ScenarioContext scenarioContext, Hooks.Hooks hooks)
    {
        _scenarioContext = scenarioContext;
        page = hooks.page.Value!;
    }

    [Given(@"I navigate to the homepage")]
    public async Task GivenInavigatetothehomepage()
    {
        homePage = new(page);
        await homePage.LaunchHomepage();
    }

    [When(@"I go to the login page")]
    public async Task WhenIgototheloginpage()
    {
        await Assertions.Expect(page).ToHaveTitleAsync("Your Store");
    }

    [When(@"I enter username ""(.*)""")]
    public async Task GivenIenterusername(string args1)
    {
        await page.TitleAsync();
    }

    [When(@"I enter password ""(.*)""")]
    public async Task GivenIenterpassword(string args1)
    {
        await page.TitleAsync();
    }

    [When(@"I click login button")]
    public async Task GivenIclickloginbutton()
    {
        await page.TitleAsync();
    }

    [Then(@"I should land on my profile page")]
    public async Task ThenIshouldlandonmyprofilepage()
    {
        await page.TitleAsync();
    }

}
