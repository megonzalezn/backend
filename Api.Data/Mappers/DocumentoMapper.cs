using Api.Entidad;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Api.Data.Mappers
{
    class DocumentoMapper : EntityTypeConfiguration<Documento>
    {
        public DocumentoMapper()
        {
            this.ToTable("Documento");

            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.IdUsuario).IsOptional();
            this.Property(c => c.IdDistribuidora).IsOptional();
        }
    }
}
