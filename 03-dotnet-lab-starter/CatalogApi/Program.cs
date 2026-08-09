using CatalogApi.Dtos;
using CatalogApi.Services;
using CatalogApi.Validation;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Day 2: services registered into the DI container (Scoped) ──────────────
builder.Services.AddScoped<ICatalogService, CatalogService>();

// ── Day 3: validation — the validator is itself resolved through DI ────────
builder.Services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();

builder.Services.AddControllers();

// ── Day 3: Swagger — reads the API's shape and serves live docs at /swagger ─
// Wired for you: read it, run it, try your endpoints from the browser.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
