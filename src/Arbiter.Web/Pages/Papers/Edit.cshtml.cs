
using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Papers;

public class EditModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public EditModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Paper Paper { get; set; } = default!;

    public SelectList TrackList { get; set; } = default!;

    private async Task LoadTracksAsync()
    {
        var tracks = await _context.Tracks
            .OrderBy(t => t.Name)
            .ToListAsync();

        TrackList = new SelectList(
            tracks,
            "Id",
            "Name"
        );
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var paper = await _context.Papers.FindAsync(id);

        if (paper == null)
            return NotFound();

        Paper = paper;

        await LoadTracksAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id != Paper.Id)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            await LoadTracksAsync();
            return Page();
        }

        bool trackExists = await _context.Tracks
            .AnyAsync(t => t.Id == Paper.TrackId);

        if (!trackExists)
        {
            ModelState.AddModelError(
                "Paper.TrackId",
                "Please select a valid track."
            );

            await LoadTracksAsync();
            return Page();
        }

        var existingPaper = await _context.Papers.FindAsync(id);

        if (existingPaper == null)
            return NotFound();

        existingPaper.Title = Paper.Title;
        existingPaper.Abstract = Paper.Abstract;
        existingPaper.Keywords = Paper.Keywords;
        existingPaper.Status = Paper.Status;
        existingPaper.TrackId = Paper.TrackId;
        existingPaper.SubmissionDate = Paper.SubmissionDate;

        // Keep the original SubmissionDate unchanged.
        existingPaper.LastModifiedDate = DateTime.Today;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
