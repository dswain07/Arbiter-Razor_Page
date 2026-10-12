using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Papers;

public class IndexModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public IndexModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public IList<Paper> Papers { get; set; } = new List<Paper>();

    public async Task OnGetAsync()
    {
        Papers = await _context.Papers
            .Include(p => p.Track)
            .ToListAsync();
    }
}