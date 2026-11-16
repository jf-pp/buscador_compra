using System.Globalization;
using System.Text;
using BuscadorCompras.Models;

namespace BuscadorCompras.Services;

/// <summary>
/// Búsqueda sobre un catálogo en memoria de productos seleccionados.
/// En la v0.1.0 no hay scraping ni APIs de tiendas: el catálogo es fijo
/// y sirve para validar que buscar un producto funciona correctamente.
/// </summary>
public sealed class ProductSearchService : IProductSearchService
{
    // Precios verificados por Josué durante El Buen Fin 2026; son el
    // catálogo inicial, no un precio en vivo.
    private static readonly IReadOnlyList<Product> Catalog =
    [
        new("Xiaomi 2K Gaming Monitor G27Qi 2026", "Xiaomi", "Monitor", 3450m,
            "mi.com/mx", "https://www.mi.com/mx/product/xiaomi-2k-gaming-monitor-g27qi-2026/"),
        new("Gigabyte GS27QA", "Gigabyte", "Monitor", 2799m,
            "DDTECH", "https://ddtech.mx/"),
        new("LG UltraGear OLED 27GX704A-B", "LG", "Monitor", 7999.20m,
            "Liverpool", "https://www.liverpool.com.mx/"),
        new("MSI MAG 274QRFW X32", "MSI", "Monitor", 5189m,
            "Cyberpuerta", "https://www.cyberpuerta.mx/"),
    ];

    public IReadOnlyList<Product> Search(string? query)
    {
        var words = Normalize(query)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (words.Length == 0)
        {
            return [];
        }

        return Catalog
            .Where(p => words.All(w => Normalize($"{p.Name} {p.Brand} {p.Category}").Contains(w)))
            .OrderBy(p => p.Name, StringComparer.Ordinal)
            .ToList();
    }

    /// <summary>Minúsculas, sin acentos y sin espacios de más.</summary>
    internal static string Normalize(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        var decomposed = text.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var withoutAccents = new string(decomposed
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray());
        return withoutAccents.Normalize(NormalizationForm.FormC);
    }
}
