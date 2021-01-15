using Api.Entidad;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Api.Data.Mappers
{
    class PrivilegioMapper:EntityTypeConfiguration<Privilegio>
    {
        public PrivilegioMapper()
        {
            this.ToTable("Privilegio");

            this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
        
    }
}
