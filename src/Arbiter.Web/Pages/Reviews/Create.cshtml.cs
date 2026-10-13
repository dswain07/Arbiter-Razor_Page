using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Arbiter.Web.Pages.Reviews;

public class CreateModel : PageModel
{
    private readonly ArbiterDbContext _context;

    public CreateModel(ArbiterDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Review Review { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Reviews.Add(Review);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}