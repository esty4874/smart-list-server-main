namespace smartList;

public partial class SmartListContext : DbContext
{
    public SmartListContext()
    {
    }

    public SmartListContext(DbContextOptions<SmartListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetailsInShop> ProductDetailsInShops { get; set; }

    public virtual DbSet<ShopList> ShopLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=SmartList;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Img).HasColumnName("img");
            entity.Property(e => e.IsInPackage).HasDefaultValueSql("('False')");
            entity.Property(e => e.ProductName).HasMaxLength(20);
            entity.Property(e => e.WeightType).HasMaxLength(20);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.Company).WithMany(p => p.Products)
                .HasForeignKey(d => d.CompanyId)
                .IsRequired(false)
                .HasConstraintName("FK_Product_Company");
        });

        modelBuilder.Entity<ProductDetailsInShop>(entity =>
        {
            entity.ToTable("ProductDetailsInShop");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetailsInShops)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductDetailsInShopy_Product");

            entity.HasOne(d => d.ShopList).WithMany(p => p.ProductDetailsInShops)
                .HasForeignKey(d => d.ShopListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductDetailsInShopy_ShopList");
        });

        modelBuilder.Entity<ShopList>(entity =>
        {
            entity.ToTable("ShopList");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IsUsedSatistic).HasDefaultValueSql("('True')");

            entity.HasOne(d => d.User).WithMany(p => p.ShopLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopList_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
