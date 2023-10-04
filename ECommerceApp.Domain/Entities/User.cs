using System.ComponentModel.DataAnnotations;
using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Dtos;

namespace ECommerceApp.Domain.Entities;

public class User
{
    [Key]
    public Guid Id {get; set;}

    public string FirstName {get; set;}

    public string LastName {get; set;}

    public string Email {get; set;}

    public string Password {get; set;}

    public string HomeAddress {get; set;}

    public string Gender {get; set;}

      
    public virtual Wallet Wallet {get; set;}

    public virtual Cart Cart {get; set;}

    public User Create(CreateUserDto userDetails)
    {
        FirstName = userDetails.FirstName;
        LastName = userDetails.LastName;
        Email = userDetails.Email;
        Gender = userDetails.Gender;
        HomeAddress = userDetails.HomeAddress;
        Password = userDetails.Password;
        Id = Guid.NewGuid();
        Wallet = new Wallet().Create(Id);
        Cart = new Cart().Create(Id);
        return this;
    }
}