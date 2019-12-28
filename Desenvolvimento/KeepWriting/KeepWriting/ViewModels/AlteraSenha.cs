using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeepWriting.ViewModels
{
    public class AlteraSenha
    {
        [Required(ErrorMessage = "O código é obrigatório.", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        public string Otp { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.", AllowEmptyStrings = false)]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve possuir no mínimo 6 caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.", AllowEmptyStrings = false)]
        [Display(Name = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "A senha não corresponde com a anterior.")]
        public string ConfirmaSenha { get; set; }
    }
}