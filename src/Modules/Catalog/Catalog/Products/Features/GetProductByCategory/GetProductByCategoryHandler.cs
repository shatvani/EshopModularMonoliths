namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);

public class GetProductByCategoryHandler(CatalogDbContext dbContext) : 
    IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var productDtos = await dbContext.Products
            .AsNoTracking()
            .Where(product => product.Categories.Contains(query.Category))
            .OrderBy(product => product.Name)
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Categories,
                product.Description,
                product.Price,
                product.ImageFile))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(productDtos);
    }
}