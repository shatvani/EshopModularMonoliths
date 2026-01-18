namespace Catalog.Products.Features.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public record GetProductsResponse(IEnumerable<ProductDto> Products);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get products")
        .WithDescription("Get all the products");
    }
}
