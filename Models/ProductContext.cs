using Microsoft.EntityFrameworkCore;

namespace Store_GB.Models
{
    public class ProductContext:DbContext
    {
        public DbSet<Storage> Storages { get; set;}
        public DbSet<Product> Products { get; set;}
        public DbSet<Category> Categories { get; set;}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        // optionsBuilder.UseNpgsql("Server=host.docker.internal;Database=web_store;Username=postgres;Password=root");
        //}
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public ProductContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(x => x.Id).HasName("ProductID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();
                 
                entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Cost)
                .HasColumnName("Cost")
                .IsRequired();


                entity.HasOne(x => x.Category)
                .WithMany(c => c.Products).HasForeignKey(x => x.Id).HasConstraintName("CategoryToProduct");
            });

            modelBuilder.Entity<Category>(entity =>
            { 
                entity.ToTable("Category");
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storage");
                entity.HasKey(x => x.Id).HasName("StorageID");

                entity.Property(x => x.Name)
                .HasColumnName("StorageName");

                entity.Property(e => e.Count)
                .HasColumnName("ProductCount");

                entity.HasMany(x => x.Products)
                .WithMany(m => m.Storages)
                .UsingEntity(j=>j.ToTable("StorageProduct"));
            });
        }
    }
}
