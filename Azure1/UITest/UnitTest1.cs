using OpenQA.Selenium;

namespace UITest;

public class UnitTest1 : IClassFixture<DriverFixture>
{
    readonly DriverFixture _fixture;
    readonly IWebDriver _driver;
    public UnitTest1(DriverFixture fixture)
    {
        _fixture = fixture;
        _driver = _fixture.Driver;
    }
    [Fact]
    public void Test1()
    {
        _driver.Navigate().GoToUrl("https://localhost:7225");

        Thread.Sleep(500);

        var site2Header = DriverHelpers.WaitForElement(_driver, By.XPath("/html/body/div[1]/main/article/h1"));
        Assert.NotNull(site2Header);
        Assert.Equal("Blazor App Number 2", site2Header.Text);

        var otherSiteLink = DriverHelpers.WaitForElement(_driver, By.XPath("/html/body/div[1]/main/article/a"));
        Assert.NotNull(otherSiteLink);
        otherSiteLink.Click();

        var site1Header = DriverHelpers.WaitForElement(_driver, By.XPath("/html/body/div[1]/main/article/h1"));
        Assert.NotNull(site1Header);
        Assert.Equal("Blazor App Number 1", site1Header.Text);
    }

    [Fact]
    [DatabaseCleaner]
    public void TestDatabaseActions()
    {
        DriverHelpers.NavigateAndWaitForPage(_driver, "https://localhost:7005");

        //click create button
        Assert.True(DriverHelpers.Click(_driver, By.XPath("/html/body/div[1]/main/article/button[1]")));

        Thread.Sleep(1000);

        //go to azure 2
        Assert.True(DriverHelpers.Click(_driver, By.LinkText("Go to Azure 2")));

        Thread.Sleep(1000);

        //click the Azure 2 fetch button
        Assert.True(DriverHelpers.Click(_driver, By.XPath("/html/body/div[1]/main/article/button[2]")));

        Thread.Sleep(1000);

        //ensure we fetch Azure 1 User
        var userText = DriverHelpers.WaitForElement(_driver, By.XPath("/html/body/div[1]/main/article/p"));
        Assert.NotNull(userText);
        Assert.Equal("Azure 1 User", userText.Text);

        //go back to azure 1
        Assert.True(DriverHelpers.Click(_driver, By.LinkText("Go to Azure 1")));

        //click the Azure 1 delete button
        Assert.True(DriverHelpers.Click(_driver, By.XPath("/html/body/div[1]/main/article/button[3]")));

        Thread.Sleep(1000);

        //go back to Azure 2
        Assert.True(DriverHelpers.Click(_driver, By.LinkText("Go to Azure 2")));

        Thread.Sleep(1000);

        //re-click the Azure 2 fetch button
        Assert.True(DriverHelpers.Click(_driver, By.XPath("/html/body/div[1]/main/article/button[2]")));

        Thread.Sleep(1000);

        //ensure we fetch nothing because it was deleted on Azure 1
        userText = DriverHelpers.WaitForElement(_driver, By.XPath("/html/body/div[1]/main/article/p"));
        Assert.NotNull(userText);
        Assert.Equal("Search Found Nothing", userText.Text);
    }
}