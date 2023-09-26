public class Product
{
    public string Name { get; set; }
    public string Unit { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public DateTime ArrivalDate { get; set; }
    public Dictionary<string, string> Properties { get; set; }

    public Product(string name, string unit, int quantity, decimal pricePerUnit, DateTime arrivalDate)
    {
        Name = name;
        Unit = unit;
        Quantity = quantity;
        PricePerUnit = pricePerUnit;
        ArrivalDate = arrivalDate;
        Properties = new Dictionary<string, string>();
    }
}

public class ProductContainer<T>
{
    private LinkedList<T> products;

    public ProductContainer()
    {
        products = new LinkedList<T>();
    }

    public void Add(T item)
    {
        products.AddLast(item);
    }

    public IEnumerable<T> GetAll()
    {
        return products;
    }
}

public static class ProductUtility
{
    public static IEnumerable<T> SortByName<T>(ProductContainer<T> container)
        where T : Product
    {
        return container.GetAll().OrderBy(p => p.Name);
    }

    public static IEnumerable<T> SortByPrice<T>(ProductContainer<T> container)
        where T : Product
    {
        return container.GetAll().OrderBy(p => p.PricePerUnit);
    }

    public static IEnumerable<T> SortByArrivalDate<T>(ProductContainer<T> container)
        where T : Product
    {
        return container.GetAll().OrderBy(p => p.ArrivalDate);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var productContainer = new ProductContainer<Product>();

        productContainer.Add(new Product("Product A", "kg", 10, 5.99m, DateTime.Now));
        productContainer.Add(new Product("Product B", "piece", 50, 1.49m, DateTime.Now.AddDays(-5)));
        productContainer.Add(new Product("Product C", "liter", 20, 2.99m, DateTime.Now.AddDays(-2)));

        Console.WriteLine("Сортування за найменуванням:");
        foreach (var product in ProductUtility.SortByName(productContainer))
        {
            Console.WriteLine($"{product.Name}, {product.Quantity} {product.Unit}, ${product.PricePerUnit}");
        }

        Console.WriteLine("\nСортування за ціною:");
        foreach (var product in ProductUtility.SortByPrice(productContainer))
        {
            Console.WriteLine($"{product.Name}, {product.Quantity} {product.Unit}, ${product.PricePerUnit}");
        }

        Console.WriteLine("\nСортування за датою надходження:");
        foreach (var product in ProductUtility.SortByArrivalDate(productContainer))
        {
            Console.WriteLine($"{product.Name}, {product.Quantity} {product.Unit}, ${product.PricePerUnit}, {product.ArrivalDate.ToShortDateString()}");
        }
    }
}
