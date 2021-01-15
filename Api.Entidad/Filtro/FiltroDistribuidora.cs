using System;

namespace Api.Entidad
{
    public class FiltroDistribuidora
    {
        public FiltroDistribuidora()
        {
            Ids = new int[0];
        }
        public int Id { get; set; }
        public string Identificador { get; set; }
        public int[] Ids { get; set; }
        public string RazonSocial { get; set; }
        public string Giro { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public bool? Eliminado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool? EsVerificado { get; set; }
        public string[] Includes { get; set; }
    }
}
