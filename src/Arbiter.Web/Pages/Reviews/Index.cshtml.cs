using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Reviews;

public class IndexModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public IndexModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public IList<Review> Reviews { get; set; } = new List<Review>();

    public async Task OnGetAsync()
    {
        Reviews = await _context.Reviews.ToListAsync();
    }
}