using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Models
{
    public class FacturaDetalle
    {
        public FACTura factura { get; set; }
        public DEtalleFActura detalle { get; set; }
    }
}