
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugReportWeb;

public class StatusModel : PageModel
{
    private IBugReportProvider _provider;

    public int id { get;set;}
    public string name {get;set;}

    public StatusModel(IBugReportProvider provider)
    {
        _provider = provider;
    }

    public void OnGet()
    {
        id = 1;
        name = "bla";
    }
}