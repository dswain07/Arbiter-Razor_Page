using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Admin.Tracks;

public class IndexModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public IndexModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public IList<Track> Tracks { get; set; }
        = new List<Track>();

    public async Task OnGetAsync()
    {
        Tracks = await _context.Tracks
            .Include(t => t.Papers)
            .OrderBy(t => t.Name)
            .AsNoTracking()
            .ToListAsync();
    }
}
