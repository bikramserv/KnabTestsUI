# Introduction 

UI integrations test for login to a Trell and Create a Trello Board

- Category("TrelloTest")]
 

1 solution with 1 projects using playwright
azure test pipeline runs test using dotnet command to build and test.


# Getting Started

These UI tests make use of `playwright`. More information on:
    - https://playwright.dev/dotnet/docs/intro

Test classes are based on

- PageTest
- ContextTest
- BrowserTest
- PlaywrightTest


##	Installation on local machine

# TL;DR;

Use a peace of code to install the browsers inside the project. Using the steps below works on development machine, but not in a docker container.
See https://www.meziantou.net/distributing-applications-that-depend-on-microsoft-playwright.htm

```
using Microsoft.Playwright;

Console.WriteLine("Installing browsers");

// The following line installs the default browsers. If you only need a subset of browsers,
// you can specify the list of browsers you want to install among: chromium, chrome,
// chrome-beta, msedge, msedge-beta, msedge-dev, firefox, and webkit.
// var exitCode = Microsoft.Playwright.Program.Main(new[] { "install", "webkit", "chrome" });
var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
if (exitCode != 0)
{
    Console.WriteLine("Failed to install browsers");
    Environment.Exit(exitCode);
}

Console.WriteLine("Browsers installed");

// Using playwright to launch a browser
using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync();
await using var browserContext = await browser.NewContextAsync();
var page = await browserContext.NewPageAsync();
await page.GotoAsync("https://www.meziantou.net");
Console.WriteLine(await page.InnerTextAsync("h1"));
```

- Create Nunit test project
  - `dotnet new nunit -n KnabTestsUI`

- Add project dependency
  - `cd KnabTestsUI`
  -  `cd .\KnabTestsUI\`
  - `dotnet add package Microsoft.Playwright.NUnit`

- Build the project
  - `dotnet build`

If encountering `401` to access repositories then use, `dotnet restore --interactive`

- Install playwright CLI and required browsers
  - `dotnet tool install -g Microsoft.Playwright.CLI`
  - `playwright install`
  If not possible to install, navigate to a different folder without a dotnet project like `e:\temp` and install it globally.

  Optional, specify specific version like `dotnet tool install -g Microsoft.Playwright.CLI --version 1.2.2`
  - `dotnet tool search playwright` Look for available versions
  - `dotnet tool list -g` List the globally installed tools

# Update playwright nuget packages

After update nuget package, also issue command `playwright install` to download the correspending browsers.

# Build and Test

- `dotnet test`
- `dotnet test --filter="TestCategory=TrelloTest"`


Test showing the browser. Set environment variable `headless_browser` to false:
Example, using powershell:

- `$env:headless_browser='false'`

Using 1 parallel test workers.
- `dotnet test -- NUnit.NumberOfTestWorkers=1`
# Settings

Appsettings value:
 - `"browser_slowmo": 500, `
    Use wait 500 mms per click to prevent errors like `waiting for selector ".pagination a:has-text('All'):visible" to be visible`

dotnet publish -o ./release         

# Test new playwright release
!! Currently mcr.microsoft.com/playwright:v1.18.0-focal is not working

docker run -it --rm --ipc=host mcr.microsoft.com/playwright:v1.18.0-focal /bin/bash
> npx playwright install chrome
> ls /ms-playwright

# Some remark

- QuerySelectorAllAsync is discouraged, use Locator, see `https://playwright.dev/dotnet/docs/api/class-elementhandle`.
- Autorefresh/autoreload pages are difficult to test, so try to disable that feature when testing.

# Debug locally using Test method Run/Debug command
- Enable "OmniSharp Use Modern Net" in File>Preferences>Settings, searching "useModernNet". That will install OmiSharp for .NET 6 and prevent build error because of a "ref" folder (Same as in https://stackoverflow.com/questions/71356435/cannot-run-or-debug-c-sharp-tests-with-codelens-in-vscode).
- Use appsettings.Development.json to define your own parameters.
- 


=======
