using System;
using System.Collections.Generic;

namespace Api.Entidad
{
    public class Distribuidora: BasePersistente
    {
        public int Id { get; set; }
        public string Identificador { get; set; }
        public string RazonSocial { get; set; }
        public string Giro { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EsVerificado { get; set; }

        public ICollection<Telefono> Telefonos { get; set; }
        public ICollection<UsuarioDistribuidora> UsuarioDistribuidora { get; set; }
        public ICollection<Documento> Documentos { get; set; }
    }
}
