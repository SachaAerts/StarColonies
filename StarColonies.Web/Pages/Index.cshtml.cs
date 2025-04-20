using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class IndexModel(ILogger<IndexModel> logger, IColonyRepository colonyRepository)
    : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    public IList<ColonyModel> TopColonies { get; set; } = [];

    public async Task OnGetAsync()
    {
        TopColonies = await colonyRepository.GetTop10ColoniesAsync();
        foreach (var colony in TopColonies)
        {
            Console.WriteLine("Image: " + colony.LogoPath + "\n");
        }
    }
}