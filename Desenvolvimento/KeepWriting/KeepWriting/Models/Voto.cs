//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KeepWriting.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Voto
    {
        public int IdVoto { get; set; }
        public int IdUsuario { get; set; }
        public int IdInscricaoComp { get; set; }
        public int IdInscricaoHist { get; set; }
    
        public virtual Inscricao Inscricao { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}