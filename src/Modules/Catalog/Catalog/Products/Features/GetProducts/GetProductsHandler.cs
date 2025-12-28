namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<ProductDto> Products);

public class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var productDtos = await dbContext.Products
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Categories,
                product.Description,
                product.Price,
                product.ImageFile))
            .ToListAsync(cancellationToken);

        // Using Mapster for mapping (if needed in future)
        // var productDtos = await dbContext.Products
        //     .AsNoTracking()
        //     .OrderBy(product => product.Name)
        //     .ProjectToType<ProductDto>()
        //     .ToListAsync(cancellationToken); 
        // OR
        // var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductsResult(productDtos);
    }
}