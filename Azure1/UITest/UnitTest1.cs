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
        _driver.Navigate().GoToUrl("https://localhost:7005");

        var site1Header = DriverHelpers.WaitForElement(_driver, By.XPath("/html/body/div[1]/main/article/h1"));
        Assert.NotNull(site1Header);
        Assert.Equal("Blazor App Number 1", site1Header.Text);

        var otherSiteLink = DriverHelpers.WaitForElement(_driver, By.XPath("/html/body/div[1]/main/article/a"));
        Assert.NotNull(otherSiteLink);
        otherSiteLink.Click();

        var site2Header = DriverHelpers.WaitForElement(_driver, By.XPath("/html/body/div[1]/main/article/h1"));
        Assert.NotNull(site2Header);
        Assert.Equal("Blazor App Number 2", site2Header.Text);
    }
}