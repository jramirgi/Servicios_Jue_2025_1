using Servicios_Jue.Clases;
using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicios_Jue.Controllers
{
    [RoutePrefix("api/Productos")]
    public class ProductosController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<PRODucto> ConsultarTodos()
        {
            clsProducto Producto = new clsProducto();
            return Producto.ConsultarTodos();
        }
        [HttpGet]
        [Route("Consultar")]
        public PRODucto Consultar(int Codigo)
        {
            clsProducto Producto = new clsProducto();
            return Producto.Consultar(Codigo);
        }
        [HttpGet]
        [Route("ConsultarXTipoProducto")]
        public List<PRODucto> ConsultarXTipoProducto(int TipoProducto)
        {
            clsProducto Producto = new clsProducto();
            return Producto.ConsultarXTipoProducto(TipoProducto);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] PRODucto producto)
        {
            clsProducto Producto = new clsProducto();
            Producto.producto = producto;
            return Producto.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] PRODucto producto)
        {
            clsProducto Producto = new clsProducto();
            Producto.producto = producto;
            return Producto.Actualizar();
        }
        [HttpPut]
        [Route("Inactivar")]
        public string Inactivar(int Codigo)
        {
            clsProducto Producto = new clsProducto();
            return Producto.ModificarEstado(Codigo, false);
        }
        [HttpPut]
        [Route("Activar")]
        public string Activar(int Codigo)
        {
            clsProducto Producto = new clsProducto();
            return Producto.ModificarEstado(Codigo, true);
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] PRODucto producto)
        {
            clsProducto Producto = new clsProducto();
            Producto.producto = producto;
            return Producto.Eliminar();
        }
        [HttpDelete]
        [Route("EliminarXCodigo")]
        public string EliminarXCodigo(int Codigo)
        {
            clsProducto Producto = new clsProducto();
            return Producto.Eliminar(Codigo);
        }
    }
}