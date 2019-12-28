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
    public class CompeticaoViewModel
    {
        public int IdCompeticao { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "O nome deve possuir no máximo 30 caracteres.")]
        [Display(Name = "nome", ResourceType = typeof(Resource))]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.", AllowEmptyStrings = false)]
        [StringLength(1000, ErrorMessage = "A descrição deve possuir no máximo 1000 caracteres.")]
        [Display(Name = "descricao", ResourceType = typeof(Resource))]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data de início de inscrição é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [Display(Name = "inicioInscri", ResourceType = typeof(Resource))]
        public System.DateTime DataInicioInscri { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [Display(Name = "fimInscri", ResourceType = typeof(Resource))]
        public System.DateTime DataFimInscri { get; set; }

        [Required(ErrorMessage = "A data de início de votação é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [Display(Name = "inicioVota", ResourceType = typeof(Resource))]
        public System.DateTime DataInicioVota { get; set; }

        [Required(ErrorMessage = "A data de fim de votação é obrigatória", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [Display(Name = "fimVota", ResourceType = typeof(Resource))]
        public System.DateTime DataFimVota { get; set; }

        public string Status { get; set; }

        [Display(Name = "statusValor", ResourceType = typeof(Resource))]
        public string StatusValor { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        [Display(Name = "usuario", ResourceType = typeof(Resource))]
        public string NomeUsuario { get; set; }

        [Display(Name = "pontoExperiencia", ResourceType = typeof(Resource))]
        public int PontoExperiencia { get; set; }

        public Userdetails userdetails { get; set; }
    }

    public class EditarCompeticao
    {
        public int IdCompeticao { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(30, ErrorMessage = "O nome deve possuir no máximo 30 caracteres.")]
        [Display(Name = "nome", ResourceType = typeof(Resource))]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.", AllowEmptyStrings = false)]
        [StringLength(1000, ErrorMessage = "A descrição deve possuir no máximo 1000 caracteres.")]
        [Display(Name = "descricao", ResourceType = typeof(Resource))]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data de início de inscrição é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Remote("DataInicioInscriValid", "Competicao", ErrorMessage = "A competição pode iniciar somente a partir de amanhã.")]
        [Display(Name = "inicioInscri", ResourceType = typeof(Resource))]
        public System.DateTime DataInicioInscri { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "fimInscri", ResourceType = typeof(Resource))]
        public System.DateTime DataFimInscri { get; set; }

        [Required(ErrorMessage = "A data de início de votação é obrigatória.", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "inicioVota", ResourceType = typeof(Resource))]
        public System.DateTime DataInicioVota { get; set; }

        [Required(ErrorMessage = "A data de fim de votação é obrigatória", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "fimVota", ResourceType = typeof(Resource))]
        public System.DateTime DataFimVota { get; set; }

        public string Status { get; set; }

        public Nullable<int> IdUsuario { get; set; }

        [Display(Name = "usuario", ResourceType = typeof(Resource))]
        public string NomeUsuario { get; set; }

        [Display(Name = "pontoExperiencia", ResourceType = typeof(Resource))]
        public int PontoExperiencia { get; set; }

        public Userdetails userdetails { get; set; }
    }
}
