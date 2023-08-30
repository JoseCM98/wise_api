using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using wise_api.Entities;

namespace wise_api.Configurations
{
    public class LineaConfigurations : IEntityTypeConfiguration<Linea>
    {
        public void Configure(EntityTypeBuilder<Linea> builder)
        {
            builder.HasKey(x => x.CodigoLinea)
                 .HasName("Primary");

            builder.Property(x => x.CodigoLinea)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("lineas");

            builder.HasIndex(x => x.CodigoLinea)
                .HasDatabaseName("FK_IndexIdPet");

            builder.Property(x => x.IdEmpresa)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(x => x.NombreLinea)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.AbreviadoLinea)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.PresupuestoLinea)
                .HasColumnType("varchar(100)");

            builder.Property(x => x.ActivaLinea)
                .HasColumnType("TEXT");

            builder
                .Property(x => x.VentaLinea)
                .IsRequired()
                .HasColumnType("varchar(36)");
            builder
                .Property(x => x.VentaLinea)
                .IsRequired()
                .HasColumnType("varchar(36)");
            builder
                .Property(x => x.IdEAN13)
                .IsRequired()
                .HasColumnType("varchar(36)");
            builder
                .Property(x => x.UsuariosLinea)
                .IsRequired()
                .HasColumnType("varchar(36)");


        }
    }
}
