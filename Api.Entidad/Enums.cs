namespace Api.Entidad.Enums
{
    public enum Estado
    {
        Pendiente = 1,
        Completado = 2
    }
    public enum TipoPedido
    {
        Cliente = 1,
        Consolidado = 2,
        Proveedor = 3
    }

    public enum ProcesoUsuarioDistribuidora
    {
        TrabajadorLeeQR = 1,
        TrabajadorEnviaSolicitud = 2,
        DistribuidoraLeeQR = 3,
        DistribuidoraEnviaSolicitud = 4
    }

    public enum ProcesoPrivilegioUsuarioDistribuidora
    {
        AsignaActualiza = 1
    }

    public enum ProcesoPedido
    {
        NuevoPedidoCliente = 1
    }
}
