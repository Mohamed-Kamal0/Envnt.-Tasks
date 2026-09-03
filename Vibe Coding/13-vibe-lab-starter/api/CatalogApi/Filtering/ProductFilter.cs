using CatalogApi.Models;

namespace CatalogApi.Filtering;

public static class ProductFilter
{
    public static IEnumerable<Product> ByName(IEnumerable<Product> query, string? search)
    {
        if (string.IsNullOrWhiteSpace(search) || query is null)
            return query ?? Enumerable.Empty<Product>();

        return query.Where(p => p.Name.Contains(search, StringComparison.Ordinal));
    }
}