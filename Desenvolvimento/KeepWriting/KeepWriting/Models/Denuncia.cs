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
    
    public partial class Denuncia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Denuncia()
        {
            this.DenunciaCapitulo = new HashSet<DenunciaCapitulo>();
            this.DenunciaComentario = new HashSet<DenunciaComentario>();
            this.DenunciaCompeticao = new HashSet<DenunciaCompeticao>();
            this.DenunciaEvento = new HashSet<DenunciaEvento>();
            this.DenunciaHistoria = new HashSet<DenunciaHistoria>();
            this.DenunciaUsuario = new HashSet<DenunciaUsuario>();
        }
    
        public int IdDenuncia { get; set; }
        public string Motivo { get; set; }
        public string Explicacao { get; set; }
        public string Data { get; set; }
        public int IdUsuario { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenunciaCapitulo> DenunciaCapitulo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenunciaComentario> DenunciaComentario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenunciaCompeticao> DenunciaCompeticao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenunciaEvento> DenunciaEvento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenunciaHistoria> DenunciaHistoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenunciaUsuario> DenunciaUsuario { get; set; }
    }
}