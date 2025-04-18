using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models;
using StarColonies.Web.Middlewares;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Configurations.Seeder;
using StarColonies.Infrastructures.Data.Configurations.Seeder.Map;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;
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
builder.Services.AddDefaultIdentity<ColonistEntity>()
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

await IdentitySeeder.SeedRolesAndUsersAsync(app.Services);
await SeedDataAsync(app);

app.Run();
return;

static async Task SeedDataAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    
    var context = scope.ServiceProvider.GetRequiredService<StarColoniesDbContext>();
    
    await context.Database.MigrateAsync();
    
    MapSeeder.Seed(context);
    ColonieSeeder.Seed(context);
    GiveItems(context);
}

static void GiveItems(StarColoniesDbContext context) 
{
    var colonists = context.Colonists
        .Include(c => c.Inventory)
        .ToList();

    var items = context.Items.ToList();

    foreach (var colon in colonists)
    {
        foreach (var item in from item in items where item.Name != "AK-47" let hasAlreadyItem = colon.Inventory.Any(i => i.ItemId == item.Id) where !hasAlreadyItem select item)
        {
            colon.Inventory.Add(new ColonistItemEntity
            {
                ItemId = item.Id,
                ColonistId = colon.Id
            });
        }
    }

    context.SaveChanges();
}

