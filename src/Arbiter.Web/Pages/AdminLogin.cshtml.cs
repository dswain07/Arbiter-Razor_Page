using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Arbiter.Web.Pages;

public class AdminLoginModel : PageModel
{
    private readonly IConfiguration _configuration;

    public AdminLoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Password))
        {
            ModelState.AddModelError(
                nameof(Password),
                "Please enter the administrator password."
            );

            return Page();
        }

        string? configuredPassword =
            _configuration["AdminAuth:Password"];

        if (string.IsNullOrEmpty(configuredPassword))
        {
            ModelState.AddModelError(
                string.Empty,
                "Administrator authentication is not configured."
            );

            return Page();
        }

        byte[] enteredHash = SHA256.HashData(
            Encoding.UTF8.GetBytes(Password)
        );

        byte[] configuredHash = SHA256.HashData(
            Encoding.UTF8.GetBytes(configuredPassword)
        );

        bool validPassword =
            CryptographicOperations.FixedTimeEquals(
                enteredHash,
                configuredHash
            );

        if (!validPassword)
        {
            ModelState.AddModelError(
                string.Empty,
                "Invalid administrator password."
            );

            return Page();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "Administrator"),
            new Claim(ClaimTypes.Role, "Administrator")
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        if (!string.IsNullOrEmpty(ReturnUrl) &&
            Url.IsLocalUrl(ReturnUrl))
        {
            return LocalRedirect(ReturnUrl);
        }

        return RedirectToPage("/Admin/Tracks/Index");
    }
}