using Microsoft.Playwright;
using PlaywrightSpecflowProject.Utilities;
using TechTalk.SpecFlow;

namespace PlaywrightSpecflowProject.Hooks;

[Binding]
public class Hooks
{
    private ThreadLocal<IPlaywright> playwright = new();
    public ThreadLocal<IBrowser> browser { get; private set; } = new();
    public ThreadLocal<IBrowserContext> context { get; private set; } = new();
    public ThreadLocal<IPage> page { get; private set; } = new();

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Properties.ReadProperties();
        ExtentReport.InitExtentReport();
    }

    // [BeforeFeature]
    // public static void BeforeFeature(FeatureContext fc) =>
    //     ExtentReport.CreateFeatureNode(fc.FeatureInfo.Title);

    [Before]  // BeforeScenario
    public async Task BeforeScenarioHook(ScenarioContext sc, FeatureContext fc)
    {
        string browserName = Properties.browser.ToLower();
        playwright.Value = await Playwright.CreateAsync();
        browser.Value = await GetBrowserInstance(browserName);
        context.Value = await browser.Value.NewContextAsync(new() { ViewportSize = ViewportSize.NoViewport });
        page.Value = await context.Value.NewPageAsync();

        ExtentReport.CreateTest(sc.ScenarioInfo.Title, fc.FeatureInfo.Title);
    }

    [AfterScenario]
    public async Task CleanUp() => await context.Value!.CloseAsync();

    [AfterTestRun]
    public static void AfterTest() => ExtentReport.FlushReport();

    [AfterStep]
    public async Task AddStepsToReport(ScenarioContext sc)
    {
        string stepType = sc.StepContext.StepInfo.StepDefinitionType.ToString();
        string stepText = sc.StepContext.StepInfo.Text;
        string stepInfo = $"<b>{stepType}</b> {stepText}";

        if (sc.TestError == null)
            ExtentReport.AddPassedStep(stepInfo);
        else
            ExtentReport.AddFailedStep(stepInfo, sc.TestError.Message, await CommonUtilities.TakeScreenshot(page.Value!));
    }

    private async Task<IBrowser> GetBrowserInstance(string browserName)
    {
        if (browserName.Equals("chrome"))
        {
            return await playwright.Value!.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Channel = "chrome",
                Args = ["--start-maximized"]
            });
        }
        else if (browserName.Contains("edge"))
        {
            return await playwright.Value!.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Channel = "msedge",
                Args = ["--start-maximized"]
            });
        }
        else if (browserName.Equals("firefox"))
        {
            return await playwright.Value!.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = ["--kiosk"]
            });
        }
        else if (browserName.Equals("safari"))
        {
            return await playwright.Value!.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                // Channel = "chrome",
                // Args = ["--start-maximized"]
            });
        }
        else
        {
            return await playwright.Value!.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = ["--start-maximized"]
            });
        }
    }
}