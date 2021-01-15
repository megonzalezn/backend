using Api.Data;

namespace Api.Negocio
{
    public class DatosFactory
    {
        private ApiRepository _repo;
        public IApiRepository Datos
        {
            get
            {
                if (_repo == null)
                {
                    _repo = new ApiRepository(new ApiContext());
                }
                return _repo;
            }
        }
    }
}
