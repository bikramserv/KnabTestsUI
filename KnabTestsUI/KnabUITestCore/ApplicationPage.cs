using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using KnabTestsUI.Models;

namespace KnabTestsUI.KnabUITestCore;

public class ApplicationPage
{

    protected readonly IPage _page;

    public ApplicationPage(IPage page)
    {
        _page = page;
    }


    public async Task LoginAsync(string username, string password)
    {
        await _page.FillAsync("input[id='username']", username);
        await _page.FillAsync("input[id='password']", password);
        await _page.ClickAsync("#login-submit");

        // Wait until user account menu is visible, so we are logged in            
        // await _page.Locator("header button span.user_name").WaitForAsync();
    }

    public async Task LoginAsynconlyUserLogin(string username)
    {
        await _page.FillAsync("input[id='username']", username);
        //  await _page.FillAsync("input[id='password']", password);
        await _page.ClickAsync("#login-submit");

        // Wait until user account menu is visible, so we are logged in            
        // await _page.Locator("header button span.user_name").WaitForAsync();
    }

}
