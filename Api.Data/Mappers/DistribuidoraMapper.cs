using Api.Entidad;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Api.Data.Mappers
{
    class DistribuidoraMapper : EntityTypeConfiguration<Distribuidora>
    {
        public DistribuidoraMapper()
        {
            this.ToTable("Distribuidora");

            this.HasKey(e => e.Id);
            this.Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(e => e.Id).IsRequired();

            this.Property(p => p.Identificador).HasMaxLength(20);
            this.Property(p => p.Identificador).IsRequired();

            this.Property(p => p.RazonSocial).HasMaxLength(100);
            this.Property(p => p.RazonSocial).IsRequired();

            this.Property(p => p.Giro).HasMaxLength(500);
            this.Property(p => p.Giro).IsRequired();

            this.Property(p => p.Direccion).HasMaxLength(100);
            this.Property(p => p.Direccion).IsRequired();

            this.Property(p => p.Mail).HasMaxLength(50);
            this.Property(p => p.Mail).IsRequired();

            this.Property(p => p.Eliminado).IsRequired();

        }
    }
}
