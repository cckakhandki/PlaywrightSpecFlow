using Newtonsoft.Json;

namespace PlaywrightSpecflowProject.Utilities;

public class Properties {

    public static IDictionary<string, object> props {get; private set;} = new Dictionary<string, object>();

    public static string appURL = null!;
    public static string browser = null!;

    public static void ReadProperties() {
        string propsFilePath = CommonUtilities.GetFilePath("/configs/appConfigs.json");

        var propsFile = File.ReadAllText(propsFilePath);
        props = JsonConvert.DeserializeObject<Dictionary<string, object>>(propsFile)!;

        appURL = (string)props["appURL"];
        browser = (string)props["browser"];
    }
}