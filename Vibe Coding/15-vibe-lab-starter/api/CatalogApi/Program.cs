using CatalogApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog API — Vibe Coding bench", Version = "v1" });
});

// CORS. In Week 2 day 9 you hit this wall yourself and fixed it; here it is
// pre-wired so the bench runs on the first try. `ng serve` is on 4200.
builder.Services.AddCors(options =>
{
    options.AddPolicy("angular-dev", policy => policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("angular-dev");
app.MapControllers();

app.Run();
