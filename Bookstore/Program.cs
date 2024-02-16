using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bookstore.Data;
using Microsoft.AspNetCore.Identity;
using Bookstore.Models;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookstoreContext") ?? throw new InvalidOperationException("Connection string 'BookstoreContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddIdentity<Account, Role>()
                    .AddRoles<Role>()
                    .AddRoleManager<RoleManager<Role>>()
                    .AddSignInManager<SignInManager<Account>>()
                    .AddRoleValidator<RoleValidator<Role>>()
                    .AddEntityFrameworkStores<BookstoreContext>();


// Konfiguracja opcji cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    // Ustawienie czasu wygaśnięcia cookie
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Przykładowy czas wygaśnięcia
    options.SlidingExpiration = true; // Odświeżanie cookie przy aktywności użytkownika

    // Kluczowe dla nietrwałej sesji - cookie nie są zapisywane po zamknięciu przeglądarki
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.LoginPath = "/Account/Login"; // Ścieżka do Twojego logowania
    options.LogoutPath = "/Account/Logout"; // Ścieżka do wylogowania
    options.AccessDeniedPath = "/Account/AccessDenied"; // Ścieżka do strony "Odmowa dostępu"
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BookstoreContext>();
        var userManager = services.GetRequiredService<UserManager<Account>>();
        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        await DbInitializer.Initialize(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database with roles and users.");
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Umożliwia uwierzytelnianie
app.UseAuthorization(); // Umożliwia autoryzację

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
