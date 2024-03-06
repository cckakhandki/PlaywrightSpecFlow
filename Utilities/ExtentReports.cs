using System.Data.SqlTypes;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace PlaywrightSpecflowProject.Utilities;

public class ExtentReport
{

    private static ExtentReports extentReports = null!;
    // private static ExtentTest feature = null!;
    // public static ExtentTest scenario = null!;
    public static ThreadLocal<ExtentTest> test = new();

    public static void InitExtentReport()
    {
        string reportPath = Path.Combine(CommonUtilities.GetProjectDirectoryPath, "Reports", "spark_HTML_Report.html");
        string jsonConfigPath = Path.Combine(CommonUtilities.GetProjectDirectoryPath, "configs", "extentconfigs.json");

        var htmlReporter = new ExtentSparkReporter(reportPath);
        if (jsonConfigPath != null)
            htmlReporter.LoadJSONConfig(jsonConfigPath);
        else
        {
            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "SpecFlow Test Report";
            htmlReporter.Config.TimelineEnabled = true;
        }

        extentReports = new();
        extentReports.AttachReporter(htmlReporter);
        extentReports.AddSystemInfo("OS", "Windows");
        extentReports.AddSystemInfo("Browser", Properties.browser);
        extentReports.AddSystemInfo("App URL", Properties.appURL);
    }

    public static void FlushReport() => extentReports.Flush();

    // public static void CreateFeatureNode(string featureTitle) => feature = extentReports.CreateTest<Feature>(featureTitle);

    // public static void CreateScenarionNode(string scenarioTitle) => scenario = feature.CreateNode<Scenario>(scenarioTitle);

    // public static void CreatePassedStep(string stepType, string stepInfo)
    // {
    //     if (stepType.Equals("Given"))
    //         scenario.CreateNode<Given>(stepInfo);
    //     else if (stepType.Equals("When"))
    //         scenario.CreateNode<When>(stepInfo);
    //     else if (stepType.Equals("Then"))
    //         scenario.CreateNode<Then>(stepInfo);
    //     else
    //         scenario.CreateNode<And>(stepInfo);
    // }

    // public static void CreateFailedTestStep(string stepType, string stepInfo, string errMsg, string screenshot)
    // {
    //     if (stepType.Equals("Given"))
    //         scenario.CreateNode<Given>(stepInfo).Fail(errMsg,
    //             MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
    //     else if (stepType.Equals("When"))
    //         scenario.CreateNode<When>(stepInfo).Fail(errMsg,
    //             MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
    //     else if (stepType.Equals("Then"))
    //         scenario.CreateNode<Then>(stepInfo).Fail(errMsg,
    //             MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
    //     else
    //         scenario.CreateNode<And>(stepInfo).Fail(errMsg,
    //             MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
    // }

    public static void CreateTest(string scenarioName, string category = null!)
    {
        test.Value = extentReports.CreateTest(scenarioName);
        if (category != null)
            test.Value.AssignCategory(category);
    }

    public static void AddPassedStep(string stepInfo) => test.Value!.Pass(stepInfo);

    public static void AddFailedStep(string stepInfo, SqlString errMsg, string screenshot)
    {
        test.Value!.Fail(stepInfo);
        test.Value!.Log(Status.Error, $"Excception occured: <br>{errMsg}", 
            MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
    }

    public static void LogInfo(string text) {
        test.Value!.Log(Status.Info, text);
    }
}