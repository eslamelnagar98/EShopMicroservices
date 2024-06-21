namespace Ordering.Application.Orders.Queries.GetOrdersByName;
public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageNumber = query.PaginationRequest.PageIndex;

        var pageSize = query.PaginationRequest.PageSize;

        var orders = await dbContext.Orders
                       .Include(o => o.OrderItems)
                       .OrderBy(o => o.OrderName.Value)
                       .ToPageListAsync(pageNumber, pageSize, cancellationToken);

        var orderDtoPageList = orders.ToOrderDtoPageList();

        return new GetOrdersResult(orderDtoPageList);

    }
}