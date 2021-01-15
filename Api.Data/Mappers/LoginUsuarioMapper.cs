using Api.Entidad;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Api.Data.Mappers
{
    class LoginUsuarioMapper : EntityTypeConfiguration<LoginUsuario>
    {
        public LoginUsuarioMapper() 
        {
            this.ToTable("LoginUsuario");
            this.HasKey(c => c.IdUsuario);
            this.Property(c => c.IdUsuario).IsRequired();
        }
    }
}
