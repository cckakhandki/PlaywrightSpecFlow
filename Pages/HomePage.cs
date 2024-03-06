using Microsoft.Playwright;
using PlaywrightSpecflowProject.Utilities;

namespace PlaywrightSpecflowProject.Pages;

public class HomePage
{
    private IPage page;

    public HomePage(IPage page) => this.page = page;

    public async Task LaunchHomepage()
    {
        await page.GotoAsync(Properties.appURL);
    }
}