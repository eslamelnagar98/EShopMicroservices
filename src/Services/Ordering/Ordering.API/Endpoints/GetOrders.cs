﻿namespace Ordering.API.Endpoints;

public record GetOrdersResponse(PagedList<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));

               var response = result.MapToOrdersResponse();

                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
    }

    
}
public static class OrderExtension
{
    public static GetOrdersResponse MapToOrdersResponse(this GetOrdersResult getOrdersResult)
    {
        return new GetOrdersResponse(getOrdersResult.Orders);
    }
}
