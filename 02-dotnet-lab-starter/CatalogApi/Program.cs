using CatalogApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Day 2: the DI container ─────────────────────────────────────────────────
// Ask for an ICatalogService anywhere (a controller constructor, say) and the container
// hands you a CatalogService — `new CatalogService()` appears NOWHERE in this codebase.
// Scoped = one instance per HTTP request; it's the lifetime a DbContext will need
// on day 4, so we start with it now.
builder.Services.AddScoped<ICatalogService, CatalogService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
