using buckstore.orders.service.domain.Aggregates.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.orders.service.infrastructure.Data.Mappings.Database
{
	public class BuyerMap  : IEntityTypeConfiguration<Buyer>
	{
		public void Configure(EntityTypeBuilder<Buyer> builder)
		{
			builder.ToTable("buyers");

			builder.HasKey(buyer => buyer.Id);

			builder.Property(buyer => buyer.Cpf)
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("Cpf")
				.IsRequired();
			
			builder.Property(buyer => buyer.Name)
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("Name")
				.IsRequired();

			builder.HasMany(b => b.PaymentMethods)
				.WithOne()
				.HasForeignKey("BuyerId")
				.OnDelete(DeleteBehavior.Cascade);

			var navigation = builder.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));
			
			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

		}
	}
}