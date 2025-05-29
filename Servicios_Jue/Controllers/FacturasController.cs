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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Facturas")]
    [Authorize]
    public class FacturasController : ApiController
    {
        [HttpGet]
        [Route("ListarProductos")]
        public IQueryable ListarProductos(int NumeroFactura)
        {
            clsFactura Factura = new clsFactura();
            return Factura.ListarProductos(NumeroFactura);
        }
        [HttpPost]
        [Route("GrabarFactura")]
        public string GrabarFactura([FromBody] FacturaDetalle facturaDet)//[FromBody] FacturaDetalle factura)
        {
            clsFactura factura = new clsFactura();
            factura.factura = facturaDet.factura;
            factura.detalleFactura = facturaDet.detalle;
            return factura.GrabarFactura();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int NumeroDetalle)
        {
            clsFactura Factura = new clsFactura();
            return Factura.EliminarProducto(NumeroDetalle);
        }
    }
}