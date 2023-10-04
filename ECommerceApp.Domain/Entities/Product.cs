using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Enums;

namespace ECommerceApp.Domain.Entities;

public class Product
{
    public Guid Id {get; set;}

    public string Name {get; set;}

    public decimal Price {get; set;}

    public string? Make {get; set;}

    public string? Model {get; set;}

    public float Rating {get; set;}

    public int NoOfRatings {get; set;}

    public string Type {get; set;}

    public int QuantityAvailable {get; set;}

    public DateTime LastDateRestocked  {get; set;}

    public DateTime DateAdded {get; set;}

    public byte[] Picture {get; set;}

    public Product Create(NewProductDto productDetails)
    {
        byte[] imageDetails;
            
        using (var stream = new MemoryStream())
            {
                productDetails.Image.CopyTo(stream);
                imageDetails = stream.ToArray();
            }

        Name = productDetails.Name;
        Price = productDetails.Price;
        Make = productDetails.Make;
        Model = productDetails.Model;
        QuantityAvailable = productDetails.QuantityAvailable;
        Type = productDetails.Type;
        LastDateRestocked = DateTime.Now;
        DateAdded = DateTime.Now;
        Id = Guid.NewGuid();
        Picture = imageDetails;
        return this;
    }

    public bool BuyProduct(Guid productId, int quantity)
    {
        if(productId== Id && QuantityAvailable > quantity)
        {
            QuantityAvailable -= quantity;
            return true;
        }
        return false;
    }

    public Product? GetProduct(Guid productId)
    {
        if(productId == Id)
        {
            return this;
        }
        return null;
    }

    public bool RateProduct(Guid productId, int rating)
    {
        if(productId == Id)
        {
            var previousSum = Rating * NoOfRatings;
            NoOfRatings += 1;
            var newAvg = (previousSum + rating)/ NoOfRatings;
            Rating = newAvg;
            return true;
        }
        return false;
    }
}