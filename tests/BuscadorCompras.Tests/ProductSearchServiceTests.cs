using BuscadorCompras.Services;

namespace BuscadorCompras.Tests;

public sealed class ProductSearchServiceTests
{
    private readonly ProductSearchService _search = new();

    [Fact]
    public void Search_EncuentraPorNombreParcial()
    {
        var results = _search.Search("G27Qi");

        Assert.Single(results);
        Assert.Contains("G27Qi", results[0].Name);
    }

    [Fact]
    public void Search_IgnoraMayusculasYAcentos()
    {
        var results = _search.Search("XIAOMI g27qi");

        Assert.Single(results);
        Assert.Equal("Xiaomi", results[0].Brand);
    }

    [Fact]
    public void Search_TodasLasPalabrasDebenCoincidir()
    {
        // "MSI monitor" sí coincide; "MSI Xiaomi" no, porque ningún
        // producto es de ambas marcas a la vez.
        Assert.Single(_search.Search("MSI monitor"));
        Assert.Empty(_search.Search("MSI Xiaomi"));
    }

    [Fact]
    public void Search_BuscaPorCategoria()
    {
        Assert.Equal(4, _search.Search("monitor").Count);
    }

    [Fact]
    public void Search_ConsultaVacia_NoDevuelveNada()
    {
        Assert.Empty(_search.Search(null));
        Assert.Empty(_search.Search(""));
        Assert.Empty(_search.Search("   "));
    }

    [Fact]
    public void Search_SinCoincidencias_DevuelveVacio()
    {
        Assert.Empty(_search.Search("teclado mecánico"));
    }
}
