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

    public static IWebElement? WaitForElement(IWebDriver driver, By byId, int seconds = 10, int intervalMs = 500)
    {
        DateTime startRef = DateTime.Now;
        while (DateTime.Now.Subtract(startRef) < TimeSpan.FromSeconds(seconds))
        {
            try
            {
                var wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(seconds), TimeSpan.FromMilliseconds(intervalMs));
                return wait.Until(ExpectedConditions.ElementIsVisible(byId));
            }
            catch (Exception)
            {
                //swallow and try again
            }
        }
        return null;
    }

    public static bool Click(IWebDriver driver, By byId, int timeout = 10)
    {
        DateTime startRef = DateTime.Now;
        while (DateTime.Now.Subtract(startRef) < TimeSpan.FromSeconds(timeout))
        {
            try
            {
                new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(timeout), TimeSpan.FromMilliseconds(1000))
                .Until(ExpectedConditions.ElementToBeClickable(byId)).Click();
                return true;
            }
            catch (Exception)
            {
                //swallow exception and try again
            }
        }
        return false;
    }

    public static void ScrollToBottom(IWebDriver driver)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        //wait for animation
        Thread.Sleep(500);
    }

    public static void WaitForPage(IWebDriver driver, string url, int timeout = 10)
    {
        DefaultWait<IWebDriver> fluentWait = new(driver)
        {
            Timeout = TimeSpan.FromSeconds(timeout),
            PollingInterval = TimeSpan.FromMilliseconds(250)
        };
        fluentWait.Until(x => x.Url = url);
    }

    public static void WaitForPageContains(IWebDriver driver, string urlFragment, int timeout = 10)
    {
        DefaultWait<IWebDriver> fluentWait = new(driver)
        {
            Timeout = TimeSpan.FromSeconds(timeout),
            PollingInterval = TimeSpan.FromMilliseconds(250)
        };
        fluentWait.Until(x => x.Url.Contains(urlFragment));
    }

    public static void NavigateAndWaitForPage(IWebDriver driver, string url, int timeout = 10)
    {
        driver.Navigate().GoToUrl(url);
        WaitForPage(driver, url);
    }
}
