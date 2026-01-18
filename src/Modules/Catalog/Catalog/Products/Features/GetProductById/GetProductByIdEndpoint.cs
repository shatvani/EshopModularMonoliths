namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdRequest(Guid ProductId);

public record GetProductByIdResponse(ProductDto? Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{productId}", async (Guid productId, ISender sender) =>
        {
            var query = new GetProductByIdQuery(productId);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
         .WithName("GetProductById")
         .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get product by id")
         .WithDescription("Get a product by its unique identifier");
    }
}
