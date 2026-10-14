using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Admin.Tracks;

public class CreateModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public CreateModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Track Track { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        string trackName = Track.Name.Trim();

        bool exists = await _context.Tracks
            .AnyAsync(t => t.Name == trackName);

        if (exists)
        {
            ModelState.AddModelError(
                "Track.Name",
                "A track with this name already exists."
            );

            return Page();
        }

        Track.Name = trackName;
        Track.Description = Track.Description.Trim();

        _context.Tracks.Add(Track);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
