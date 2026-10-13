using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Arbiter.Web.Pages.Papers;

public class DeleteModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public DeleteModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Paper Paper { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var paper = await _context.Papers.FindAsync(id);

        if (paper == null)
            return NotFound();

        Paper = paper;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var paper = await _context.Papers.FindAsync(id);

        if (paper != null)
        {
            _context.Papers.Remove(paper);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}