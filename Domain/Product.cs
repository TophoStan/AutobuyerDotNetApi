namespace Domain;

public class Product
{
    public string Name { get; set; } = "Default product name";

    public Uri Url { get; set; } = new Uri("");
    
    public string Description { get; set; } = "Default product description";

    public decimal Price { get; set; } = -1;
    
    public List<Option> Options {get; set;} = [];

    public List<OrderStep> OrderSteps { get; set; } = [];
}