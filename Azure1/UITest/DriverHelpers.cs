using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace UITest;

public class DriverHelpers
{
    public static IWebElement? WaitForElementById(IWebDriver driver, string id)
    {
        return WaitForElement(driver, By.Id(id));
    }

    public static IWebElement? WaitForElement(IWebDriver driver, By byId, int seconds = 10)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(byId));
        }
        catch (Exception)
        {
            return null;
        }
    }
}
