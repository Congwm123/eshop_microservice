﻿
namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => new OrderDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: new AddressDto(
                    order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.Country,
                    order.BillingAddress.State,
                    order.BillingAddress.ZipCode
                    ),
                BillingAddress: new AddressDto(
                    order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.Country,
                    order.BillingAddress.State,
                    order.BillingAddress.ZipCode
                    ),
                Payment: new PaymentDto(
                    order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    order.Payment.PaymentMethod
                    ),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList()
            ));
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(
            order.Id.Value, 
            order.CustomerId.Value, 
            order.OrderName.Value,
            new AddressDto(order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.Country,
                    order.BillingAddress.State,
                    order.BillingAddress.ZipCode),
            new AddressDto(order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.Country,
                    order.BillingAddress.State,
                    order.BillingAddress.ZipCode),
            new PaymentDto(order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    order.Payment.PaymentMethod),
            order.Status,
            order.OrderItems.Select(x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList());
    }
}
