using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Admin.Tracks;

public class EditModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public EditModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Track Track { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var track = await _context.Tracks.FindAsync(id);

        if (track == null)
            return NotFound();

        Track = track;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (Track == null || id != Track.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return Page();

        string trackName = Track.Name.Trim();

        bool nameExists = await _context.Tracks
            .AnyAsync(t =>
                t.Name == trackName &&
                t.Id != id);

        if (nameExists)
        {
            ModelState.AddModelError(
                "Track.Name",
                "Another track with this name already exists."
            );

            return Page();
        }

        var existingTrack = await _context.Tracks.FindAsync(id);

        if (existingTrack == null)
            return NotFound();

        existingTrack.Name = trackName;
        existingTrack.Description = Track.Description.Trim();
        existingTrack.MaxQuota = Track.MaxQuota;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}