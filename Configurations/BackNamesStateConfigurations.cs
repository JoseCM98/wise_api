using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using wise_api.Entities;

namespace wise_api.Configurations
{
    public class BackNamesStateConfigurations : IEntityTypeConfiguration<BackNamesState>
    {
        public void Configure(EntityTypeBuilder<BackNamesState> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("backnamesstate");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdBackNamesState");

            builder.Property(x => x.Name)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(x => x.State)
              .IsRequired()
              .HasColumnType("tinyint");


        }
    }
}
