
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrderHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrderResult>
{
    public async Task<GetOrderResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.pageIndex;
        var pageSize = query.PaginationRequest.pageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
        var orders = await dbContext.Orders.Include(x => x.OrderItems).OrderBy(x => x.OrderName.Value).Skip(pageSize * pageIndex).Take(pageSize).ToListAsync(cancellationToken);

        return new GetOrderResult(new PaginationResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
    }
}
