using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

public class IndexModel(IColonyRepository colonyRepository)
    : PageModel
{
    public IList<ColonyModel> TopColonies { get; set; } = [];

    public async Task OnGetAsync() 
        => TopColonies = await colonyRepository.GetTop10ColoniesAsync();
}