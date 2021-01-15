namespace Api.Entidad
{
    public class FiltroPrivilegio
    {
        public FiltroPrivilegio()
        {
            Ids = new int[0];
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Eliminado { get; set; }
        public int[] Ids { get; set; }
    }
}
