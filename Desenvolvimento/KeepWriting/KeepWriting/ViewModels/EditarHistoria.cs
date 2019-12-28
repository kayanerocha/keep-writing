using KeepWriting.LocalResource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeepWriting.ViewModels
{
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