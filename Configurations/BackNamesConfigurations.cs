using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using wise_api.Entities;

namespace wise_api.Configurations
{
    public class BackNamesConfigurations : IEntityTypeConfiguration<BackNames>
    {
        public void Configure(EntityTypeBuilder<BackNames> builder)
        {
            builder.HasKey(x => x.Id)
                 .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("BackNames");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdBackNames");

            builder.Property(x => x.Name)
               .IsRequired()
               .HasColumnType("varchar(100)");

          

        }
    }
}
