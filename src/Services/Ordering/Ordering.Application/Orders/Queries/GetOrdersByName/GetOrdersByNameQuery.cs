namespace Ordering.Application.Orders.Queries.GetOrdersByName;
public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;

public record GetOrdersResult(PagedList<OrderDto> Orders);