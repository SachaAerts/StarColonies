using Microsoft.EntityFrameworkCore;
using StarColonies.Web.Middlewares;
using StarColonies.Infrastructures.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ReverseProxyLinksMiddleware>();
builder.Services.AddDbContext<StarColoniesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString
        ("StarColoniesContext"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions());
app.UseReverseProxyLinks();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();