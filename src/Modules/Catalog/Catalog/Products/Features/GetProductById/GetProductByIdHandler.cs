namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(ProductDto? Product);

public class GetProductByIdHandler(CatalogDbContext dbContext)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var productDto = await dbContext.Products
            .AsNoTracking()
            .Where(product => product.Id == query.ProductId)
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Categories,
                product.Description,
                product.Price,
                product.ImageFile))
            .SingleOrDefaultAsync(cancellationToken);

        // If you want to throw an exception when not found, uncomment below
        /*
        if (productDto is null)
        {
            throw new Exception($"Product with id {query.ProductId} not found.");
        }
        */

        return new GetProductByIdResult(productDto);
    }
}