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
    
    public partial class DenunciaCompeticao
    {
        public int IdDenunciaCompeticao { get; set; }
        public int IdDenuncia { get; set; }
        public int IdCompeticao { get; set; }
    
        public virtual Competicao Competicao { get; set; }
        public virtual Denuncia Denuncia { get; set; }
    }
}
