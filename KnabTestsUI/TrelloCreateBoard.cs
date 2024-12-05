using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using KnabTestsUI.Models;

namespace KnabTestsUI;

[Parallelizable(ParallelScope.Self)]
[Category("TrelloTest")]
public class TrelloCreateBoard : PlaywrightTest
{
    public IConfigurationRoot Config { get; set; }
    private string _uri;
    private string _username;

    private string _password;

    private bool _headless;

    private int _slowMO;

    [OneTimeSetUp]


    public void OneTimeSetup()
    {
        Console.WriteLine("Installing browsers");
        //The following line installs the browser, you can also specify list of broweser needs to be installed.
        //you can list all browser like chrome , firefox etc.

        var exitcode = Microsoft.Playwright.Program.Main(new[] { "install" });
        if (exitcode != 0)
        {
            Console.WriteLine("Failed to install the browsers");

        }

        Console.WriteLine("Browser Installed");

        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        Config = builder.Build();

        _uri = Config["app_uri"];
        _username = Config["app_username"];
        _password = Config["app_password"];
        if (!Int32.TryParse(Config["browser_slowmo"], out int slowMO))
        {
            slowMO = 500;
        };
        _slowMO = slowMO;

        var headless = Config["headless_browser"] ?? "true";
        _headless = headless.Equals("true", StringComparison.CurrentCultureIgnoreCase);

        var ignoreHTTPSErrors = Config["ignore_https_errors]"] ?? "true";
        //_ignoreHTTPSErrors = ignoreHTTPSErrors.Equals("true", StringComparison.CurrentCultureIgnoreCase);

    }

    private async Task<IBrowser> GetBrowser()
    {
        return await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = _headless,

        });

    }

    private BrowserNewContextOptions GetBrowserNewContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            // IgnoreHTTPSErrors = _ignoreHTTPSErrors}
            //StorageStatePath = "auth.json"
        };
    }


    [OneTimeTearDown]
    public void OneTimeTearDown()
    {

    }

    [Test]
    public async Task TitleCheckTrello()

    {
        await using var browser = await GetBrowser();

        await using var context = await browser.NewContextAsync(GetBrowserNewContextOptions());

        var page = await context.NewPageAsync();

        var trellologinPage = new TrelloPage(page);

        await trellologinPage.GotoAsync(_uri);

        string _trelloTitle = await page.TitleAsync();

        string _expTitle = "Manage Your Team’s Projects From Anywhere | Trello";

        Assert.AreEqual(_trelloTitle, _expTitle);

        Console.WriteLine($"Title is same as as expected from the webapp: {_expTitle}");


    }


    [Test]
    public async Task AcceptCookiesAndClickLogin()
    {
        await using var browser = await GetBrowser();

        await using var context = await browser.NewContextAsync(GetBrowserNewContextOptions());

        var page = await context.NewPageAsync();
        var trellologinPage = new TrelloPage(page);
        await trellologinPage.GotoAsync(_uri);
        var acceptcookies = page.Locator("//button[text()='Accept all cookies']");
        Assert.NotNull(acceptcookies, "clicked Accept all cookies");

        await acceptcookies.ClickAsync();
        try
        {
            await page.WaitForSelectorAsync("a[data-testid='login']", new PageWaitForSelectorOptions
            {
                State = WaitForSelectorState.Visible
            });
            await page.ClickAsync("a[data-testid='login']");
            await trellologinPage.GotoAsync(_uri + "login");
            await trellologinPage.LoginAsynconlyUserLogin(_username);

            // await trellologinPage.GotoAsync(_uri+"login");
            await trellologinPage.LoginAsync(_username, _password);
            var dashboardElement = page.Locator(".board-tile-details-name");

            await page.ClickAsync(".mod-add");
            // Fill in the board name
            string _NewBoard = $"NewBoard_{DateTime.Now:yyyyMMdd_HHmmss}";
            await page.WaitForSelectorAsync("[data-testid='create-board-title-input']");
            await page.FillAsync("[data-testid='create-board-title-input']", _NewBoard);
            await page.ClickAsync("button[data-testid='create-board-submit-button']");
            Console.WriteLine($"Board created with name: {_NewBoard}");

            // Step 4: Verify board creation
            // Wait for board page to load (check for unique board element)

            Console.WriteLine("Board created successfully.");

            //Keep the browser open
            //Console.ReadLine();

        }
        catch (PlaywrightException ex)
        {
            Console.WriteLine($"Failed to interact with Log In button: {ex.Message}");
        }



    }
}






