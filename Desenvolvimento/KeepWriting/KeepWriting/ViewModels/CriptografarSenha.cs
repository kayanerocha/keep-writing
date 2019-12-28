using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KeepWriting.Models
{
    public class CriptografarSenha
    {
        public static string criptografarSenha(string senha)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(senha)));
        }
    }
}