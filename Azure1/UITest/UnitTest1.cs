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

        Thread.Sleep(500);

        var pg = _driver.PageSource;

        //Assert.Equal("<html lang=\"en\"><head>\r\n    <meta charset=\"utf-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <base href=\"/\">\r\n    <link rel=\"stylesheet\" href=\"css/bootstrap/bootstrap.min.css\">\r\n    <link href=\"css/site.css\" rel=\"stylesheet\">\r\n    <link href=\"Azure1.styles.css\" rel=\"stylesheet\">\r\n    <link rel=\"icon\" type=\"image/png\" href=\"favicon.png\">\r\n    <!--!--><!--!--><title>Index</title><!--!-->\r\n</head>\r\n<body>\r\n    <!--!--><!--!--><!--!--><!--!--><!--!--><!--!--><!--!--><!--!--><!--!--><!--!--><!--!-->\r\n\r\n<div class=\"page\" b-n5vfhcsesv=\"\"><div class=\"sidebar\" b-n5vfhcsesv=\"\"><!--!--><div class=\"top-row ps-3 navbar navbar-dark\" b-bqvmqhjcoo=\"\"><div class=\"container-fluid\" b-bqvmqhjcoo=\"\"><!--!--><a class=\"navbar-brand\" href=\"\" b-bqvmqhjcoo=\"\">Azure1</a>\r\n        <button title=\"Navigation menu\" class=\"navbar-toggler\" b-bqvmqhjcoo=\"\"><!--!--><span class=\"navbar-toggler-icon\" b-bqvmqhjcoo=\"\"></span></button></div></div><!--!-->\r\n\r\n<div class=\"collapse nav-scrollable\" b-bqvmqhjcoo=\"\"><nav class=\"flex-column\" b-bqvmqhjcoo=\"\"><div class=\"nav-item px-3\" b-bqvmqhjcoo=\"\"><!--!--><a href=\"\" class=\"nav-link active\"><!--!--><span class=\"oi oi-home\" aria-hidden=\"true\" b-bqvmqhjcoo=\"\"></span> Home\r\n            </a></div><!--!-->\r\n        <div class=\"nav-item px-3\" b-bqvmqhjcoo=\"\"><!--!--><a href=\"counter\" class=\"nav-link\"><!--!--><span class=\"oi oi-plus\" aria-hidden=\"true\" b-bqvmqhjcoo=\"\"></span> Counter\r\n            </a></div><!--!-->\r\n        <div class=\"nav-item px-3\" b-bqvmqhjcoo=\"\"><!--!--><a href=\"fetchdata\" class=\"nav-link\"><!--!--><span class=\"oi oi-list-rich\" aria-hidden=\"true\" b-bqvmqhjcoo=\"\"></span> Fetch data\r\n            </a></div></nav></div></div><!--!-->\r\n\r\n    <main b-n5vfhcsesv=\"\"><div class=\"top-row px-4 auth\" b-n5vfhcsesv=\"\"><!--!--><!--!--><!--!--><a href=\"Identity/Account/Register\">Register</a>\r\n        <!--!--><a href=\"Identity/Account/Login\">Log in</a><!--!-->\r\n            <!--!--><a href=\"https://docs.microsoft.com/aspnet/\" target=\"_blank\" b-n5vfhcsesv=\"\">About</a></div><!--!-->\r\n\r\n        <article class=\"content px-4\" b-n5vfhcsesv=\"\"><!--!--><!--!--><!--!--><!--!-->\r\n\r\n<!--!--><h1 tabindex=\"-1\">Blazor App Number 1</h1>\r\n\r\n<!--!--><a href=\"https://localhost:7225\">Go to Azure 2</a>\r\n\r\n<!--!--><div class=\"alert alert-secondary mt-4\"><!--!--><span class=\"oi oi-pencil me-2\" aria-hidden=\"true\"></span>\r\n    <strong>How is Blazor working for you?</strong><!--!-->\r\n\r\n    <!--!--><span class=\"text-nowrap\">\r\n        Please take our\r\n        <a target=\"_blank\" class=\"font-weight-bold link-dark\" href=\"https://go.microsoft.com/fwlink/?linkid=2186158\">brief survey</a></span>\r\n    and tell us what you think.\r\n</div></article></main></div><!--!-->\r\n            <!--!-->\r\n\r\n    <div id=\"blazor-error-ui\">\r\n        \r\n        \r\n            An unhandled exception has occurred. See browser dev tools for details.\r\n        \r\n        <a href=\"\" class=\"reload\">Reload</a>\r\n        <a class=\"dismiss\">🗙</a>\r\n    </div>\r\n\r\n    <script src=\"_framework/blazor.server.js\"></script>\r\n\r\n\r\n</body></html>", pg);

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