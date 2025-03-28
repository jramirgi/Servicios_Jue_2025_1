using Microsoft.Ajax.Utilities;
using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Clases
{
    public class clsProducto
    {
        private DBSuperEntities dbSuper = new DBSuperEntities(); // Es el atributo (Propiedad) para gestionar la conexión a la base de datos
        public PRODucto producto { get; set; }
        public string Insertar()
        {
            try
            {
                dbSuper.PRODuctoes.Add(producto); //Agregar el objeto empleado a la lista de "empleadoes". Todavía no se agrega a la base de datos. Se debe invocar el método SaveChanges()
                dbSuper.SaveChanges(); //Guardar los cambios en la base de datos
                return "Producto insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el producto: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                PRODucto prod = Consultar(producto.Codigo);
                if (prod == null)
                {
                    return "El código del producto no existe en la BD";
                }
                dbSuper.PRODuctoes.AddOrUpdate(producto);
                dbSuper.SaveChanges();
                return "El producto se actualizó correctamente";
            }
            catch (Exception ex)
            {
                return "Hubo un error al actualizar el producto: " + ex.Message;
            }
        }
        public PRODucto Consultar(int Codigo)
        {
            return dbSuper.PRODuctoes.FirstOrDefault(p => p.Codigo == Codigo);
        }
        public List<PRODucto> ConsultarTodos()
        {
            return dbSuper.PRODuctoes
                .OrderBy(p => p.Nombre)
                .ToList();
        }
        public List<PRODucto> ConsultarXTipoProducto(int CodTipoProducto)
        {
            return dbSuper.PRODuctoes
                .Where(p => p.CodigoTipoProducto == CodTipoProducto)
                .OrderBy(p => p.Nombre)
                .ToList();
        }
        public string Eliminar()
        {
            try
            {
                PRODucto prod = Consultar(producto.Codigo);
                if (prod == null)
                {
                    return "El código del producto no existe en la Base de Datos";
                }
                dbSuper.PRODuctoes.Remove(prod);
                dbSuper.SaveChanges();
                return "Se eliminó correctamente el producto de la base de datos";
            }
            catch (Exception ex)
            {
                return "Hubo un error al eliminar el producto: " + ex.Message;
            }
        }
        public string Eliminar(int Codigo)
        {
            try
            {
                PRODucto prod = Consultar(Codigo);
                if (prod == null)
                {
                    return "El código del producto no existe en la Base de Datos";
                }
                dbSuper.PRODuctoes.Remove(prod);
                dbSuper.SaveChanges();
                return "Se eliminó correctamente el producto de la base de datos";
            }
            catch (Exception ex)
            {
                return "Hubo un error al eliminar el producto: " + ex.Message;
            }
        }
        public string ModificarEstado(int Codigo, bool Activo)
        {
            //Activo es el valor de la variable, cuando es true el producto se activa, cuando es false se inactiva el producto
            try
            {
                PRODucto prod = Consultar(Codigo);
                if (prod == null)
                {
                    return "El código del producto no existe en la Base de Datos";
                }
                prod.Activo = Activo;
                dbSuper.SaveChanges();
                if (Activo)
                {
                    return "Se activó correctamente el producto";
                }
                else
                {
                    return "Se inactivó correctamente el producto";
                }
            }
            catch (Exception ex)
            {
                return "Hubo un error al modificar el estado del producto: " + ex.Message;
            }
        }
        public string GrabarImagenProducto(int idProducto, List<string> Imagenes)
        {
            try
            {
                foreach (string imagen in Imagenes)
                {
                    ImagenesProducto imagenProducto = new ImagenesProducto();
                    imagenProducto.idProducto = idProducto;
                    imagenProducto.NombreImagen = imagen;
                    dbSuper.ImagenesProductoes.Add(imagenProducto);
                    dbSuper.SaveChanges();
                }
                return "Se grabó la información en la base de datos";
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public IQueryable ListarImagenes(int idProducto)
        {
            return from P in dbSuper.Set<PRODucto>()
                       join TP in dbSuper.Set<TIpoPRoducto>()
                       on P.CodigoTipoProducto equals TP.Codigo
                       join I in dbSuper.Set<ImagenesProducto>()
                       on P.Codigo equals I.idProducto
                   where P.Codigo == idProducto
                   orderby I.NombreImagen
                   select new
                   {
                       idTipoProducto = TP.Codigo,
                       TipoProducto = TP.Nombre,
                       idProducto = P.Codigo,
                       Producto = P.Nombre,
                       Imagen = I.NombreImagen
                   };
        }
    }
}