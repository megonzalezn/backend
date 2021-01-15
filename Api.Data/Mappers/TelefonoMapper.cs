using Api.Entidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Mappers
{
    class TelefonoMapper: EntityTypeConfiguration<Telefono>
    {
        public TelefonoMapper()
        {
            this.ToTable("Telefono");

            this.HasKey(t => t.Id);
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Id).IsRequired();

            this.Property(t => t.Numero).HasMaxLength(15);
            this.Property(t => t.Numero).IsRequired();

            this.Property(t => t.TipoEntidad).IsRequired();

            this.Property(t => t.Eliminado).IsRequired();
        }
    }
}
