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
    public class CompeticaoController : BaseController
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();

        // GET: Competicao
        public ActionResult Listar()
        {
            ViewBag.Titulo = "Competições";

            List<CompeticaoViewModel> competicaoViewModel = new List<CompeticaoViewModel>();
            var listaCompeticoes = (from Compe in db.Competicao
                                  join User in db.Usuario on Compe.IdUsuario equals User.IdUsuario
                                  where Compe.Status == "Ativado"
                                  select new { Compe.IdCompeticao, Compe.Nome, Compe.Descricao, Compe.DataInicioInscri, Compe.DataFimInscri, Compe.DataInicioVota, Compe.DataFimVota, Compe.PontoExperiencia, User.IdUsuario, NomeUsuario = User.Nome }).ToList();
            foreach (var item in listaCompeticoes)
            {
                CompeticaoViewModel competicaoVM = new CompeticaoViewModel();
                competicaoVM.IdCompeticao = item.IdCompeticao;
                competicaoVM.Nome = item.Nome;
                competicaoVM.Descricao = item.Descricao;
                competicaoVM.DataInicioInscri = item.DataInicioInscri;
                competicaoVM.DataFimInscri = item.DataFimInscri;
                competicaoVM.DataInicioVota = item.DataInicioVota;
                competicaoVM.DataFimVota = item.DataFimVota;
                competicaoVM.PontoExperiencia = item.PontoExperiencia;
                competicaoVM.IdUsuario = item.IdUsuario;
                competicaoVM.NomeUsuario = item.NomeUsuario;
                competicaoViewModel.Add(competicaoVM);
            }
            return View(competicaoViewModel);
        }

        public ActionResult MinhasCompeticoes()
        {
            ViewBag.Titulo = "Minhas competições";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            if (usuarioLogado.Status == "Ativado")
            {
                List<CompeticaoViewModel> competicaoViewModel = new List<CompeticaoViewModel>();
                var listaCompeticoes = (from Compe in db.Competicao
                                        join User in db.Usuario on Compe.IdUsuario equals User.IdUsuario
                                        where Compe.IdUsuario == usuarioLogado.IdUsuario
                                        select new { Compe.IdCompeticao, Compe.Nome, Compe.Descricao, Compe.DataInicioInscri, Compe.DataFimInscri, Compe.DataInicioVota, Compe.DataFimVota, Compe.PontoExperiencia, Compe.Status, User.IdUsuario, NomeUsuario = User.Nome }).ToList();
                foreach (var item in listaCompeticoes)
                {
                    CompeticaoViewModel competicaoVM = new CompeticaoViewModel();
                    competicaoVM.IdCompeticao = item.IdCompeticao;
                    competicaoVM.Nome = item.Nome;
                    competicaoVM.Descricao = item.Descricao;
                    competicaoVM.DataInicioInscri = item.DataInicioInscri;
                    competicaoVM.DataFimInscri = item.DataFimInscri;
                    competicaoVM.DataInicioVota = item.DataInicioVota;
                    competicaoVM.DataFimVota = item.DataFimVota;
                    competicaoVM.PontoExperiencia = item.PontoExperiencia;
                    competicaoVM.Status = item.Status;
                    competicaoVM.IdUsuario = item.IdUsuario;
                    competicaoVM.NomeUsuario = item.NomeUsuario;
                    competicaoViewModel.Add(competicaoVM);
                }
                return View(competicaoViewModel);
            }
            else
            {
                return View("~/Views/Usuario/Status.cshtml");
            }
        }

        // GET: Competicao/Details/5
        public ActionResult Detalhes(int? id)
        {
            ViewBag.Titulo = "Competição";

            var consultaCompeticao = db.Competicao.Any(e => e.IdCompeticao == id);
            if (id == null || consultaCompeticao == false)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                List<CompeticaoViewModel> competicaoViewModel = new List<CompeticaoViewModel>();
                var listaCompeticoes = (from Compe in db.Competicao
                                        join User in db.Usuario on Compe.IdUsuario equals User.IdUsuario
                                        select new { Compe.IdCompeticao, Compe.Nome, Compe.Descricao, Compe.DataInicioInscri, Compe.DataFimInscri, Compe.DataInicioVota, Compe.DataFimVota, Compe.PontoExperiencia, Compe.Status, User.IdUsuario, NomeUsuario = User.Nome }).ToList();
                foreach (var item in listaCompeticoes)
                {
                    CompeticaoViewModel competicaoVM = new CompeticaoViewModel();
                    competicaoVM.IdCompeticao = item.IdCompeticao;
                    competicaoVM.Nome = item.Nome;
                    competicaoVM.Descricao = item.Descricao;
                    competicaoVM.DataInicioInscri = item.DataInicioInscri;
                    competicaoVM.DataFimInscri = item.DataFimInscri;
                    competicaoVM.DataInicioVota = item.DataInicioVota;
                    competicaoVM.DataFimVota = item.DataFimVota;
                    competicaoVM.PontoExperiencia = item.PontoExperiencia;
                    competicaoVM.Status = item.Status;
                    competicaoVM.IdUsuario = item.IdUsuario;
                    competicaoVM.NomeUsuario = item.NomeUsuario;
                    competicaoViewModel.Add(competicaoVM);
                }
                return View(competicaoViewModel.Find(x => x.IdCompeticao == id));
            }
        }

        // GET: Competicao/Create
        public ActionResult Criar()
        {
            ViewBag.Titulo = "Criar competição";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            var competicaoViewModel = new CompeticaoViewModel();
            return View(competicaoViewModel);
        }

        // POST: Competicao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "IdCompeticao,Nome,Descricao,DataInicio,DataFim,IdUsuario")] Competicao competicao, CompeticaoViewModel competicaoViewModel)
        {
            if(competicaoViewModel.DataInicioInscri <= DateTime.Now)
            {
                ModelState.AddModelError("DataInicioInscri", "As inscrições podem iniciar somente a partir de amanhã a meia-noite.");
            }
            if (competicaoViewModel.DataFimInscri.Date <= competicaoViewModel.DataInicioInscri.Date)
            {
                ModelState.AddModelError("DataFimInscri", "Data inválida, a competição deve possuir um período para inscrição.");
            }
            if (competicaoViewModel.DataInicioVota.Date <= competicaoViewModel.DataFimInscri.Date)
            {
                ModelState.AddModelError("DataInicioVota", "O período de votação deve iniciar somente após o período de inscrições.");
            }
            if (competicaoViewModel.DataFimVota.Date <= competicaoViewModel.DataInicioVota.Date)
            {
                ModelState.AddModelError("DataFimVota", "Data inválida, a competição deve possuir um período para votação.");
            }
            if (ModelState.IsValid)
            {
                var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
                if(usuarioLogado.Email == "keepwritingpds@gmail.com")
                {
                    competicao.PontoExperiencia = 250;
                }
                else
                {
                    competicao.PontoExperiencia = 200;
                }
                competicao.Nome = competicaoViewModel.Nome;
                competicao.Descricao = competicaoViewModel.Descricao;
                competicao.DataInicioInscri = competicaoViewModel.DataInicioInscri;
                competicao.DataFimInscri = competicaoViewModel.DataFimInscri;
                competicao.DataInicioVota = competicaoViewModel.DataInicioVota;
                competicao.DataFimVota = competicaoViewModel.DataFimVota;
                competicao.Status = "Ativado";
                int usuarioAutor = Convert.ToInt32(Session["IdUsuario"].ToString());
                competicao.IdUsuario = usuarioAutor;
                db.Competicao.Add(competicao);
                db.SaveChanges();
                return RedirectToAction("MinhasCompeticoes");
            }
            return View(competicaoViewModel);
        }

        // GET: Competicao/Edit/5
        public ActionResult Editar(int? id)
        {
            ViewBag.Titulo = "Editar competição";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competicao competicao = db.Competicao.Find(id);
            if (competicao == null)
            {
                return HttpNotFound();
            }
            var editarCompeticao = new EditarCompeticao();
            editarCompeticao.IdCompeticao = competicao.IdCompeticao;
            editarCompeticao.Nome = competicao.Nome;
            editarCompeticao.Descricao = competicao.Descricao;
            editarCompeticao.DataInicioInscri = competicao.DataInicioInscri;
            editarCompeticao.DataFimInscri = competicao.DataFimInscri;
            editarCompeticao.DataInicioVota = competicao.DataInicioVota;
            editarCompeticao.DataFimVota = competicao.DataFimVota;
            editarCompeticao.PontoExperiencia = competicao.PontoExperiencia;
            editarCompeticao.IdUsuario = competicao.IdUsuario;
            return View(editarCompeticao);
        }

        // POST: Competicao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IdCompeticao,Nome,Descricao,DataInicio,DataFim,IdUsuario")] Competicao competicao, EditarCompeticao editarCompeticao, int id)
        {
            Competicao verifCompeticao = db.Competicao.Find(id);
            int comp = db.Competicao.AsNoTracking().Where(c => c == verifCompeticao).Count();
            if (editarCompeticao.DataFimInscri.Date <= verifCompeticao.DataInicioInscri.Date)
            {
                ModelState.AddModelError("DataFimInscri", "Data inválida, a competição deve possuir um período para inscrição.");
            }
            if(verifCompeticao.DataInicioVota.Date <= editarCompeticao.DataFimInscri.Date)
            {
                ModelState.AddModelError("DataFimInscri", "A data ultrapassa o período de votação.");
            }
            if (editarCompeticao.DataFimVota.Date <= verifCompeticao.DataInicioVota.Date)
            {
                ModelState.AddModelError("DataFimVota", "Data inválida, a competição deve possuir um período para votação.");
            }
            if (ModelState.IsValid)
            {
                if (comp > 0)
                {
                    int usuarioAutor = Convert.ToInt32(Session["IdUsuario"].ToString());
                    competicao.IdUsuario = usuarioAutor;
                    competicao.IdCompeticao = editarCompeticao.IdCompeticao;
                    competicao.Nome = editarCompeticao.Nome;
                    competicao.Descricao = editarCompeticao.Descricao;
                    competicao.DataInicioInscri = editarCompeticao.DataInicioInscri;
                    competicao.DataFimInscri = editarCompeticao.DataFimInscri;
                    competicao.DataInicioVota = editarCompeticao.DataInicioVota;
                    competicao.DataFimVota = editarCompeticao.DataFimVota;
                    competicao.Status = "Ativado";
                    db.Entry(competicao).State = EntityState.Modified;
                    db.Entry(competicao).Property(p => p.PontoExperiencia).IsModified = false;
                    db.SaveChanges();
                    return RedirectToAction("MinhasCompeticoes", "Competicao");
                }
            }
            return View(editarCompeticao);
        }

        // GET: Competicao/Delete/5
        public ActionResult Excluir(int? id)
        {
            ViewBag.Titulo = "Excluir competição";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            var consultaCompeticao = db.Competicao.Any(e => e.IdCompeticao == id);
            if (id == null || consultaCompeticao == false)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                List<CompeticaoViewModel> competicaoViewModel = new List<CompeticaoViewModel>();
                var listaCompeticoes = (from Compe in db.Competicao
                                        join User in db.Usuario on Compe.IdUsuario equals User.IdUsuario
                                        select new { Compe.IdCompeticao, Compe.Nome, Compe.Descricao, Compe.DataInicioInscri, Compe.DataFimInscri, Compe.DataInicioVota, Compe.DataFimVota, Compe.PontoExperiencia, Compe.Status, User.IdUsuario, NomeUsuario = User.Nome }).ToList();
                foreach (var item in listaCompeticoes)
                {
                    CompeticaoViewModel competicaoVM = new CompeticaoViewModel();
                    competicaoVM.IdCompeticao = item.IdCompeticao;
                    competicaoVM.Nome = item.Nome;
                    competicaoVM.DataInicioInscri = item.DataInicioInscri;
                    competicaoVM.DataFimInscri = item.DataFimInscri;
                    competicaoVM.DataInicioVota = item.DataInicioVota;
                    competicaoVM.DataFimVota = item.DataFimVota;
                    competicaoVM.PontoExperiencia = item.PontoExperiencia;
                    competicaoVM.Status = item.Status;
                    competicaoVM.IdUsuario = item.IdUsuario;
                    competicaoVM.NomeUsuario = item.NomeUsuario;
                    competicaoViewModel.Add(competicaoVM);
                }
                return View(competicaoViewModel.Find(x => x.IdCompeticao == id));
            }
        }

        // POST: Competicao/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(int id)
        {
            Competicao competicao = db.Competicao.Find(id);
            db.Competicao.Remove(competicao);
            db.SaveChanges();
            return RedirectToAction("MinhasCompeticoes");
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
