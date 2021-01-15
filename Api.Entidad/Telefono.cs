using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Entidad
{
    public class Telefono
    {
        public int Id { get; set; }

        [ForeignKey("Distribuidora")]
        public int? IdDistribuidora { get; set; }
        public Distribuidora Distribuidora { get; set; }
        public int TipoEntidad { get; set; }
        public string Numero { get; set; }
        public bool Eliminado { get; set; }
    }
}
