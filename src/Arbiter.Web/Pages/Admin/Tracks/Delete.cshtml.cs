using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Admin.Tracks;

public class DeleteModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public DeleteModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public Track? Track { get; set; }

    public int PaperCount { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Track = await _context.Tracks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (Track == null)
            return NotFound();

        PaperCount = await _context.Papers
            .CountAsync(p => p.TrackId == id);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var track = await _context.Tracks.FindAsync(id);

        if (track == null)
            return NotFound();

        bool hasPapers = await _context.Papers
            .AnyAsync(p => p.TrackId == id);

        if (hasPapers)
        {
            ModelState.AddModelError(
                string.Empty,
                "This track cannot be deleted because " +
                "it contains associated papers."
            );

            Track = track;

            PaperCount = await _context.Papers
                .CountAsync(p => p.TrackId == id);

            return Page();
        }

        _context.Tracks.Remove(track);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}