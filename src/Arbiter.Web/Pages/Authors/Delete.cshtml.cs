using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Arbiter.Web.Pages.Authors;

public class DeleteModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public DeleteModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Author Author { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var author = await _context.Authors.FindAsync(id);

        if (author == null)
            return NotFound();

        Author = author;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var author = await _context.Authors.FindAsync(id);

        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}