using Microsoft.EntityFrameworkCore;
using Marginean_Daria_Lab2.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Books");
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/Details");
    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Publishers", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categories", "AdminPolicy");
});

builder.Services.AddDbContext<Marginean_Daria_Lab2Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Marginean_Daria_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Marginean_Daria_Lab2Context' not found.")));


builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LibraryIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'LibraryIdentityContextConnection' not found.")));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication(); 

app.UseAuthorization();

app.MapRazorPages();

app.Run();