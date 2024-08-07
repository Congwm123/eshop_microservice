namespace Ordering.Application.Orders.Queries.GetOrderByCustomerQuery;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;
public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
