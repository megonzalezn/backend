using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entidad
{
    public class Usuario : BasePersistente
    {
        public int Id { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Pin { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool DebeCambiarClave { get; set; }
        public string PushToken { get; set; }
        public string RefreshToken { get; set; }
        public bool Eliminado { get; set; }
        public bool EsVerificado { get; set; }

        public ICollection<UsuarioDistribuidora> UsuarioDistribuidora { get; set; }
        public ICollection<Documento> Documentos { get; set; }
        public virtual LoginUsuario LoginUsuario { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
