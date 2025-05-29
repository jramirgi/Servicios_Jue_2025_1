using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Clases
{
    public class clsEmpleado
    {
        private DBSuperEntities dbSuper = new DBSuperEntities(); // Es el atributo (Propiedad) para gestionar la conexión a la base de datos
        public EMPLeado empleado { get; set; } //Propiedad para manipular la información en la base de datos: Permite agregar, modificar o eliminar
        public string Insertar()
        {
            try
            {
                dbSuper.EMPLeadoes.Add(empleado); //Agregar el objeto empleado a la lista de "empleadoes". Todavía no se agrega a la base de datos. Se debe invocar el método SaveChanges()
                dbSuper.SaveChanges(); //Guardar los cambios en la base de datos
                return "Empleado insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el empleado: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                //Antes de actualizar un elemento (empleado), se debe consultar para verificar que exista, y ahí si poderlo actualizar
                EMPLeado empl = Consultar(empleado.Documento);
                if (empl == null)
                {
                    return "El empleado con el documento ingresado no existe, por lo tanto no se puede actualizar";
                }
                //El empleado existe lo podemos actualizar
                dbSuper.EMPLeadoes.AddOrUpdate(empleado); //Actualiza el objeto empleado en la lista de "empleadoes". Todavía no se actualiza en la base de datos. Se debe invocar el método SaveChanges()
                dbSuper.SaveChanges(); //Guardar los cambios en la base de datos
                return "Se actualizó el empleado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el empleado: " + ex.Message;
            }
        }
        public List<EMPLeado> ConsultarTodos()
        {
            return dbSuper.EMPLeadoes
                .OrderBy(e => e.PrimerApellido) //OrderBy() es una función que permite ordenar los elementos de una lista de acuerdo a un criterio específico. En este caso, se ordena por el primer apellido
                .ToList(); //ToList() es una función que convierte una lista de datos en una lista de objetos
        }
        public EMPLeado Consultar(string Documento)
        {
            //Expresiones lambda.  => permite definir funciones anónimas o instancias de objetos, sin la creación formal, dependiendo de la lista a la cual se hace referencia
            //FirstOrDefault. Es una función que permite consultar el primer elemento de una lista que cumple las condiciones solicitadas
            return dbSuper.EMPLeadoes.FirstOrDefault(e => e.Documento == Documento);
        }
        public string Eliminar()
        {
            try
            {
                //Antes de eliminar se debe verificar si el empleado existe
                EMPLeado empl = Consultar(empleado.Documento);
                if (empl == null)
                {
                    return "El empleado con el documento ingresado no existe, por lo tanto no se puede eliminar";
                }
                //El empleado existe lo podemos eliminar. Se elimina el objeto empleado que se busca, no el que se envía como parámetro
                dbSuper.EMPLeadoes.Remove(empl); //Eliminar el objeto empleado de la lista de "empleadoes". Todavía no se elimina de la base de datos. Se debe invocar el método SaveChanges()
                dbSuper.SaveChanges(); //Guardar los cambios en la base de datos
                return "Se eliminó el empleado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el empleado: " + ex.Message;
            }
        }
        public string Eliminar(string Documento)
        {
            try
            {
                //Antes de eliminar se debe verificar si el empleado existe
                EMPLeado empl = Consultar(Documento);
                if (empl == null)
                {
                    return "El empleado con el documento ingresado no existe, por lo tanto no se puede eliminar";
                }
                //El empleado existe lo podemos eliminar. Se elimina el objeto empleado que se busca, no el que se envía como parámetro
                dbSuper.EMPLeadoes.Remove(empl); //Eliminar el objeto empleado de la lista de "empleadoes". Todavía no se elimina de la base de datos. Se debe invocar el método SaveChanges()
                dbSuper.SaveChanges(); //Guardar los cambios en la base de datos
                return "Se eliminó el empleado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el empleado: " + ex.Message;
            }
        }
        public IQueryable ConsultarXUsuario(string Usuario)
        {
            return from E in dbSuper.Set<EMPLeado>()
                   join EC in dbSuper.Set<EMpleadoCArgo>()
                   on E.Documento equals EC.Documento
                   join C in dbSuper.Set<CARGo>()
                   on EC.CodigoCargo equals C.Codigo
                   join U in dbSuper.Set<Usuario>()
                   on E.Documento equals U.Documento_Empleado
                   where U.userName == Usuario
                   select new
                   {
                       idEmpleado = EC.Codigo,
                       Empleado = E.Nombre + " " + E.PrimerApellido + " " + E.SegundoApellido,
                       Cargo = C.Nombre
                   };
        }
    }
}