namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(CustomerId.Of(new Guid("bc9e8c80-142b-4866-b60e-0ac0169cac10")), "John", "john@gmail.com"),
            Customer.Create(CustomerId.Of(new Guid("b903bda2-f94c-44f5-a390-f5c233dcd123")), "David", "david@gmail.com")
        };

    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("18210c42-95fc-49e8-b8df-34d9e3c67399")), "IphoneX", 2000),
            Product.Create(ProductId.Of(new Guid("25c639de-5700-4196-81e5-2f28bea102f1")), "Samsung", 1000),
            Product.Create(ProductId.Of(new Guid("1cc60828-33bb-4a9a-aa5c-939de1f4122c")), "Huawei Plus", 500),
            Product.Create(ProductId.Of(new Guid("68d187ee-a717-42e4-b5f9-179d28686e66")), "Xiaomi Mi", 450)
        };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("John", "Doe", "john.doe@example.com", "123 Main St", "USA", "CA", "90210");
            var address2 = Address.Of("Jane", "Smith", "jane.smith@example.com", "456 Elm St", "USA", "NY", "10001");
            var payment1 = Payment.Of("John Doe", "4111111111111111", "12/25", "123", 1);
            var payment2 = Payment.Of("Jane Smith", "5500000000000004", "01/26", "456", 2);

            var order1 = Order.Create(OrderId.Of(Guid.NewGuid()),
                                    CustomerId.Of(new Guid("bc9e8c80-142b-4866-b60e-0ac0169cac10")),
                                    OrderName.Of("ORD_1"),
                                    address1, address2, payment1);

            order1.Add(ProductId.Of(new Guid("18210c42-95fc-49e8-b8df-34d9e3c67399")), 2, 2000);
            order1.Add(ProductId.Of(new Guid("25c639de-5700-4196-81e5-2f28bea102f1")), 4, 1000);

            var order2 = Order.Create(OrderId.Of(Guid.NewGuid()),
                                    CustomerId.Of(new Guid("b903bda2-f94c-44f5-a390-f5c233dcd123")),
                                    OrderName.Of("ORD_2"),
                                    address1, address2, payment2);

            order2.Add(ProductId.Of(new Guid("1cc60828-33bb-4a9a-aa5c-939de1f4122c")), 1, 500);
            order2.Add(ProductId.Of(new Guid("68d187ee-a717-42e4-b5f9-179d28686e66")), 4, 450);

            return new List<Order> { order1, order2 };
        }
    }

}
