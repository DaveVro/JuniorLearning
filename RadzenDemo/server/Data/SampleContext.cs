using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using RadzenDemo.Models.Sample;

namespace RadzenDemo.Data
{
  public partial class SampleContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public SampleContext(DbContextOptions<SampleContext> options):base(options)
    {
    }

    public SampleContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RadzenDemo.Models.Sample.OrderDetail>()
              .HasOne(i => i.Order)
              .WithMany(i => i.OrderDetails)
              .HasForeignKey(i => i.OrderId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<RadzenDemo.Models.Sample.OrderDetail>()
              .HasOne(i => i.Product)
              .WithMany(i => i.OrderDetails)
              .HasForeignKey(i => i.ProductId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<RadzenDemo.Models.Sample.Order>()
              .Property(p => p.OrderDate)
              .HasColumnType("date");

        builder.Entity<RadzenDemo.Models.Sample.Order>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<RadzenDemo.Models.Sample.OrderDetail>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<RadzenDemo.Models.Sample.OrderDetail>()
              .Property(p => p.Quantity)
              .HasPrecision(10, 0);

        builder.Entity<RadzenDemo.Models.Sample.OrderDetail>()
              .Property(p => p.OrderId)
              .HasPrecision(10, 0);

        builder.Entity<RadzenDemo.Models.Sample.OrderDetail>()
              .Property(p => p.ProductId)
              .HasPrecision(10, 0);

        builder.Entity<RadzenDemo.Models.Sample.Product>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<RadzenDemo.Models.Sample.Product>()
              .Property(p => p.ProductPrice)
              .HasPrecision(19, 4);
        this.OnModelBuilding(builder);
    }


    public DbSet<RadzenDemo.Models.Sample.Order> Orders
    {
      get;
      set;
    }

    public DbSet<RadzenDemo.Models.Sample.OrderDetail> OrderDetails
    {
      get;
      set;
    }

    public DbSet<RadzenDemo.Models.Sample.Product> Products
    {
      get;
      set;
    }
  }
}
