namespace Domain;

public class Brand
{
    public string Name { get; set; } = "Default Name";

    public Uri BaseUrl { get; set; } = new Uri("");

    public Uri LogoUrl { get; set; } = new Uri("");

    public double MinimumFreeDeliveryPrice { get; set; } = -1;

    public List<Product> Products { get; set; } = [];
}