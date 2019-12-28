using KeepWriting.LocalResource;
using KeepWriting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeepWriting.ViewModels
{
    public class EventoViewModel
    {
        public EventoLiterario eventoLiterario { get; set; }
        public Userdetails userdetails { get; set; }

        public int IdEvento { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "O nome deve possuir no máximo 50 caracteres.")]
        [Display(Name = "nome", ResourceType = typeof(Resource))]
        public string Nome { get; set; }

        [StringLength(1000, ErrorMessage = "A descrição deve possuir no máximo 1000 caracteres.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A descrição é obrigatória.")]
        [Display(Name = "descricao", ResourceType = typeof(Resource))]
        public string Descricao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "dataInicio", ResourceType = typeof(Resource))]
        public System.DateTime DataInicio { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime EditarDataInicio { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A data de fim é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "dataFim", ResourceType = typeof(Resource))]
        public System.DateTime DataFim { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime EditarDataFim { get; set; }

        public int IdUsuario { get; set; }

        [Display(Name = "usuario", ResourceType = typeof(Resource))]
        public string Usuario { get; set; }

        public int IdLocalizacao { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "O estado deve possuir no máximo 30 caracteres.")]
        [Display(Name = "estado", ResourceType = typeof(Resource))]
        public string Estado { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória", AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "A cidade deve possuir no máximo 30 caracteres.")]
        [Display(Name = "cidade", ResourceType = typeof(Resource))]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "O endereço deve possuir no máximo 50 caracteres.")]
        [Display(Name = "endereco", ResourceType = typeof(Resource))]
        public string Endereco { get; set; }

        [StringLength(50, ErrorMessage = "O local deve possuir no máximo 50 caracteres.")]
        [Display(Name = "local", ResourceType = typeof(Resource))]
        public string Local { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "imagem", ResourceType = typeof(Resource))]
        public HttpPostedFileBase ImagemUpload { get; set; }

        public string ImagemEdit { get; set; }

        public string Status { get; set; }

        [Display(Name = "statusValor", ResourceType = typeof(Resource))]
        public string StatusValor { get; set; }

        [Display(Name = "imagem", ResourceType = typeof(Resource))]
        public byte[] Imagem { get; set; }

        [Display(Name = "link", ResourceType = typeof(Resource))]
        public string Link { get; set; }
    }
}