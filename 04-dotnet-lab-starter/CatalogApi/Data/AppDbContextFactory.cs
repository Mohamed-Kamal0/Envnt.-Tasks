using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CatalogApi.Data;

// Used ONLY by the `dotnet ef` command-line tools at design time (e.g. when you run
// `dotnet ef migrations add`). It lets the tools build an AppDbContext without starting
// the whole web app — which is why creating a migration doesn't run Program.cs.
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=catalog.db")
            .Options;
        return new AppDbContext(options);
    }
}
