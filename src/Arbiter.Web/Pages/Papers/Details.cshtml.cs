using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Papers;

public class DetailsModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public DetailsModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public Paper? Paper { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Paper = await _context.Papers
            .Include(p => p.Track)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (Paper == null)
            return NotFound();

        return Page();
    }
}