using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using wise_api.Entities;

namespace wise_api.Configurations
{
    public class BackGroundTimeConfigurations : IEntityTypeConfiguration<BackGroundTime>
    {
        public void Configure(EntityTypeBuilder<BackGroundTime> builder)
        {
            builder.HasKey(x => x.Id)
                 .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("BackGroundTime");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdBackGroundTime");

            builder.Property(x => x.Hora)
               .IsRequired()
               .HasColumnType("varchar(255)");

            builder.Property(x => x.Iteracion)
                .IsRequired()
                .HasColumnType("varchar(100)");


        }
    }
}
