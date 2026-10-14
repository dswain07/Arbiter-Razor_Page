
using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Papers;

public class IndexModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public IndexModel(ArbiterDbContext context)
    {
        _context = context;
    }

    public IList<Paper> Papers { get; set; } = new List<Paper>();

    public SelectList TrackList { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SearchType { get; set; } = "All";

    [BindProperty(SupportsGet = true)]
    public string? SelectedStatus { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? SelectedTrackId { get; set; }

    public async Task OnGetAsync()
    {
        // Load tracks for the filter dropdown.
        var tracks = await _context.Tracks
            .OrderBy(t => t.Name)
            .ToListAsync();

        TrackList = new SelectList(
            tracks,
            "Id",
            "Name",
            SelectedTrackId
        );

        // Start with all papers.
        var query = _context.Papers
            .Include(p => p.Track)
            .AsNoTracking()
            .AsQueryable();

        // Apply text search.
        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            string term = SearchTerm.Trim();

            switch (SearchType)
            {
                case "Id":
                    if (int.TryParse(term, out int paperId))
                    {
                        query = query.Where(p => p.Id == paperId);
                    }
                    else
                    {
                        query = query.Where(p => false);
                    }
                    break;

                case "Title":
                    query = query.Where(p =>
                        p.Title.Contains(term));
                    break;

                case "Abstract":
                    query = query.Where(p =>
                        p.Abstract.Contains(term));
                    break;

                case "Keywords":
                    query = query.Where(p =>
                        p.Keywords != null &&
                        p.Keywords.Contains(term));
                    break;

                default:
                    // Search all supported fields.
                    bool isId = int.TryParse(term, out int id);

                    query = query.Where(p =>
                        (isId && p.Id == id) ||
                        p.Title.Contains(term) ||
                        p.Abstract.Contains(term) ||
                        (p.Keywords != null &&
                        p.Keywords.Contains(term)));
                    break;
            }
        }

        // Apply status filter.
        if (!string.IsNullOrWhiteSpace(SelectedStatus))
        {
            query = query.Where(p =>
                p.Status == SelectedStatus);
        }

        // Apply track filter.
        if (SelectedTrackId.HasValue &&
            SelectedTrackId.Value > 0)
        {
            query = query.Where(p =>
                p.TrackId == SelectedTrackId.Value);
        }

        // Execute the combined query.
        Papers = await query
            .OrderBy(p => p.Id)
            .ToListAsync();
    }
}
