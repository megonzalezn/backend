using System;

namespace Api.Entidad
{
    public class FiltroUsuario
    {
        public FiltroUsuario()
        {
            Ids = new int[0];
        }
        public int Id { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool? Eliminado { get; set; }
        public bool? EsVerificado { get; set; }
        public string[] Includes { get; set; }
        public int[] Ids { get; set; }
    }
}
