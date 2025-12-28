namespace Catalog.Products.Dtos;

public record ProductDto(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    decimal Price,
    string ImageFile);
