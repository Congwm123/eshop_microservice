namespace Ordering.Application.Orders.Queries.GetOrderByName;

public record GetOrderByNameQuery(string Name) : IQuery<GetOrderByNameResult>;
public class GetOrderByNameResult(IEnumerable<OrderDto> Orders);
