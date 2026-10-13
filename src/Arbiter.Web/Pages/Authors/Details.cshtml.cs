using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Authors;

public class DetailsModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public DetailsModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public Author? Author { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Author = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == id);

        if (Author == null)
            return NotFound();

        return Page();
    }
}