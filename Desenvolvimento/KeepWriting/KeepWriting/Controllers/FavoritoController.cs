using KeepWriting.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeepWriting.Controllers
{
    public class FavoritoController : Controller
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();

        // GET: Favorito
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Favoritar(int idHistoria, Favorito favorito)
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            Favorito verifFavorito = db.Favorito.Where(f => f.IdUsuario == usuarioLogado.IdUsuario && f.IdHistoria == idHistoria).FirstOrDefault();
            Historia historia = db.Historia.Find(idHistoria);
            if (verifFavorito == null)
            {
                favorito.IdHistoria = idHistoria;
                favorito.IdUsuario = usuarioLogado.IdUsuario;
                historia.QtdFavoritos += 1;
                db.Entry(historia).State = EntityState.Modified;
                db.Entry(historia).Property(p => p.QtdVisualizacoes).IsModified = false;
                db.Favorito.Add(favorito);
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Historia", action = "Detalhes", id = idHistoria });
            }
            else
            {
                ModelState.AddModelError("", "Você ja favoritou esta história.");
                return RedirectToRoute(new { controller = "Historia", action = "Detalhes", id = idHistoria });
            }
        }
    }
}