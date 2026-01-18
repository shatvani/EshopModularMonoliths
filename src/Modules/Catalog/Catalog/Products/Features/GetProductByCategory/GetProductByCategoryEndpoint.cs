namespace Catalog.Products.Features.GetProductByCategory;

public class GetProductByCategoryEndpoint : ICarterModule
{
    //public record GetProductByCategoryRequest(string Category);

    public record GetProductByCategoryResponse(ProductDto? Product);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(category));
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        })
         .WithName("GetProductByCategory")
         .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get product by category")
         .WithDescription("Get a product by its category");
    }
}
