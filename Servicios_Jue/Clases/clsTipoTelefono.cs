﻿using Servicios_Jue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Clases
{
    public class clsTipoTelefono
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public IQueryable LlenarCombo()
        {
            return from T in dbSuper.Set<TIpoTElefono>()
                   orderby T.Nombre
                   select new
                   {
                       Codigo = T.Codigo,
                       Nombre = T.Nombre
                   };
        }
    }
}