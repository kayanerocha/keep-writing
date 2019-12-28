using KeepWriting.LocalResource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeepWriting.ViewModels
{
    public class HistoriaXCompeticaoVM
    {
        public int IdHistoria { get; set; }

        public int IdCompeticao { get; set; }

        public string Competicao { get; set; }

        [Display(Name = "titulo", ResourceType = typeof(Resource))]
        public string Titulo { get; set; }
    }
}