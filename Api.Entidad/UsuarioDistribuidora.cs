using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entidad
{
    public class UsuarioDistribuidora : BasePersistente
    {
        public int Id { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("Distribuidora")]
        public int IdDistribuidora { get; set; }
        public Distribuidora Distribuidora { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Alias { get; set; }
        public bool Eliminado { get; set; }
        public bool AceptaDistribuidora { get; set; }
        public bool AceptaUsuario { get; set; }
        public bool EsAdmin { get; set; }
        public DateTime? FechaAceptacion { get; set; }

        public List<PrivilegioUsuarioDistribuidora> Privilegios { get; set; }
        [NotMapped]
        public Entidad.Enums.ProcesoUsuarioDistribuidora Proceso { get; set; }
    }
}
