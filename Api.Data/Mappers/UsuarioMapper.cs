using Api.Entidad;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Api.Data.Mappers
{
    class UsuarioMapper : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMapper()
        {
            this.ToTable("Usuario");

            this.HasKey(c => c.Id);
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
        }
    }
}
