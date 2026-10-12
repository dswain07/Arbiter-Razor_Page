using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Papers.Add(Paper);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}