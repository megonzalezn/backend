using System.ComponentModel.DataAnnotations.Schema;
namespace Api.Entidad
{
    public class PrivilegioUsuarioDistribuidora : BasePersistente
    {
        public int IdUsuarioDistribuidora { get; set; }
        [ForeignKey("Privilegio")]
        public int IdPrivilegio { get; set; }
        public Privilegio Privilegio { get; set; }

        [NotMapped]
        public int Id { get; set; }
        
        [NotMapped]
        public string Nombre { get; set; }
        
        [NotMapped]
        public string Descripcion { get; set; }
        
        [NotMapped]
        public bool Seleccionado { get; set; }

        [NotMapped]
        public Entidad.Enums.ProcesoPrivilegioUsuarioDistribuidora Proceso { get; set; }
    }
}