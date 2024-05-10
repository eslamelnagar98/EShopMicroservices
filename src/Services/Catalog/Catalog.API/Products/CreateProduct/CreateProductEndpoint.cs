namespace Catalog.API.Products.CreateProduct;
internal sealed record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
internal sealed record CreateProductResponse(Guid Id);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<GetProductQuery>();
            var result = await sender.Send(command, cancellationToken);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}