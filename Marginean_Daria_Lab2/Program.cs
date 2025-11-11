using Microsoft.EntityFrameworkCore;
using Marginean_Daria_Lab2.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Asta e conexiunea ta existentă pentru cărți
builder.Services.AddDbContext<Marginean_Daria_Lab2Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Marginean_Daria_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Marginean_Daria_Lab2Context' not found.")));

// Asta e conexiunea LIPSĂ pentru utilizatori (Identity) pe care o adăugăm
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LibraryIdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'LibraryIdentityContextConnection' not found.")));

// Linia asta adăugată de Identity. Acum va funcționa, pentru că linia de mai sus există.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Asta activează autentificarea. E important să fie DUPĂ UseRouting și ÎNAINTE de UseAuthorization
app.UseAuthentication(); 

app.UseAuthorization();

app.MapRazorPages();

app.Run();