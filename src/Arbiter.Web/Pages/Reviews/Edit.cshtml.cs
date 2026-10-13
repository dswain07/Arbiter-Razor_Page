
using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Reviews;

public class EditModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public EditModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Review Review { get; set; } = default!;

    public SelectList PaperList { get; set; } = default!;

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

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var review = await _context.Reviews.FindAsync(id);

        if (review == null)
            return NotFound();

        Review = review;

        await LoadPapersAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id != Review.Id)
            return BadRequest();

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

        var existingReview = await _context.Reviews.FindAsync(id);

        if (existingReview == null)
            return NotFound();

        existingReview.PaperId = Review.PaperId;
        existingReview.ReviewerName = Review.ReviewerName;
        existingReview.Score = Review.Score;
        existingReview.Comments = Review.Comments;
        existingReview.ReviewDate = Review.ReviewDate;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
