using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using wise_api.Entities;

namespace wise_api.Configurations
{
    public class BackNamesHistoryConfigurations : IEntityTypeConfiguration<BackNamesHistory>
    {
        public void Configure(EntityTypeBuilder<BackNamesHistory> builder)
        {
            builder.HasKey(x => x.Id)
                 .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("BackNamesHistory");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdBackNamesHistory");

            builder.Property(x => x.Name)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(x => x.State)
                .IsRequired()
                .HasColumnType("tinyint");

            builder.Property(x => x.Hour)
                .IsRequired()
                .HasColumnType("varchar(100)");


        }
    }
}
