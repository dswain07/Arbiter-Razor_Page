using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Authorss;

public class IndexModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public IndexModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public IList<Author> Authors { get; set; } = new List<Author>();

    public async Task OnGetAsync()
    {
        Authors = await _context.Authors.ToListAsync();
    }
}