namespace ECommerceApp.Contracts.Products;

public class ListProduct
{
    public Guid Id {get; set;}
    public string Name {get; set;}

    public string Image {get; set;}

    public int QuantityAvailable {get; set;}

    public float Rating {get; set;}

    public int NoOfRatings {get; set;}

    public string Make {get; set;}

    public string Model {get; set;}

    public decimal Price {get; set;}

    public string Type {get; set;}
    
}

