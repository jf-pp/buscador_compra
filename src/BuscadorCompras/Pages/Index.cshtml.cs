using BuscadorCompras.Models;
using BuscadorCompras.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuscadorCompras.Pages;

public class IndexModel : PageModel
{
    private readonly IProductSearchService _search;

    public IndexModel(IProductSearchService search)
    {
        _search = search;
    }

    public string? Query { get; private set; }

    public bool Searched { get; private set; }

    public IReadOnlyList<Product> Results { get; private set; } = [];

    public void OnGet(string? q)
    {
        Query = q;
        Searched = !string.IsNullOrWhiteSpace(q);
        Results = Searched ? _search.Search(q) : [];
    }
}
