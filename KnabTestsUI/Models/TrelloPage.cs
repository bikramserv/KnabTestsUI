using System.Threading.Tasks;
using KnabTestsUI.KnabUITestCore;
using Microsoft.Playwright;
using System.Globalization;

namespace KnabTestsUI.Models;
public class TrelloPage : ApplicationPage

{
    public CultureInfo cultureinfo;

    public TrelloPage(IPage page) : base(page)
    {
        cultureinfo = new Cultureinfo("en-US");

    }

    public async Task GotoAsync(string url)
    {
        await _page.GotoAsync(url);
    }
}

internal class Cultureinfo : CultureInfo
{
    public Cultureinfo(string name) : base(name)
    {
    }
}