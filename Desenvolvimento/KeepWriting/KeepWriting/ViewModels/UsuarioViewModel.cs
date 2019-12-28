using KeepWriting.LocalResource;
using KeepWriting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeepWriting.ViewModels
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "O nome deve possuir no máximo 100 caracteres.")]
        [Display(Name = "nome", ResourceType = typeof(Resource))]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [Display(Name = "dataNasc", ResourceType = typeof(Resource))]
        public System.DateTime DataNasc { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime EditarDataNasc { get; set; }

        [MaxLength(1000, ErrorMessage = "A descrição deve possuir no máximo 1000 caracteres.")]
        [Display(Name = "descricao", ResourceType = typeof(Resource))]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.", AllowEmptyStrings = false)]
        [MaxLength(100, ErrorMessage = "O e-mail deve possuir no máximo 1000 caracteres.")]
        [Display(Name = "email", ResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.", AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres.")]
        [MaxLength(16, ErrorMessage = "A senha deve possuir no máximo 16 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "senha", ResourceType = typeof(Resource))]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "A senha não corresponde com a anterior.")]
        [Display(Name = "confirmarSenha", ResourceType = typeof(Resource))]
        public string ConfirmarSenha { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "imagem", ResourceType = typeof(Resource))]
        public HttpPostedFileBase ImagemUpload { get; set; }

        [Display(Name = "imagem", ResourceType = typeof(Resource))]
        public byte[] Foto { get; set; }

        [Display(Name = "qtdHistorias", ResourceType = typeof(Resource))]
        public int QtdHistorias { get; set; }

        [Display(Name = "qtdPonto", ResourceType = typeof(Resource))]
        public int QtdPonto { get; set; }

        public string Status { get; set; }

        public int IdNivel { get; set; }

        [Display(Name = "nivel", ResourceType = typeof(Resource))]
        public string Nivel { get; set; }

        public Userdetails userdetails { get; set; }
    }
}