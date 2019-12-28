using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeepWriting.ViewModels
{
    public class RecuperarSenha
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}