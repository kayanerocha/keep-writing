using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KeepWriting.Helper;
using KeepWriting.Models;
using KeepWriting.Utilities;
using KeepWriting.ViewModels;

namespace KeepWriting.Controllers
{
    public class CapituloController : BaseController
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();

        // GET: Capitulo
        public ActionResult Listar(int? idHistoria)
        {
            var capitulos = db.Capitulo.Include(c => c.Historia);
            return View(capitulos.Where(c => c.IdHistoria == idHistoria && c.Status == "Ativado" && c.Situacao == true));
        }

        public ActionResult MeusCapitulos(int idHistoria)
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            if (usuarioLogado.Status == "Ativado")
            {
                var capitulos = db.Capitulo.Include(c => c.Historia);
                return View(capitulos.Where(c => c.IdHistoria == idHistoria));
            }
            else
            {
                return View("~/Views/Usuario/Status.cshtml");
            }
        }

        // GET: Capitulo/Details/5
        public ActionResult Detalhes(int? id)
        {
            var consultaCapitulo = db.Capitulo.Any(e => e.IdCapitulo == id);
            if (id == null || consultaCapitulo == false)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                var capitulos = db.Capitulo.Include(c => c.Historia);
                return View(capitulos.ToList().Find(c => c.IdCapitulo == id));
            }
        }

        // GET: Capitulo/Create
        public ActionResult Criar(int idHistoria)
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            var capituloViewModel = new CapituloViewModel();
            var consultaHistoria = db.Historia.Where(h => h.IdHistoria == idHistoria).FirstOrDefault();
            capituloViewModel.Situacao = consultaHistoria.Situacao;
            return View(capituloViewModel);
        }

        // POST: Capitulo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "IdCapitulo,IdHistoria,Titulo,Conteudo,Comentario")] Capitulo capitulo, CapituloViewModel capituloViewModel, int idHistoria)
        {
            var consultaHistoria = db.Historia.Where(h => h.IdHistoria == idHistoria).FirstOrDefault();
            if (ModelState.IsValid)
            {
                capitulo.IdHistoria = idHistoria;
                capitulo.Titulo = capituloViewModel.Titulo;
                capitulo.Conteudo = capituloViewModel.Conteudo;
                if(consultaHistoria.Situacao == true)
                {
                    capitulo.Situacao = capituloViewModel.Situacao;
                }
                else
                {
                    capitulo.Situacao = false;
                }
                capitulo.Status = "Ativado";
                db.Capitulo.Add(capitulo);
                db.SaveChanges();
                return RedirectToAction("MinhasHistorias", "Historia");
            }                       
            return View(capituloViewModel);
        }

        
        public ActionResult Editar(int id)
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Capitulo capitulo = db.Capitulo.Find(id);
            if (capitulo == null)
            {
                return HttpNotFound();
            }
            return View(capitulo);
        }

        // POST: Capitulo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Exclude = "Status")] Capitulo capitulo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int idHistoria = capitulo.IdHistoria;
                    db.Entry(capitulo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToRoutePermanent(new { controller = "Capitulo", action = "MeusCapitulos", idHistoria });
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível atualizar o capítulo.");
            }
            //int usuarioAutor = Convert.ToInt32(Session["IdUsuario"].ToString());
            //ViewBag.IdHistoria = new SelectList(db.Historia, "IdHistoria", "Titulo", capitulo.IdHistoria);
            return View(capitulo);
        }

        // GET: Capitulo/Delete/5
        public ActionResult Excluir(int? id)
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitulo capitulo = db.Capitulo.Find(id);
            if (capitulo == null)
            {
                return HttpNotFound();
            }
            return View(capitulo);
        }

        // POST: Capitulo/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(int id)
        {
            Capitulo capitulo = db.Capitulo.Find(id);
            Historia historia = db.Historia.Where(h => h.IdHistoria == capitulo.IdHistoria).FirstOrDefault();
            int idHistoria = historia.IdHistoria;
            db.Capitulo.Remove(capitulo);
            db.SaveChanges();
            return RedirectToRoutePermanent(new { controller = "Capitulo", action = "MeusCapitulos", idHistoria });
        }

        public ActionResult Publicar(int idCapitulo)
        {
            Capitulo capitulo = db.Capitulo.Find(idCapitulo);
            Historia historia = db.Historia.Where(h => h.IdHistoria == capitulo.IdHistoria).FirstOrDefault();
            if (historia.Situacao)
            {
                capitulo.Situacao = true;
                db.Entry(capitulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Capitulo", action = "MeusCapitulos", idHistoria = historia.IdHistoria });
            }
            else
            {
                ModelState.AddModelError("", "Não é possível publicar capítulo de história em rascunho.");
                int id;
                return RedirectToAction("Detalhes", id = idCapitulo);
            }
        }

        public ActionResult Retirar(int idCapitulo)
        {
            Capitulo capitulo = db.Capitulo.Find(idCapitulo);
            Historia historia = db.Historia.Where(h => h.IdHistoria == capitulo.IdHistoria).FirstOrDefault();
            capitulo.Situacao = false;
            db.SaveChanges();
            return RedirectToRoute(new { controller = "Capitulo", action = "MeusCapitulos", idHistoria = historia.IdHistoria });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ChangeCurrentCulture(int id)
        {
            Log.Info("Usuário alterou o idioma.");
            //  
            // Change the current culture for this user.  
            //  
            CultureHelper.CurrentCulture = id;
            //  
            // Cache the new current culture into the user HTTP session.   
            //  
            Session["CurrentCulture"] = id;
            //  
            // Redirect to the same page from where the request was made!   
            //  
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
