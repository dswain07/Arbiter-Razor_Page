using Arbiter.Data.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Razor Pages.
builder.Services.AddRazorPages(options =>
{
    // Protect all pages inside the Admin folder.
    options.Conventions.AuthorizeFolder(
        "/Admin",
        "AdminOnly"
    );
});

// Configure SQLite database.
builder.Services.AddDbContext<ArbiterDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString(
            "DefaultConnection"
        )
    )
);

// Configure cookie authentication.
builder.Services
    .AddAuthentication(
        CookieAuthenticationDefaults.AuthenticationScheme
    )
    .AddCookie(options =>
    {
        options.LoginPath = "/AdminLogin";
        options.AccessDeniedPath = "/AccessDenied";

        options.Cookie.Name = "Arbiter.AdminAuth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

// Configure authorization policy.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Administrator");
    });
});

var app = builder.Build();

// Configure HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Prevent caching of administration responses.
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/Admin"))
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.CacheControl =
                "no-store, no-cache, must-revalidate";

            context.Response.Headers.Pragma = "no-cache";
            context.Response.Headers.Expires = "0";

            return Task.CompletedTask;
        });
    }

    await next();
});


// Authentication must run before authorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();


if (app.Environment.IsDevelopment())
{
    app.MapGet("/debug/routes", (
        IEnumerable<EndpointDataSource> sources) =>
    {
        return sources
            .SelectMany(source => source.Endpoints)
            .OfType<RouteEndpoint>()
            .Select(endpoint => new
            {
                Route = endpoint.RoutePattern.RawText,
                Methods = endpoint.Metadata
                    .GetMetadata<HttpMethodMetadata>()?
                    .HttpMethods
            })
            .Where(endpoint =>
                endpoint.Route != null &&
                endpoint.Route.Contains("AdminLogout"))
            .ToList();
    });
}


// Initialize database.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ArbiterDbContext>();

    Arbiter.Web.Data.DbInitializer.Initialize(context);
}

app.Run();