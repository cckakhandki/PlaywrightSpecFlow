using Microsoft.Playwright;

namespace PlaywrightSpecflowProject.Utilities;


public class CommonUtilities
{

    public static string GetProjectDirectoryPath => Directory.GetParent(@"../../../")!.FullName;

    public static string GetFilePath(string relativePath)
    {
        string filePath = GetProjectDirectoryPath + relativePath;
        if (File.Exists(filePath))
            return filePath;
        else
            throw new FileNotFoundException($"File not found at path {filePath}");
    }

    public static async Task TakeScreenshot(IPage page, string fileName)
    {
        string filePath = Path.Combine([GetProjectDirectoryPath, "Reports", fileName + ".png"]);
        await page.ScreenshotAsync(new() { Path = filePath, FullPage = true });
    }

    public static async Task<string> TakeScreenshot(IPage page) => 
        Convert.ToBase64String(await page.ScreenshotAsync());
    
}