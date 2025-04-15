using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models;
using StarColonies.Web.Middlewares;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Configurations.Seeder.Map;
using StarColonies.Infrastructures.Data.dataclass;
using StarColonies.Web.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ReverseProxyLinksMiddleware>();
builder.Services.AddDbContext<StarColoniesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString
        ("StarColoniesContext"));
});
builder.Services.AddDefaultIdentity<Colonist>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StarColoniesDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions());
app.UseReverseProxyLinks();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

await SeedRolesAndAdminAsync(app); // TODO : DO A SEEDER FOR THE DATABASE AND CALL IN SeedDataAsync

await SeedDataAsync(app);

app.Run();
return;

static async Task SeedDataAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    
    var context = scope.ServiceProvider.GetRequiredService<StarColoniesDbContext>();
    
    // Check if the database exists and create it if it doesn't
    await context.Database.MigrateAsync();
    
    // Seed the database with initial data
    MapSeeder.Seed(context);
}

static async Task SeedRolesAndAdminAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Colonist>>();

    string[] roles = ["Admin", "Player"];
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    
    // Start by seeding the database
    StarColoniesDbContext context = scope.ServiceProvider.GetRequiredService<StarColoniesDbContext>();
    MapSeeder.Seed(context);

    // Données du compte admin par défaut
    const string adminUsername = "admin";
    const string adminEmail = "admin@starcolonies.com";
    const string adminPassword = "Password123_";
    
    Colonist? adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var newAdmin = new Colonist
        {
            UserName = adminUsername,
            Email = adminEmail,
            DateOfBirth = new DateTime(2002, 09, 28),
            Level = 1000,
            Strength = 1003,
            Endurance = 1003,
            Musty = 100000,
            JobModel = JobModel.Engineer
        };

        var result = await userManager.CreateAsync(newAdmin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error creating admin: {error.Description}");
            }
        }
    }
}