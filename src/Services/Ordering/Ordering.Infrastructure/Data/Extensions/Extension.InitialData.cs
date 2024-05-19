﻿namespace Ordering.Infrastructure.Data.Extensions;
public partial class Extension
{
    public static readonly List<Customer> GetCustomers = new List<Customer>
        {
            Customer.Create(new Guid("58c49479-ec65-4de2-86e7-033c546291aa"), "Mehmet", "mehmet@gmail.com"),
            Customer.Create(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"), "John", "john@gmail.com")
        };

    public static readonly List<Product> GetProducts = new List<Product>
        {
            Product.Create(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), "IPhone X", 500),
            Product.Create(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), "Samsung 10", 400),
            Product.Create(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"), "Huawei Plus", 650),
            Product.Create(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"), "Xiaomi Mi", 450)
        };

    public static IEnumerable<Order> GetOrders()
    {
        var address1Shipping = Address.Of("Mehmet", "Ozkaya", "mehmet@gmail.com", "Bahcelievler No:4", "Turkey", "Istanbul", "38050");
        var address1Billing = Address.Of("Mehmet", "Ozkaya", "mehmet@gmail.com", "Bahcelievler No:4", "Turkey", "Istanbul", "38050");

        var address2Shipping = Address.Of("John", "Doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");
        var address2Billing = Address.Of("John", "Doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

        var payment1 = Payment.Of("Mehmet", "5555555555554444", "12/28", "355", 1);
        var payment2 = Payment.Of("John", "8885555555554444", "06/30", "222", 2);

        var order1 = Order.Create(Guid.NewGuid(), new Guid("58c49479-ec65-4de2-86e7-033c546291aa"), "ORD_1",
                        shippingAddress: address1Shipping,
                        billingAddress: address1Billing,
                        payment1);
        order1.Add(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500);
        order1.Add(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400);

        var order2 = Order.Create(Guid.NewGuid(), new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"), "ORD_2",
                        shippingAddress: address2Shipping,
                        billingAddress: address2Billing,
                        payment2);
        order2.Add(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"), 1, 650);
        order2.Add(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"), 2, 450);

        return new List<Order> { order1, order2 };
    }

}
