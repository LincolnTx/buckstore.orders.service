using buckstore.orders.service.domain.Aggregates.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace buckstore.orders.service.infrastructure.Data.Mappings.Database
{
	public class BuyerMap  : IEntityTypeConfiguration<Buyer>
	{
		public void Configure(EntityTypeBuilder<Buyer> builder)
		{
			builder.ToTable("buyer");

			builder.HasKey(buyer => buyer.Id);

			builder.Property(buyer => buyer.Cpf)
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasColumnName("cpf")
				.IsRequired();
		}
	}
}