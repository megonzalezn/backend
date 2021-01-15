using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compliance.Data.Entitites;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Compliance.Data.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            _UrlHelper = new UrlHelper(request);
        }

        public ParametrosModel Create(Parametro parametro)
        {
            return new ParametrosModel()
            {
                Url = _UrlHelper.Link("Parametros", new { id = parametro.Id }),
                Id = parametro.Id,
                Nombre = parametro.Nombre,
                Vigente = parametro.Vigente,
                TipoParametroId = parametro.TipoParametroId,
                Tipo = Create(parametro.Tipo)
            };
        }

        public TipoParametrosModel Create(TipoParametro tipoParametro)
        {
            return new TipoParametrosModel()
            {
                Url = _UrlHelper.Link("tipoParametro", new { id = tipoParametro.Id }),
                Id = tipoParametro.Id,
                Nombre = tipoParametro.Nombre,
                Vigente = tipoParametro.Vigente

            };
        }


        public Parametro Parse(ParametrosModel model)
        {
            try
            {
                var parametro = new Parametro()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Vigente = model.Vigente,
                    TipoParametroId = model.TipoParametroId,
                    Tipo = Parse(model.Tipo)
                };

                return parametro;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TipoParametro Parse(TipoParametrosModel model)
        {
            try
            {
                var tipoParametro = new TipoParametro()
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Vigente = model.Vigente,
                };

                return tipoParametro;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}
