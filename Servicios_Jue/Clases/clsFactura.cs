using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Clases
{
    public class clsFactura
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public FACTura factura { get; set; }
        public DEtalleFActura detalleFactura { get; set; }
        public string GrabarFactura()
        {
            if (factura.Numero == 0)
            {
                int NroFactura = Convert.ToInt32(GrabarEncabezado());
            }
            detalleFactura.Numero = factura.Numero;
            return GrabarDetalle();
        }
        private string GrabarEncabezado()
        {
            try
            {
                factura.Numero = GenerarNumeroFactura();
                factura.Fecha = DateTime.Now;
                dbSuper.FACTuras.Add(factura);
                dbSuper.SaveChanges();
                return factura.Numero.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private int GenerarNumeroFactura()
        {
            return dbSuper.FACTuras.Select(f => f.Numero).DefaultIfEmpty(0).Max() + 1;
            /*
            int Numero = 0;
            try
            {
                Numero = dbSuper.FACTuras.Max(x => x.Numero) + 1;
            }
            catch (Exception)
            {
                Numero = 1;
            }
            return Numero;
            */
        }
        private string GrabarDetalle()
        {
            try
            {
                //detalleFactura = factura.DEtalleFActuras.FirstOrDefault();
                dbSuper.DEtalleFActuras.Add(detalleFactura);
                dbSuper.SaveChanges();
                return factura.Numero.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public IQueryable ListarProductos(int NumeroFactura)
        {
            return from D in dbSuper.Set<DEtalleFActura>()
                   join P in dbSuper.Set<PRODucto>()
                   on D.CodigoProducto equals P.Codigo
                   join TP in dbSuper.Set<TIpoPRoducto>()
                   on P.CodigoTipoProducto equals TP.Codigo
                   where D.Numero == NumeroFactura
                   select new
                   {
                       Eliminar = "<img src=\"../Imagenes/Eliminar.png\" onclick=\"Eliminar(" + D.Codigo + ", " + D.Cantidad + ", " + D.ValorUnitario + ")\"/>",
                       Tipo_Producto = TP.Nombre,
                       Producto = P.Nombre,
                       Cantidad = D.Cantidad,
                       Valor_Unitario = D.ValorUnitario,
                       Subtotal = D.Cantidad * D.ValorUnitario
                   };
        }
        public string EliminarProducto(int Codigo)
        {
            try
            {
                detalleFactura = dbSuper.DEtalleFActuras.FirstOrDefault(d => d.Codigo == Codigo);
                dbSuper.DEtalleFActuras.Remove(detalleFactura);
                dbSuper.SaveChanges();
                return "Se eliminó";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}