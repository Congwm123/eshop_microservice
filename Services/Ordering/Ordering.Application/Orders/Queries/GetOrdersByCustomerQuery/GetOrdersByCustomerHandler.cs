namespace Ordering.Application.Orders.Queries.GetOrderByCustomerQuery;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.Include(o => o.OrderItems).AsNoTracking()
            .Where(x => x.CustomerId == CustomerId.Of(query.CustomerId)).OrderBy(x => x.OrderName).ToListAsync();
        return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
    }
}
