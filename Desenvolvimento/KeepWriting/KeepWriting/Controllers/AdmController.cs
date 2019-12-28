using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeepWriting.Controllers
{
    public class AdmController : BaseController
    {
        // GET: Adm
        public ActionResult Index()
        {
            var usuarioLogado = HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            else
            {
                var usuario = (KeepWriting.Models.Usuario)HttpContext.Session["usuario"];
                ViewBag.Nome = usuario.Nome;
                ViewBag.Email = usuario.Email;
            }            
            return View();
        }
    }
}