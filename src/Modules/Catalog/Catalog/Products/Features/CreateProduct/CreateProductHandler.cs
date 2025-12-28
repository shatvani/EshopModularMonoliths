namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductHandler(CatalogDbContext dbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command.Product);

        dbContext.Products.Add(product);
        return await dbContext.SaveChangesAsync(cancellationToken)
            .ContinueWith(_ => new CreateProductResult(product.Id), cancellationToken);
    }

    private static Product CreateNewProduct(ProductDto productDto)
    {
        return Product.Create
        (
            Guid.NewGuid(),
            productDto.Name,
            productDto.Categories,
            productDto.Description, 
            productDto.ImageFile,
            productDto.Price
        );
    }
}

