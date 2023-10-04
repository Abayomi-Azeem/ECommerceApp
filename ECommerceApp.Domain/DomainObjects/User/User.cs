using ECommerceApp.Domain.DomainObjects.User.Entities;
using ECommerceApp.Domain.DomainObjects.User.ValueObjects;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Models;

namespace ECommerceApp.Domain.DomainObjects.User;

public  class User : AggregateRoot<UserId>
{
    private User(UserId id, CreateUserDto userDetails): base(id)
    {
        FirstName = userDetails.FirstName;
        LastName = userDetails.LastName;
        Email = userDetails.Email;
        Gender = userDetails.Gender;
        HomeAddress = userDetails.HomeAddress;
        Password = userDetails.Password;
        Wallet = Wallet.Create(id);
        Cart = Cart.Create(id);
    }
    public string FirstName {get; }

    public string LastName {get; }

    public string Email {get; }

    public string Password {get; }

    public string HomeAddress {get; }

    public string Gender {get; }

    public virtual Wallet Wallet {get; }

    public virtual Cart Cart {get; }


    public User Create(CreateUserDto userDetails)
    {
        return new (UserId.CreateUserId(), userDetails);
    }
    // public User Create(CreateUserDto userDetails)
    // {
    //     FirstName = userDetails.FirstName;
    //     LastName = userDetails.LastName;
    //     Email = userDetails.Email;
    //     Gender = userDetails.Gender;
    //     HomeAddress = userDetails.HomeAddress;
    //     Password = userDetails.Password;
    //     Id = Guid.NewGuid();
    //     Wallet = new Wallet().Create(Id);
    //     Cart = new Cart().Create(Id);
    //     return this;
    // }
}