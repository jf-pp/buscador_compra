namespace BuscadorCompras.Models;

/// <summary>Producto seleccionado que el buscador conoce en la v0.1.0.</summary>
public sealed record Product(
    string Name,
    string Brand,
    string Category,
    decimal? PriceMxn,
    string? Store,
    string? Url);
