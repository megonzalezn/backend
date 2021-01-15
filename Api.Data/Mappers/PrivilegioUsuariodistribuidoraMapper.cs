using Api.Entidad;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Mappers
{
    class PrivilegioUsuarioDistribuidoraMapper: EntityTypeConfiguration<PrivilegioUsuarioDistribuidora>
    {
        public PrivilegioUsuarioDistribuidoraMapper()
        {
            this.ToTable("PrivilegioUsuarioDistribuidora");

            this.HasKey(p => new { p.IdPrivilegio, p.IdUsuarioDistribuidora });

        }
    }
}
