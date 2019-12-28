using KeepWriting.LocalResource;
using KeepWriting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeepWriting.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.", AllowEmptyStrings = false)]
        [Display(Name = "email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha é obrigatória.")]
        [Display(Name = "senha", ResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "lembrar", ResourceType = typeof(Resource))]
        public bool Lembreme { get; set; }

        [Display(Name = "entrar", ResourceType = typeof(Resource))]
        public string Entrar { get; set; }

        public Userdetails userdetails { get; set; }
    }
}