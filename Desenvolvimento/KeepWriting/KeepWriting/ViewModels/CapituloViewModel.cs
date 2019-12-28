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
    public class CapituloViewModel
    {
        public int IdCapitulo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O título é obrigatório.")]
        [StringLength(50, ErrorMessage = "O título deve possuir no máximo 50 caracteres.")]
        [Display(Name = "titulo", ResourceType = typeof(Resource))]
        public string Titulo { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "O capítulo deve ser escrito.")]
        [Display(Name = "conteudo", ResourceType = typeof(Resource))]
        [AllowHtml]
        public string Conteudo { get; set; }

        public bool Situacao { get; set; }

        public string Status { get; set; }

        public int IdHistoria { get; set; }

        public Userdetails userdetails {get;set;}
    }
}