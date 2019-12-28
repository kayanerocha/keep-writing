using KeepWriting.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KeepWriting.Repositorios
{
    public class GestaoElementos
    {
        public static bool VerificarAutenticacao()
        {
            var usuarioLogado = (Usuario)HttpContext.Current.Session["usuario"];
            if (usuarioLogado != null)
                return true;
            else
                return false;
        }

        public static bool VerifStatusUsuario()
        {
            var usuarioLogado = (Usuario)HttpContext.Current.Session["usuario"];
            if (usuarioLogado.Status == "Ativado")
                return true;
            else
                return false;
        }

        public static bool VerificarElemento(string status, int idAutor)
        {
            var usuarioLogado = (Usuario)HttpContext.Current.Session["usuario"];
            if (status == "Desativado" || idAutor != usuarioLogado.IdUsuario)
                return false;
            else
                return true;
        }

        public static int SalvarImagem(HttpPostedFileBase imagemUpload)
        {
            var imageTypes = new string[]{
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
            if (imagemUpload == null || imagemUpload.ContentLength == 0)
            {
                //imagem = null;
                return 0;
            }
            else if (imagemUpload != null || imagemUpload.ContentLength > 0)
            {
                if (!imageTypes.Contains(imagemUpload.ContentType))
                {
                    //ModelState.AddModelError("ImagemUpload", "Escolha uma imagem GIF, JPG ou PNG.");
                    return 1;
                }
                //lemos a imagem e a seguir os bytes armazenados
                //using (var binaryReader = new BinaryReader(imagemUpload.InputStream))
                    //eventoLiterario.Imagem = binaryReader.ReadBytes(eventoViewModel.ImagemUpload.ContentLength);
                    return 2;
            }
            return 3;
        }
    }
}