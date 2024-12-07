using System;
using System.Collections.Generic;

// address class
class Address
{
    private string StreetAddress;
    private string City;
    private string StateOrProvince;
    private string Country;

    // constructor
    public Address(string streetAddress, string city, string stateOrProvince, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        StateOrProvince = stateOrProvince;
        Country = country;
    }

    // method to check if the address is in USA
    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    // method to return the address as a formatted string
    public string GetFullAddress()
    {
        return $"{StreetAddress}\n{City}, {StateOrProvince}\n{Country}";
    }
}

// customer class
class Customer
{
    private string Name;
    private Address Address;

    // constructor
    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    // method to check if the customer lives in USA
    public bool LivesInUSA()
    {
        return Address.IsInUSA();
    }

    // method to get the customer's shipping address
    public string GetShippingLabel()
    {
        return $"{Name}\n{Address.GetFullAddress()}";
    }
}

// product class
class Product
{
    private string Name;
    private string ProductID;
    private decimal PricePerUnit;
    private int Quantity;

    // constructor
    public Product(string name, string productID, decimal pricePerUnit, int quantity)
    {
        Name = name;
        ProductID = productID;
        PricePerUnit = pricePerUnit;
        Quantity = quantity;
    }

    // method to calculate the total cost of the product
    public decimal GetTotalCost()
    {
        return PricePerUnit * Quantity;
    }

    // method to get the packing label for the product
    public string GetPackingLabel()
    {
        return $"{Name} (ID: {ProductID})";
    }
}

// order class
class Order
{
    private List<Product> Products;
    private Customer Customer;

    // constructor
    public Order(Customer customer)
    {
        Products = new List<Product>();
        Customer = customer;
    }

    // method to add a product to the order
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    // method to calculate the total cost of the order
    public decimal GetTotalCost()
    {
        decimal total = 0;

        foreach (var product in Products)
        {
            total += product.GetTotalCost();
        }

        // Add shipping cost
        total += Customer.LivesInUSA() ? 5 : 35;

        return total;
    }

    // method to generate the packing label
    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";

        foreach (var product in Products)
        {
            label += $"- {product.GetPackingLabel()}\n";
        }

        return label;
    }

    // method to generate the shipping label
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{Customer.GetShippingLabel()}";
    }
}

// main program
class Program
{
    static void Main(string[] args)
    {
        // create addresses
        var address1 = new Address("123 Elm St", "Springfield", "IL", "USA");
        var address2 = new Address("456 Oak Ave", "Toronto", "ON", "Canada");

        // create customers
        var customer1 = new Customer("John Doe", address1);
        var customer2 = new Customer("Jane Smith", address2);

        // create products
        var product1 = new Product("Widget", "W123", 10.99m, 3);
        var product2 = new Product("Gadget", "G456", 15.49m, 2);
        var product3 = new Product("Thingamajig", "T789", 7.99m, 5);

        var product4 = new Product("Doodad", "D321", 20.00m, 1);
        var product5 = new Product("Whatchamacallit", "W654", 30.00m, 2);

        // create orders
        var order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        var order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);

        // display order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():0.00}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():0.00}\n");
    }
}