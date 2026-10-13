using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Paper = await _context.Papers.FindAsync(id);

        if (Paper == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.Attach(Paper).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}