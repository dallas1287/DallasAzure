using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UITest;

public class DriverFixture : IDisposable
{
    public IWebDriver Driver { get; set; }

    public DriverFixture()
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--ignore-ssl-errors=yes");
        chromeOptions.AddArgument("--ignore-certificate-errors");
        chromeOptions.AddArgument("headless");

        Driver = new ChromeDriver(chromeOptions);
    }

    public void Dispose()
    {
        Driver.Close();
        Driver.Quit();
    }
}
