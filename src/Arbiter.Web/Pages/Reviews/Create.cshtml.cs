
using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Reviews;

public class CreateModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public CreateModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Review Review { get; set; } = new();

    public SelectList PaperList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        await LoadPapersAsync();
    }

    private async Task LoadPapersAsync()
    {
        var papers = await _context.Papers
            .OrderBy(p => p.Title)
            .ToListAsync();

        PaperList = new SelectList(
            papers,
            "Id",
            "Title"
        );
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadPapersAsync();
            return Page();
        }

        bool paperExists = await _context.Papers
            .AnyAsync(p => p.Id == Review.PaperId);

        if (!paperExists)
        {
            ModelState.AddModelError(
                "Review.PaperId",
                "Please select a valid paper."
            );

            await LoadPapersAsync();
            return Page();
        }

        _context.Reviews.Add(Review);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
