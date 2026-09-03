using CatalogApi.Data;
using CatalogApi.Dtos;
using CatalogApi.Services;
using CatalogApi.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Day 4: EF Core + SQLite ────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// ── Day 2/5: services registered into the DI container (Scoped) ────────────
builder.Services.AddScoped<ICatalogService, CatalogService>();

// ── Day 3: validation ──────────────────────────────────────────────────────
builder.Services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();

builder.Services.AddControllers();

// ── Day 3: Swagger ─────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });
});

var app = builder.Build();

// Create/upgrade the database and seed demo data on startup, so the API just works.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DbSeeder.Seed(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
