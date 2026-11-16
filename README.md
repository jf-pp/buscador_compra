# Buscador de compras

## Problema

Cuando Josué quiere comprar un producto (por ejemplo, un monitor gaming),
tiene que revisar tienda por tienda — Mercado Libre, Amazon México, mi.com,
Cyberpuerta, Liverpool — y comparar precios a mano, sin saber si una
"oferta" es real o un descuento inflado. No hay un solo lugar donde buscar
un producto y ver sus mejores ofertas juntas.

## Alcance (v0.1.0)

- Aplicación web en ASP.NET Core (Razor Pages) + C# / .NET 9.
- Buscar **un producto** por nombre, marca o categoría y ver sus resultados
  con precio en MXN y tienda.
- La búsqueda no distingue mayúsculas/minúsculas ni acentos, y todas las
  palabras de la consulta deben coincidir.
- Catálogo inicial en memoria con 4 monitores seleccionados (Xiaomi G27Qi
  2026, Gigabyte GS27QA, LG UltraGear OLED 27GX704A-B y MSI MAG 274QRFW
  X32), con los precios verificados durante El Buen Fin 2026.
- Pruebas unitarias con xUnit para el servicio de búsqueda.

## Qué queda fuera (por ahora)

- Scraping o APIs de tiendas: los precios del catálogo son fijos y no se
  actualizan solos.
- Historial de precios y detector de descuentos falsos.
- Comparación entre varios productos a la vez.
- Cuentas de usuario, apartados y pedidos.
- Despliegue público (GitHub Pages, hosting): la app corre en local.

## Estructura

```text
BuscadorCompras.sln
src/BuscadorCompras/        # App web (Razor Pages)
  Models/Product.cs         # Producto del catálogo
  Services/                 # IProductSearchService + ProductSearchService
  Pages/Index.cshtml        # Pantalla de búsqueda
tests/BuscadorCompras.Tests/ # Pruebas xUnit
```

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
  (probado con 9.0.321)
- Git

## Cómo instalar y ejecutar

```bash
git clone https://github.com/jf-pp/buscador_compra.git
cd buscador_compra
dotnet restore
dotnet run --project src/BuscadorCompras
```

Abre http://localhost:5000 (o la URL que muestre la terminal) y busca,
por ejemplo, `G27Qi` o `monitor`.

## Pruebas

```bash
dotnet test
```

> Nota: en la VM de trabajo de Muse, `dotnet test` no puede arrancar el
> testhost de vstest: las conexiones TCP de loopback de .NET son
> interceptadas por el proxy de salida del entorno, así que el runner
> agota el tiempo de espera. Las pruebas sí corren con el runner de
> consola de xUnit (ya referenciado en el proyecto de pruebas):
>
> ```bash
> DOTNET_ROLL_FORWARD=LatestMajor dotnet exec \
>   ~/.nuget/packages/xunit.runner.console/2.9.2/tools/netcoreapp1.0/xunit.console.dll \
>   tests/BuscadorCompras.Tests/bin/Debug/net9.0/BuscadorCompras.Tests.dll
> ```
>
> Última corrida: 6/6 en verde (0.095 s).

## Formato de código

El proyecto usa `.editorconfig` y `nullable` activado. Para formatear:

```bash
dotnet format
```
