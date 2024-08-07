namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrderByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.Include(o => o.OrderItems).AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(query.Name)).OrderBy(x => x.OrderName).ToListAsync();
        return new GetOrderByNameResult(orders.ToOrderDtoList());
    }
}
