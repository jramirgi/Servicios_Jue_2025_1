using Servicios_Jue.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Servicios_Jue.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/TipoTelefonos")]
    [Authorize]
    public class TipoTelefonosController : ApiController
    {
        [HttpGet]
        [Route("LlenarCombo")]
        public IQueryable LlenarCombo()
        {
            clsTipoTelefono tipoTelefono = new clsTipoTelefono();
            return tipoTelefono.LlenarCombo();
        }
    }
}
