namespace Catalog.Data.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(200);
        builder.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(e => e.ImageFile).HasMaxLength(100);
        builder.Property(e => e.Category).IsRequired();
    }
}
