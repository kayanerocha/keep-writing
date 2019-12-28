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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Comentario = new HashSet<Comentario>();
            this.Competicao = new HashSet<Competicao>();
            this.Denuncia = new HashSet<Denuncia>();
            this.DenunciaUsuario = new HashSet<DenunciaUsuario>();
            this.EventoLiterario = new HashSet<EventoLiterario>();
            this.Favorito = new HashSet<Favorito>();
            this.Historia = new HashSet<Historia>();
            this.ListaLeitura = new HashSet<ListaLeitura>();
            this.Voto = new HashSet<Voto>();
        }
    
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public System.DateTime DataNasc { get; set; }
        public byte[] Foto { get; set; }
        public string Descricao { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Nullable<int> QtdHistorias { get; set; }
        public int QtdPonto { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public Nullable<int> UsuarioAlteraStatus { get; set; }
        public Nullable<bool> EmailVerificado { get; set; }
        public Nullable<System.Guid> CodigoAtivacao { get; set; }
        public string Otp { get; set; }
        public int IdNivel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comentario> Comentario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Competicao> Competicao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Denuncia> Denuncia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DenunciaUsuario> DenunciaUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventoLiterario> EventoLiterario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorito> Favorito { get; set; }
        public virtual Nivel Nivel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Historia> Historia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListaLeitura> ListaLeitura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Voto> Voto { get; set; }
    }
}
