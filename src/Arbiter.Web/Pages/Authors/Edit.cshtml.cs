using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Pages.Authors;

public class EditModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public EditModel(ArbiterDbContext context)
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.Attach(Author).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}