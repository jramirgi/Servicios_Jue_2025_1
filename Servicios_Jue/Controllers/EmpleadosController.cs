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
    [RoutePrefix("api/Empleados")]
    [Authorize]
    public class EmpleadosController : ApiController
    {
        //GET: Se utiliza para consultar información, no se debe modificar la base de datos
        //POST: Se utiliza para insertar información en la base de datos
        //PUT: Se utiliza para modificar (Actualizar) información en la base de datos
        //DELETE: Se utiliza para eliminar información en la base de datos
        [HttpGet] //Es el servicio que se va a exponer: GET, POST, PUT, DELETE
        [Route("ConsultarTodos")] //Es el nombre de la funcionalidad que se va a ejecutar
        public List<EMPLeado> ConsultarTodos()
        {
            //Se crea una instancia de la clase clsEmpleado
            clsEmpleado Empleado = new clsEmpleado();
            //Se invoca el método ConsultarTodos() de la clase clsEmpleado
            return Empleado.ConsultarTodos();
        }
        [HttpGet]
        [Route("ConsultarXDocumento")]
        public EMPLeado ConsultarXDocumento(string Documento)
        {
            //Se crea una instancia de la clases clsEmpleado
            clsEmpleado Empleado = new clsEmpleado();
            return Empleado.Consultar(Documento); 
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] EMPLeado empleado)
        {
            //Se crea una instancia de la clase clsEmpleado
            clsEmpleado Empleado = new clsEmpleado();
            //Se pasa la propieadad empleado al objeto de la clases clsEmpleado
            Empleado.empleado = empleado;
            //Se invoca el método insertar
            return Empleado.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] EMPLeado empleado)
        {
            clsEmpleado Empleado = new clsEmpleado();
            Empleado.empleado = empleado;
            return Empleado.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] EMPLeado empleado)
        {
            clsEmpleado Empleado = new clsEmpleado();
            Empleado.empleado = empleado;
            return Empleado.Eliminar();
        }
        [HttpDelete]
        [Route("EliminarXDocumento")]
        public string EliminarXDocumento(string Documento)
        {
            clsEmpleado Empleado = new clsEmpleado();
            return Empleado.Eliminar(Documento);
        }
    }
}