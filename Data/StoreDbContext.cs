using Microsoft.EntityFrameworkCore;
using Storee.Entities;
using System;
using System.Linq;

namespace Storee.Data
{
	public class StoreDbContext : DbContext
	{
		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
		{

		}

		public DbSet<Country> Country { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<ProductPrice> ProductPrice { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }
		public DbSet<Payment> Payment { get; set; }
		public DbSet<ShoppingCart> ShoppingCart { get; set; }
		public DbSet<CartItem> CartItem { get; set; }
		public DbSet<Home>Home { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.Property(p => p.Sizes)
				.HasConversion(
					v => string.Join(',', v),
					v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

			modelBuilder.Entity<Product>()
				.Property(p => p.Colors)
				.HasConversion(
					v => string.Join(',', v),
					v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

			modelBuilder.Entity<Product>()
			.Property(e => e.ImageUrls)
			.HasConversion(
				v => string.Join(';', v),
				v => v.Split(';', StringSplitOptions.RemoveEmptyEntries));

			
		}
	}
}
