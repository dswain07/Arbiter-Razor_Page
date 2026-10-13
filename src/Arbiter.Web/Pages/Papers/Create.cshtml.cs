
using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Papers;

public class CreateModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public CreateModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Paper Paper { get; set; } = new();

    public SelectList TrackList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        await LoadTracksAsync();
    }

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

    public async Task<IActionResult> OnPostAsync()
    {
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

        Paper.LastModifiedDate = null;

        _context.Papers.Add(Paper);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
