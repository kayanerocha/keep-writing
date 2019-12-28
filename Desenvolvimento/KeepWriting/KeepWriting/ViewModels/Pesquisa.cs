using KeepWriting.LocalResource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeepWriting.ViewModels
{
    public class Pesquisa
    {
        public string Nome { get; set; }
    }

    public class ResultadoPesquisa
    {
        [Display(Name = "titulo", ResourceType = typeof(Resource))]
        public string Titulo { get; set; }

        [Display(Name = "capa", ResourceType = typeof(Resource))]
        public byte[] Capa { get; set; }

        [Display(Name = "autor", ResourceType = typeof(Resource))]
        public string Autor { get; set; }
    }
}