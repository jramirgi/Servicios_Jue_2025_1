//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Servicios_Jue.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario_Perfil
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idPerfil { get; set; }
        public bool Activo { get; set; }
    
        public virtual Perfil Perfil { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
