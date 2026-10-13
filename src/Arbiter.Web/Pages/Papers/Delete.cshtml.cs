
using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Papers;

public class DeleteModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public DeleteModel(ArbiterDbContext context)
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var paper = await _context.Papers.FindAsync(id);

        if (paper == null)
            return NotFound();

        bool hasReviews = await _context.Reviews
            .AnyAsync(r => r.PaperId == id);

        bool hasAuthors = await _context.Set<PaperAuthor>()
            .AnyAsync(pa => pa.PaperId == id);

        if (hasReviews || hasAuthors)
        {
            ModelState.AddModelError(
                string.Empty,
                "This paper has associated reviews or authors. " +
                "Remove those relationships before deleting it."
            );

            Paper = await _context.Papers
                .Include(p => p.Track)
                .FirstAsync(p => p.Id == id);

            return Page();
        }

        _context.Papers.Remove(paper);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
