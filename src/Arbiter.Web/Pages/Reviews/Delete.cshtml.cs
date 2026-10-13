
using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Reviews;

public class DeleteModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public DeleteModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Review Review { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var review = await _context.Reviews
            .Include(r => r.Paper)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (review == null)
            return NotFound();

        Review = review;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var review = await _context.Reviews.FindAsync(id);

        if (review == null)
            return NotFound();

        _context.Reviews.Remove(review);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
