using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repository;

public class ECommerceAppDbContext : DbContext
{
    public ECommerceAppDbContext(DbContextOptions<ECommerceAppDbContext> options): base(options)
    {
        
    }

    public DbSet<User> Users {get; set;}
    public DbSet<Product> Products {get; set;}
    public DbSet<Wallet> Wallets {get; set;}
    public DbSet<Order> Orders {get; set;}
    public DbSet<Cart> Carts {get; set;}
    public DbSet<ProductQuantity> ProductQuantities {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Wallet>()
            .Property(p => p.Balance)
            .HasColumnType("decimal(18,4)");
        modelBuilder.Entity<Cart>()
            .Property(p => p.TotalPrice)
            .HasColumnType("decimal(18,4)");
            modelBuilder.Entity<Order>()
            .Property(p => p.AmountPaid)
            .HasColumnType("decimal(18,4)");
        modelBuilder.Entity<Order>()
            .Property(p => p.Discount)
            .HasColumnType("decimal(18,4)"); 
        modelBuilder.Entity<Order>()
            .Property(p => p.TotalPrice)
            .HasColumnType("decimal(18,4)");      
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,4)");      
    }
}