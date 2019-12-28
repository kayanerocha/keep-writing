using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KeepWriting.Helper;
using KeepWriting.Models;
using KeepWriting.Repositorios;
using KeepWriting.Utilities;
using KeepWriting.ViewModels;

namespace KeepWriting.Controllers
{
    public class HistoriaController : BaseController
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();

        // GET: Historias
        public ActionResult Listar()
        {
            ViewBag.Titulo = "Histórias";
            var historias = db.Historia.Include(h => h.Genero).Include(h => h.Idioma).Include(h => h.Usuario);
            return View(historias.ToList().Where(h => h.Status == "Ativado" && h.Situacao == true));
        }

        public ActionResult MinhasHistorias()
        {
            ViewBag.Titulo = "Minhas histórias";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            if (usuarioLogado.Status == "Ativado")
            {
                var minhasHistorias = db.Historia.Include(h => h.Genero).Include(h => h.Idioma).Include(h => h.Usuario);
                return View(minhasHistorias.ToList().Where(h => h.IdUsuario == usuarioLogado.IdUsuario));
            }
            else
            {
                return View("~/Views/Usuario/Status.cshtml");
            }
        }

        // GET: Historias/Details/5
        public ActionResult Detalhes(int? id)
        {
            ViewBag.Titulo = "História";


            var consultaHistoria = db.Historia.Any(e => e.IdHistoria == id);
            if (id == null || consultaHistoria == false)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                var historia = db.Historia.Include(h => h.Genero).Include(h => h.Idioma).Include(h => h.Usuario);
                return View(historia.ToList().Find(h => h.IdHistoria == id));
            }
        }

        public ActionResult DetalhesPrivados(int? id)
        {
            ViewBag.Titulo = "História";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }

            var consultaHistoria = db.Historia.Any(e => e.IdHistoria == id);
            if (id == null || consultaHistoria == false)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else if (usuarioLogado.Status == "Ativado")
            {
                var historia = db.Historia.Include(h => h.Genero).Include(h => h.Idioma).Include(h => h.Usuario);
                return View(historia.ToList().Find(h => h.IdHistoria == id));
            }
            else
            {
                return View("~/Views/Usuario/Status.cshtml");
            }
        }

        // GET: Historias/Create
        public ActionResult Criar()
        {
            ViewBag.Titulo = "Criar história";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            ViewBag.IdGeneroPortugues = new SelectList(db.Genero, "IdGenero", "NomePortugues");
            ViewBag.IdGeneroIngles = new SelectList(db.Genero, "IdGenero", "NomeIngles");
            ViewBag.IdIdioma = new SelectList(db.Idioma, "IdIdioma", "Descricao");
            
            var classificacoesPortugues = new List<Classificacao> { new Classificacao { Id = 1, Descricao =  "Livre"}, new Classificacao { Id = 2, Descricao = "Para maiores de 16 anos" }, new Classificacao {Id = 3, Descricao = "Para maiores de 18 anos" } };
            var classificacoesIngles = new List<Classificacao> { new Classificacao { Id = 1, Descricao = "Free" }, new Classificacao { Id = 2, Descricao = "For over 16 years" }, new Classificacao { Id = 3, Descricao = "For over 18 years" } };
            ViewBag.ClassificacoesPortugues = classificacoesPortugues;
            ViewBag.ClassificacoesIngles = classificacoesIngles;

            var historiaViewModel = new HistoriaViewModel();
            return View(historiaViewModel);
        }

        // POST: Historias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "IdHistoria,IdGeneroPortugues,IdUsuario,Titulo,Sinopse,Classificacao,QtdVotos,Capa,Situacao,IdGeneroIngles,IdIdioma")] Historia historia, HistoriaViewModel historiaViewModel)
        {
            switch (GestaoElementos.SalvarImagem(historiaViewModel.ImagemUpload))
            {
                case 0:
                    historia.Capa = null;
                    break;
                case 1:
                    ModelState.AddModelError("ImagemUpload", "Escolha uma imagem GIF, JPG ou PNG.");
                    break;
                case 2:
                    using (var binaryReader = new BinaryReader(historiaViewModel.ImagemUpload.InputStream))
                        historia.Capa = binaryReader.ReadBytes(historiaViewModel.ImagemUpload.ContentLength);
                    break;
            }

            if (ModelState.IsValid)
            {
                int culture = Convert.ToInt32(Session["CurrentCulture"].ToString());
                var classificacao = 0;
                if (culture == 0)
                {
                    classificacao = historiaViewModel.ClassificacaoPortugues;
                    historia.IdGenero = historiaViewModel.IdGeneroPortugues;
                }
                else
                {
                    classificacao = historiaViewModel.ClassificacaoIngles;
                    historia.IdGenero = historiaViewModel.IdGeneroIngles;
                }
                switch (classificacao)
                {
                    case 1:
                        historia.Classificacao = "Livre";
                        break;
                    case 2:
                        historia.Classificacao = "Para maiores de 16 anos";
                        break;
                    case 3:
                        historia.Classificacao = "Para maiores de 18 anos";
                        break;
                }
                historia.Titulo = historiaViewModel.Titulo;
                historia.Sinopse = historiaViewModel.Sinopse;                
                historia.Situacao = false;
                historia.IdIdioma = historiaViewModel.IdIdioma;
                historia.Status = "Ativado";
                historia.QtdFavoritos = 0;
                historia.QtdVisualizacoes = 0;
                int usuarioAutor = Convert.ToInt32(Session["IdUsuario"].ToString());
                historia.IdUsuario = usuarioAutor;
                db.Historia.Add(historia);
                db.SaveChanges();
                return RedirectToAction("MinhasHistorias");
            }

            ViewBag.IdGeneroPortugues = new SelectList(db.Genero, "IdGenero", "NomePortugues", historia.IdGenero);
            ViewBag.IdGeneroIngles = new SelectList(db.Genero, "IdGeneroIngles", "NomeIngles", historia.IdGenero);
            ViewBag.IdIdioma = new SelectList(db.Idioma, "IdIdioma", "Descricao", historia.IdIdioma);

            var classificacoesPortugues = new List<Classificacao> { new Classificacao { Id = 1, Descricao = "Livre" }, new Classificacao { Id = 2, Descricao = "Para maiores de 16 anos" }, new Classificacao { Id = 3, Descricao = "Para maiores de 18 anos" } };
            var classificacoesIngles = new List<Classificacao> { new Classificacao { Id = 1, Descricao = "Free" }, new Classificacao { Id = 2, Descricao = "For over 16 years" }, new Classificacao { Id = 3, Descricao = "For over 18 years" } };
            ViewBag.ClassificacoesPortugues = classificacoesPortugues;
            ViewBag.ClassificacoesIngles = classificacoesIngles;
            return View(historiaViewModel);
        }

        // GET: Historias/Edit/5
        public ActionResult Editar(int? id)
        {
            ViewBag.Titulo = "Editar história";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historia historia = db.Historia.Find(id);
            if (historia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGeneroIngles = new SelectList(db.Genero, "IdGenero", "NomeIngles", historia.IdGenero);
            ViewBag.IdGeneroPortugues = new SelectList(db.Genero, "IdGenero", "NomePortugues", historia.IdGenero);
            ViewBag.IdIdioma = new SelectList(db.Idioma, "IdIdioma", "Descricao", historia.IdIdioma);

            var classificacoesPortugues = new List<Classificacao> { new Classificacao { Id = 1, Descricao = "Livre" }, new Classificacao { Id = 2, Descricao = "Para maiores de 16 anos" }, new Classificacao { Id = 3, Descricao = "Para maiores de 18 anos" } };
            var classificacoesIngles = new List<Classificacao> { new Classificacao { Id = 1, Descricao = "Free" }, new Classificacao { Id = 2, Descricao = "For over 16 years" }, new Classificacao { Id = 3, Descricao = "For over 18 years" } };
            ViewBag.ClassificacoesPortugues = classificacoesPortugues;
            ViewBag.ClassificacoesIngles = classificacoesIngles;

            var editarHistoria = new EditarHistoria();
            editarHistoria.IdHistoria = historia.IdHistoria;
            editarHistoria.Titulo = historia.Titulo;
            editarHistoria.Sinopse = historia.Sinopse;
            editarHistoria.Situacao = historia.Situacao;
            editarHistoria.IdIdioma = historia.IdIdioma;
            int usuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            editarHistoria.IdUsuario = usuario;
            editarHistoria.IdGeneroPortugues = historia.IdGenero;
            editarHistoria.Capa = historia.Capa;
            return View(editarHistoria);
        }

        // POST: Historias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Titulo,Sinopse,Classificacao,QtdVotos,Capa")] Historia historia, EditarHistoria editarHistoria)
        {
            var imageTypes = new string[]{
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
            if (editarHistoria.ImagemUpload == null || editarHistoria.ImagemUpload.ContentLength == 0)
            {
                historia.Capa = editarHistoria.Capa;
            }
            else if (editarHistoria.ImagemUpload != null || editarHistoria.ImagemUpload.ContentLength > 0)
            {
                if (!imageTypes.Contains(editarHistoria.ImagemUpload.ContentType))
                {
                    ModelState.AddModelError("ImagemUpload", "Escolha uma imagem GIF, JPG ou PNG.");
                }
                //lemos a imagem e a seguir os bytes armazenados
                using (var binaryReader = new BinaryReader(editarHistoria.ImagemUpload.InputStream))
                    historia.Capa = binaryReader.ReadBytes(editarHistoria.ImagemUpload.ContentLength);
            }

            if (ModelState.IsValid)
            {
                int culture = Convert.ToInt32(Session["CurrentCulture"].ToString());
                var classificacao = 0;
                if (culture == 0)
                {
                    classificacao = editarHistoria.ClassificacaoPortugues;
                    historia.IdGenero = editarHistoria.IdGeneroPortugues;
                }
                else
                {
                    classificacao = editarHistoria.ClassificacaoIngles;
                    historia.IdGenero = editarHistoria.IdGeneroIngles;
                }
                switch (classificacao)
                {
                    case 1:
                        historia.Classificacao = "Livre";
                        break;
                    case 2:
                        historia.Classificacao = "Para maiores de 16 anos";
                        break;
                    case 3:
                        historia.Classificacao = "Para maiores de 18 anos";
                        break;
                }

                historia.IdHistoria = editarHistoria.IdHistoria;
                historia.Titulo = editarHistoria.Titulo;
                historia.Sinopse = editarHistoria.Sinopse;
                historia.Situacao = editarHistoria.Situacao;
                historia.IdIdioma = editarHistoria.IdIdioma;
                historia.Status = "Ativado";
                int usuario = Convert.ToInt32(Session["IdUsuario"].ToString());
                historia.IdUsuario = usuario;
                db.Entry(historia).State = EntityState.Modified;
                db.Entry(historia).Property(p => p.QtdFavoritos).IsModified = false;
                db.Entry(historia).Property(p => p.QtdVisualizacoes).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("MinhasHistorias");
            }

            ViewBag.IdGeneroIngles = new SelectList(db.Genero, "IdGenero", "NomeIngles", historia.IdGenero);
            ViewBag.IdGeneroPortugues = new SelectList(db.Genero, "IdGenero", "NomePortugues", historia.IdGenero);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nome", historia.IdUsuario);
            return View(editarHistoria);
        }

        // GET: Historias/Delete/5
        public ActionResult Excluir(int? id)
        {
            ViewBag.Titulo = "Excluir história";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            var consultaHistoria = db.Historia.Any(e => e.IdHistoria == id);
            if (id == null || consultaHistoria == false)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                var historia = db.Historia.Include(h => h.Genero).Include(h => h.Idioma).Include(h => h.Usuario);
                return View(historia.ToList().Find(h => h.IdHistoria == id));
            }
        }

        // POST: Historias/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(int id)
        {
            Historia historia = db.Historia.Find(id);
            Capitulo capitulo = db.Capitulo.Where(c => c.IdHistoria == id).FirstOrDefault();
            if(capitulo != null)
            {
                db.Capitulo.Remove(capitulo);
            }
            db.Historia.Remove(historia);
            db.SaveChanges();
            return RedirectToAction("MinhasHistorias");
        }

        public ActionResult Publicar(int idHistoria)
        {
            Historia historia = db.Historia.Find(idHistoria);
            historia.Situacao = true;
            db.Entry(historia).State = EntityState.Modified;
            db.Entry(historia).Property(p => p.Capa).IsModified = false;
            db.Entry(historia).Property(p => p.QtdFavoritos).IsModified = false;
            db.Entry(historia).Property(p => p.QtdVisualizacoes).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("MinhasHistorias");
        }

        public ActionResult Retirar(int idHistoria)
        {
            Historia historia = db.Historia.Find(idHistoria);
            Capitulo capitulo = db.Capitulo.Where(c => c.IdHistoria == idHistoria).FirstOrDefault();
            Inscricao inscricao = db.Inscricao.Where(i => i.IdHistoria == idHistoria).FirstOrDefault();
            Voto voto = db.Voto.Where(v => v.IdInscricaoHist == idHistoria).FirstOrDefault();
            historia.Situacao = false;
            if(capitulo != null)
            {
                capitulo.Situacao = false;
                db.Entry(capitulo).State = EntityState.Modified;
            }
            if(inscricao != null)
                db.Inscricao.Remove(inscricao);
            if(voto != null)
                db.Voto.Remove(voto);
            db.Entry(historia).State = EntityState.Modified;
            db.Entry(historia).Property(p => p.Capa).IsModified = false;
            db.Entry(historia).Property(p => p.QtdFavoritos).IsModified = false;
            db.Entry(historia).Property(p => p.QtdVisualizacoes).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("MinhasHistorias");
        }

        public ActionResult AlterarCapa(int idHistoria)
        {
            Historia historia = db.Historia.Find(idHistoria);
            var historiaViewModel = new HistoriaViewModel();
            return View(historiaViewModel);
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
            // Change the current culture for this user.  
            CultureHelper.CurrentCulture = id;
            // Cache the new current culture into the user HTTP session.   
            Session["CurrentCulture"] = id;
            // Redirect to the same page from where the request was made!   
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
