using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Clases
{
    public class clsCliente
    {
        //Crear un atributo de la clase DBSuperEntities que me permita manipular los datos de mi bd a través del EF
        DBSuperEntities dbSuper = new DBSuperEntities();
        //Creamos una propieadad del tipo "CLIENTE" que se va a recibir como parámetro de entrada
        public CLIEnte cliente { get; set; }
        public string Insertar()
        {
            try
            {
                //Se invoca el conjunto de datos de la clase que quiero grabar, y se invoca el método add
                dbSuper.CLIEntes.Add(cliente);
                //Al terminar, se debe invocar el método SaveChanges, para que se grabe en la base de datos
                dbSuper.SaveChanges();
                return "Se grabó el cliente " + cliente.Nombre + " " + cliente.PrimerApellido;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<CLIEnte> ConsultarTodos()
        {
            return dbSuper.CLIEntes.OrderBy(p => p.PrimerApellido).ToList();
        }
        public string Actualizar()
        {
            //Para actualizar, vamos a utilizar el método AddorUpdate, que permite actualizar la información de un cliente que
            //ya existe. El problema que se puede presentar, es que si se envía la información repetida, sin la clave primaria,
            //se graba doble la información
            //Siempre se actualizan todos los campos, si se quiere actualizar uno o varios, pero no todos, se tiene que hacer un p
            //proceso diferente
            try
            {
                dbSuper.CLIEntes.AddOrUpdate(cliente);
                dbSuper.SaveChanges();
                return "Se actualizaron los datos del cliente con documento: " + cliente.Documento;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Eliminar()
        {
            //Para eliminar un elemento, primero se tiene que consultar, y si existe se puede eliminar, de lo contrario no hace nada
            CLIEnte _cliente = Consultar(cliente.Documento);
            if (_cliente == null)
            {
                return "El cliente no existe en la base de datos";
            }
            dbSuper.CLIEntes.Remove(_cliente);
            dbSuper.SaveChanges();
            return "Se eliminó el cliente: " + _cliente.Nombre + " " + _cliente.PrimerApellido;
        }
        public CLIEnte Consultar(string documento)
        {
            //variables o expresiones lambda. Son variables que toman el tipo del objeto que están manipulando, sin tener que hacer una instancia de la
            //clase a la que se refieren
            //Para consultar un dato, se invoca el método "FirstOrDefault". Dentro de los paréntesis se deben poner las condiciones para la búsqueda
            return dbSuper.CLIEntes.FirstOrDefault(c => c.Documento == documento);
        }
        public IQueryable ConsultarConTelefono()
        {
            return from C in dbSuper.Set<CLIEnte>()
                   join T in dbSuper.Set<TELEfono>()
                   on C.Documento equals T.Documento into CTe
                   from T in CTe.DefaultIfEmpty()
                   orderby C.PrimerApellido, C.SegundoApellido, C.Nombre
                   group CTe by new { C.Documento, C.Nombre, C.PrimerApellido, C.SegundoApellido, C.FechaNacimiento, C.Email, C.Direccion }
                   into g
                   select new
                   {
                       Editar = "<img src=\"../Imagenes/Edit.png\" onclick=\"Editar('" + g.Key.Documento + "', '" + g.Key.Nombre + "', '" + 
                                g.Key.PrimerApellido + "', '" + g.Key.SegundoApellido + "', '" + g.Key.FechaNacimiento + 
                                "', '" + g.Key.Email + "', '" + g.Key.Direccion + "')\" style=\"cursor:grab\"/>",
                       Nro_telefonos = g.Count(),
                       Documento = g.Key.Documento,
                       Cliente = g.Key.PrimerApellido + " " + g.Key.SegundoApellido + " " + g.Key.Nombre,
                       FechaNacimiento = g.Key.FechaNacimiento,
                       Email = g.Key.Email,
                       Direccion = g.Key.Direccion
                   };
        }
    }
}