using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Web.Middlewares;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Configurations.Seeder;
using StarColonies.Infrastructures.Data.Configurations.Seeder.Map;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Mapper;
using StarColonies.Infrastructures.Mapper.DomainToEntity;
using StarColonies.Infrastructures.Mapper.EntityToDomain;
using StarColonies.Infrastructures.Repositories;
using StarColonies.Web.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ReverseProxyLinksMiddleware>();

// Inject Mapper: Entity -> Domain(Models)
builder.Services.AddScoped<IEntityToDomainMapper<PlanetModel, PlanetEntity>,     PlanetToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<MissionModel, MissionEntity>,   MissionToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<EnemyModel, EnemyEntity>,       EnemyToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<ColonyModel, ColonyEntity>,     ColonyToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<EffectModel, EffectEntity>,     EffectToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<ItemModel, ItemEntity>,         ItemToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<ColonistModel, ColonistEntity>, ColonistToDomainMapper>();

// Inject Mapper: Domain(Models) -> Entity
builder.Services.AddScoped<IDomainToEntityMapper<ColonistEntity, ColonistModel>, ColonistToEntityMapper>();
builder.Services.AddScoped<IDomainToEntityMapper<ColonyEntity, ColonyModel>,     ColonyToEntityMapper>();

// Inject Repositories
builder.Services.AddScoped<IMapRepository, MapRepository>();
builder.Services.AddScoped<IColonyRepository, ColonyRepository>();
builder.Services.AddScoped<IColonistRepository, ColonistRepository>();
builder.Services.AddScoped<IInventaryRepository, InventaryRepository>();

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
    
    Seed(context);
}

static void Seed(StarColoniesDbContext context)
{
    MapSeeder.Seed(context);
    ColonySeeder.Seed(context);
    InventarySeeder.Seed(context);
}