using System;

namespace Api.Entidad
{
    public class FiltroUsuarioDistribuidora
    {
        public FiltroUsuarioDistribuidora()
        {
            Ids = new int[0];
            IdsUsuario = new int[0];
            IdsDistribuidora = new int[0];
        }
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdDistribuidora { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string Alias { get; set; }
        public bool? AceptaDistribuidora { get; set; }
        public bool? AceptaUsuario { get; set; }
        public bool? Eliminado { get; set; }
        public bool? EsAdmin { get; set; }
        public string[] Includes { get; set; }
        public int[] Ids { get; set; }
        public int[] IdsUsuario { get; set; }
        public int[] IdsDistribuidora { get; set; }
    }
}
