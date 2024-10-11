namespace Catalog.API.Products.GetProducts;
internal sealed record GetProductsRequest(int PageNumber = 1, int PageSize = 10);
internal sealed record GetProductsResponse(CatalogPageList<Product> Products);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest getProductsRequest, ISender sender) =>
        {
            var query = getProductsRequest.Adapt<GetProductsQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
