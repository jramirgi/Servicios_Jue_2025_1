using Servicios_Jue.Clases;
using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Servicios_Jue.Controllers
{
    //Se define la ruta del servicio
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Clientes")]
    public class ClientesController : ApiController
    {
        /*
         * POST: Insertar información
         * PUT:  Actualizar información
         * DELETE: Eliminar informacion
         * GET:  Consultar informacion
         */
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] CLIEnte Cliente)
        {
            //El método Post, debe invocar el método Insertar de la clase clsCliente
            //Se crea el objeto de tipo clsCliente
            clsCliente _cliente = new clsCliente();
            _cliente.cliente = Cliente;
            //invocar el método de insertar
            return _cliente.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] CLIEnte Cliente)
        {
            //El método Put, debe invocar el método Actualizar de la clase clsCliente
            //Se crea el objeto de tipo clsCliente
            clsCliente _cliente = new clsCliente();
            _cliente.cliente = Cliente;
            //invocar el método de insertar
            return _cliente.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] CLIEnte Cliente)
        {
            //El método Delete, debe invocar el método Eliminar de la clase clsCliente
            //Se crea el objeto de tipo clsCliente
            clsCliente _cliente = new clsCliente();
            _cliente.cliente = Cliente;
            //invocar el método de insertar
            return _cliente.Eliminar();
        }
        [HttpGet]
        [Route("ConsultarXDocumento")]
        public CLIEnte ConsultarXDocumento(string Documento)
        {
            //Se crea el objeto de la clase ClsCliente, y se invoca el método Consultar
            clsCliente _ciente = new clsCliente();
            return _ciente.Consultar(Documento);
        }
        
        [HttpGet]
        [Route("ClientesConTelefonos")]
        public IQueryable ClientesConTelefonos()
        {
            //Se crea el objeto de la clase ClsCliente, y se invoca el método Consultar
            clsCliente _ciente = new clsCliente();
            return _ciente.ConsultarConTelefono();
        }
    }
}