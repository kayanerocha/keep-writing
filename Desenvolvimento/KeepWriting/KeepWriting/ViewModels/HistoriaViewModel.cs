using KeepWriting.LocalResource;
using KeepWriting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeepWriting.ViewModels
{
    public class HistoriaViewModel
    {
        public int IdHistoria { get; set; }

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O título é obirgatório.", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "O título deve possuir no máximo 100 caracteres.")]
        [Display(Name = "titulo", ResourceType = typeof(Resource))]
        public string Titulo { get; set; }

        [StringLength(1000, ErrorMessage = "O título deve possuir no máximo 1000 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A sinopse é obrigatória.")]
        [Display(Name = "sinopse", ResourceType = typeof(Resource))]
        public string Sinopse { get; set; }

        public int ClassificacaoPortugues { get; set; }

        public int ClassificacaoIngles { get; set; }

        [Display(Name = "classificacao", ResourceType = typeof(Resource))]
        public string Classificacao { get; set; }

        [Display(Name = "favoritos", ResourceType = typeof(Resource))]
        public Nullable<int> QtdFavoritos { get; set; }

        [Display(Name = "visualizacoes", ResourceType = typeof(Resource))]
        public int QtdVisualizacoes { get; set; }

        [Display(Name = "capa", ResourceType = typeof(Resource))]
        public byte[] Capa { get; set; }

        [Required(ErrorMessage = "A situação é obrigatória.")]
        public bool Situacao { get; set; }

        [Display(Name = "situacaoCriar", ResourceType = typeof(Resource))]
        public string SituacaoCriar { get; set; }

        [Display(Name = "situacaoListar", ResourceType = typeof(Resource))]
        public string SituacaoListar { get; set; }

        [Display(Name = "publicado", ResourceType = typeof(Resource))]
        public string publicado { get; set; }

        [Display(Name = "rascunho", ResourceType = typeof(Resource))]
        public string rascunho { get; set; }

        public int IdGeneroPortugues { get; set; }
        
        public int IdGeneroIngles { get; set; }

        public string NomePortugues { get; set; }

        public string NomeIngles { get; set; }

        [Required(ErrorMessage = "O idioma é obrigatório.", AllowEmptyStrings = false)]
        public int IdIdioma { get; set; }

        [Display(Name = "idioma", ResourceType = typeof(Resource))]
        public string Idioma { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Foto")]
        public HttpPostedFileBase ImagemUpload { get; set; }

        public Userdetails userdetails { get; set; }

        [Display(Name = "genero", ResourceType = typeof(Resource))]
        public string Genero { get; set; }

        [Display(Name = "usuario", ResourceType = typeof(Resource))]
        public string Usuario { get; set; }

        public string IdiomaHistoria { get; set; }

        [Display(Name = "status", ResourceType = typeof(Resource))]
        public string Status { get; set; }

        [Display(Name = "statusValor", ResourceType = typeof(Resource))]
        public string StatusValor { get; set; }

        [Display(Name = "livre", ResourceType = typeof(Resource))]
        public string livre { get; set; }

        [Display(Name = "dezesseis", ResourceType = typeof(Resource))]
        public string dezesseis { get; set; }

        [Display(Name = "dezoito", ResourceType = typeof(Resource))]
        public string dezoito { get; set; }
    }

    //public class Idioma{
    //    public int Id { get; set; }
    //    public string Descricao { get; set; }
    //}

    public class Classificacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class EditarHistoria
    {
        public int IdHistoria { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve possuir no máximo 100 caracteres.")]
        [Display(Name = "titulo", ResourceType = typeof(Resource))]
        public string Titulo { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "imagem", ResourceType = typeof(Resource))]
        public HttpPostedFileBase ImagemUpload { get; set; }

        public byte[] Capa { get; set; }

        [Display(Name = "genero", ResourceType = typeof(Resource))]
        public string Genero { get; set; }

        public int IdGeneroPortugues { get; set; }

        public int IdGeneroIngles { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A sinopse é obrigatória.")]
        [StringLength(5000, ErrorMessage = "A sinopse deve possuir no máximo 5000 caracteres.")]
        [Display(Name = "sinopse", ResourceType = typeof(Resource))]
        public string Sinopse { get; set; }

        [Display(Name = "classificacao", ResourceType = typeof(Resource))]
        public string Classificacao { get; set; }

        public int ClassificacaoPortugues { get; set; }

        public int ClassificacaoIngles { get; set; }

        [Required(ErrorMessage = "A situação é obrigatória.")]
        public bool Situacao { get; set; }

        [Display(Name = "situacaoCriar", ResourceType = typeof(Resource))]
        public string SituacaoCriar { get; set; }

        [Display(Name = "idioma", ResourceType = typeof(Resource))]
        public int IdIdioma { get; set; }

        public int IdUsuario { get; set; }
    }
}