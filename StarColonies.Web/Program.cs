using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains.Models;
using StarColonies.Domains.Models.Colony;
using StarColonies.Domains.Models.Items;
using StarColonies.Domains.Models.Missions;
using StarColonies.Domains.Repositories;
using StarColonies.Domains.Services.pictures;
using StarColonies.Infrastructures.Data;
using StarColonies.Infrastructures.Data.Configurations.Seeder;
using StarColonies.Infrastructures.Data.Entities;
using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Data.Seeder;
using StarColonies.Infrastructures.Data.Seeder.Colonist;
using StarColonies.Infrastructures.Data.Seeder.Factories;
using StarColonies.Infrastructures.Mapper;
using StarColonies.Infrastructures.Mapper.DomainToEntity;
using StarColonies.Infrastructures.Mapper.EntityToDomain;
using StarColonies.Infrastructures.Repositories;
using StarColonies.Infrastructures.Services;
using StarColonies.Infrastructures.Services.picture;
using StarColonies.Infrastructures.Services.RewardStrategy;
using StarColonies.Web.Factories;
using StarColonies.Web.Middlewares;
using StarColonies.Web.Services;

//========================== Application Building ==========================//
var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ReverseProxyLinksMiddleware>();

//Inject Database Context
builder.Services.AddDbContext<StarColoniesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString
        ("StarColoniesContext"));
});

//Inject Mapper: Entity -> Domain(Models)
builder.Services.AddScoped<IEntityToDomainMapper<PlanetModel, PlanetEntity>,     PlanetToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<MissionModel, MissionEntity>,   MissionToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<EnemyModel, EnemyEntity>,       EnemyToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<ColonyModel, ColonyEntity>,     ColonyToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<EffectModel, EffectEntity>,     EffectToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<ItemModel, ItemEntity>,         ItemToDomainMapper>();
builder.Services.AddScoped<IEntityToDomainMapper<ColonistModel, ColonistEntity>, ColonistToDomainMapper>();

//Inject Mapper: Domain(Models) -> Entity
builder.Services.AddScoped<IDomainToEntityMapper<ColonistEntity, ColonistModel>, ColonistToEntityMapper>();
builder.Services.AddScoped<IDomainToEntityMapper<ColonyEntity, ColonyModel>,     ColonyToEntityMapper>();
builder.Services.AddScoped<IDomainToEntityMapper<ItemEntity, ItemModel>,         ItemToEntityMapper>();
builder.Services.AddScoped<IDomainToEntityMapper<EffectEntity, EffectModel>,     EffectToEntityMapper>();

//Inject Repositories
builder.Services.AddScoped<IPlanetRepository,    PlanetRepository>();
builder.Services.AddScoped<IColonyRepository,    ColonyRepository>();
builder.Services.AddScoped<IColonistRepository,  ColonistRepository>();
builder.Services.AddScoped<IRewardRepository,    RewardRepository>();
builder.Services.AddScoped<IInventaryRepository, InventaryRepository>();
builder.Services.AddScoped<IMissionRepository,   MissionRepository>();
builder.Services.AddScoped<IEnemyRepository,     EnemyRepository>();
builder.Services.AddScoped<IItemRepository,      ItemRepository>();

//Inject Factories
builder.Services.AddScoped<IResultFactory<JsonResult, object>,  JsonResultFactory>();
builder.Services.AddScoped<IJsonContentFactory,                 JsonContentMissionFactory>();
builder.Services.AddScoped<IStrategyFactory,                    MissionRewardStrategyFactory>();
builder.Services.AddScoped<ColonistFactory>();
builder.Services.AddScoped<ColonyFactory>();
builder.Services.AddScoped<ColonyMemberFactory>();

//Inject Services
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddScoped<IMapDataService, MapDataService>();
builder.Services.AddScoped<IMissionExecutionService, MissionExecutionService>();

//Inject Command-Strategy Services
builder.Services.AddScoped<IMissionRewardStrategy, FullSuccessRewardStrategy>();
builder.Services.AddScoped<IMissionRewardStrategy, MoneyRewardStrategy>();
builder.Services.AddScoped<IMissionRewardStrategy, ResourceRewardStrategy>();
builder.Services.AddScoped<IMissionRewardStrategy, NoRewardStrategy>();
builder.Services.AddScoped<IDeletePicture, DeletePicture>();

//Inject Rate Limiting Middleware(anti-DoS)
builder.Services.AddFixedWindowRateLimiting();

builder.Services.AddDefaultIdentity<ColonistEntity>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StarColoniesDbContext>();

//========================== Application settings ==========================//
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

await SeedDataAsync(app);

app.Run();
return;

static async Task SeedDataAsync(WebApplication app)
{
    
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<StarColoniesDbContext>();
    
    await new SeedCommand().SeedAsync(context, app.Services);
}
