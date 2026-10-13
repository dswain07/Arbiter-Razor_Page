using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Reviews;

public class DetailsModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public DetailsModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public Review? Review { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Review = await _context.Reviews
            .FirstOrDefaultAsync(a => a.Id == id);

        if (Review == null)
            return NotFound();

        return Page();
    }
}