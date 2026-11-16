using BuscadorCompras.Models;

namespace BuscadorCompras.Services;

/// <summary>Busca productos dentro del catálogo seleccionado de la v0.1.0.</summary>
public interface IProductSearchService
{
    /// <summary>
    /// Devuelve los productos cuyo nombre, marca o categoría contienen
    /// todas las palabras de la consulta. Una consulta vacía no devuelve nada.
    /// </summary>
    IReadOnlyList<Product> Search(string? query);
}
