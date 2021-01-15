using Api.Entidad;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Api.Data.Mappers
{
    class UsuarioDistribuidoraMapper : EntityTypeConfiguration<UsuarioDistribuidora>
    {
        public UsuarioDistribuidoraMapper()
        {
            this.ToTable("UsuarioDistribuidora");

            this.HasKey(u => u.Id);
            this.Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(u => u.Id).IsRequired();

            this.Property(u => u.IdUsuario).IsRequired();
            this.Property(u => u.IdDistribuidora).IsRequired();
            
            this.Property(u => u.Alias).HasMaxLength(50);

            this.Property(u => u.Eliminado).IsRequired();
        }
    }
}
