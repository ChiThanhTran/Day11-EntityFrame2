using ENTITY1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENTITY1.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>(e => e.ToTable("Categories"));
        
        builder.Entity<Category>().HasMany(category => category.Products)
                                  .WithOne(product =>product.Category )
                                  .HasForeignKey(product => product.CategoryId)
                                  .IsRequired();
        
        var data = new List<Category>
        {
            new Category{Id = 1, Name ="CocaCola"},
            new Category{Id = 2, Name ="Pepsi"},
            new Category{Id = 3, Name ="Sting"},
            new Category{Id = 4, Name ="Fanta"},
            new Category{Id = 5, Name ="7Up"}
        };
        builder.Entity<Category>().HasData(data);
        builder.Entity<Product>(e => e.ToTable("Products"));
    }

    public virtual DbSet<Student>? Students { get; set; }
    public virtual DbSet<Category>? Categories { get; set; }
    public virtual DbSet<Product>? Products { get; set; }
}