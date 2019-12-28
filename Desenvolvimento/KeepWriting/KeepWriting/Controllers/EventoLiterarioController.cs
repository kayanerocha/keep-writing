using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using log4net;
using log4net.Repository.Hierarchy;

namespace KeepWriting.Controllers
{
    public class EventoLiterarioController : BaseController
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        // GET: EventoLiterario
        public ActionResult Listar()
        {
            ViewBag.Titulo = "Eventos literários";

            var eventos = db.EventoLiterario.Include(e => e.Localizacao).Include(e => e.Usuario);
            Log.Info("Usuário ou visitante visualizou todos os eventos literários públicos.");
            return View(eventos.ToList().Where(e => e.Status == "Ativado"));
        }

        public ActionResult MeusEventos()
        {
            ViewBag.Titulo = "Meus eventos literários";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (GestaoElementos.VerificarAutenticacao())
            {
                var eventos = db.EventoLiterario.Include(e => e.Localizacao).Include(e => e.Usuario);
                Log.Info("Usuário de id " + usuarioLogado.IdUsuario + " visualizou seus eventos.");
                return View(eventos.ToList().Where(e => e.IdUsuario == usuarioLogado.IdUsuario));
            }
            else
            {
                Log.Info("Um visitante tentou visualizar a página de eventos próprios sem estar logado.");
                return Redirect("/Usuario/Autenticacao");
            }            
        }

        //GET: EventoLiterario/Details/5
        public ActionResult Detalhes(int? id)
        {
            ViewBag.Titulo = "Evento literário";

            EventoLiterario eventoLiterario = db.EventoLiterario.Find(id);
            if (id == null || eventoLiterario == null)
            {
                Log.Info("Usuário ou visitante tentou acessar página de detalhes de evento literário de id não existente.");
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                var eventos = db.EventoLiterario.Include(e => e.Localizacao).Include(e => e.Usuario);
                Log.Info("Usuário ou visitante acessou a página de detalhes de evento literário.");
                return View(eventos.ToList().Find(e => e.IdEvento == id && e.Status == "Ativado"));
            }
        }

        // GET: EventoLiterario/Create
        public ActionResult Criar()
        {
            var usuarioLogado = HttpContext.Session["usuario"];
            if (!GestaoElementos.VerificarAutenticacao())
            {
                return Redirect("/Usuario/Autenticacao");
            }
            EventoViewModel eventoViewModel = new EventoViewModel();
            return View(eventoViewModel);
        }

        // POST: EventoLiterario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "IdEvento,Nome,Descricao,DataInicio,DataFim,IdUsuario,IdLocalizacao")] EventoLiterario eventoLiterario, [Bind(Include = "IdLocalizacao,Estado,Cidade,Endereco,Local")] Localizacao localizacao, EventoViewModel eventoViewModel)
        {
            try
            {
                switch (GestaoElementos.SalvarImagem(eventoViewModel.ImagemUpload))
                {
                    case 0:
                        eventoLiterario.Imagem = null;
                        break;
                    case 1:
                        ModelState.AddModelError("ImagemUpload", "Escolha uma imagem GIF, JPG ou PNG.");
                        break;
                    case 2:
                        using (var binaryReader = new BinaryReader(eventoViewModel.ImagemUpload.InputStream))
                            eventoLiterario.Imagem = binaryReader.ReadBytes(eventoViewModel.ImagemUpload.ContentLength);
                        break;
                }

                if (eventoViewModel.DataFim.Date <= DateTime.Now)
                {
                    ModelState.AddModelError("DataFim", "É permitido somente o cadastro de evento que ainda não aconteceu.");
                }
                if (eventoViewModel.DataFim.Date < eventoViewModel.DataInicio.Date)
                {
                    ModelState.AddModelError("DataFim", "A data de fim deve ser igual ou posterior a data de início.");
                }

                if (ModelState.IsValid)
                {
                    int usuarioAutor = Convert.ToInt32(Session["IdUsuario"].ToString());
                    eventoLiterario.IdUsuario = usuarioAutor;
                    eventoLiterario.Nome = eventoViewModel.Nome;
                    eventoLiterario.Descricao = eventoViewModel.Descricao;
                    eventoLiterario.DataInicio = eventoViewModel.DataInicio;
                    eventoLiterario.DataFim = eventoViewModel.DataFim;
                    eventoLiterario.Link = eventoViewModel.Link;
                    eventoLiterario.Status = "Ativado";
                    localizacao.Estado = eventoViewModel.Estado;
                    localizacao.Cidade = eventoViewModel.Cidade;
                    localizacao.Endereco = eventoViewModel.Endereco;
                    localizacao.Local = eventoViewModel.Local;
                    db.EventoLiterario.Add(eventoLiterario);
                    db.Localizacao.Add(localizacao);
                    db.SaveChanges(); //Não está inserindo o IdLocalizacao
                    return RedirectToAction("MeusEventos");
                }
            }
            catch (Exception e)
            {
                Log.Error("Usuário não conseguiu criar evento literário");
                Logger.Error(e);
                ModelState.AddModelError("", "Nâo foi possível criar a história.");
            }
            return View(eventoViewModel);
        }

        // GET: EventoLiterario/Edit/5
        public ActionResult Editar(int? id)
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
            EventoLiterario eventoLiterario = db.EventoLiterario.Find(id);
            Localizacao localizacao = db.Localizacao.Find(id);
            if (!GestaoElementos.VerificarElemento(eventoLiterario.Status, eventoLiterario.IdUsuario))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            if (eventoLiterario == null && localizacao == null)
            {
                return HttpNotFound();
            }

            var eventoViewModel = new EventoViewModel();
            eventoViewModel.Nome = eventoLiterario.Nome;
            eventoViewModel.Descricao = eventoLiterario.Descricao;
            eventoViewModel.EditarDataInicio = eventoLiterario.DataInicio;
            eventoViewModel.EditarDataFim = eventoLiterario.DataFim;
            eventoViewModel.Link = eventoLiterario.Link;
            eventoViewModel.Imagem = eventoLiterario.Imagem;
            eventoViewModel.Estado = localizacao.Estado;
            eventoViewModel.Cidade = localizacao.Cidade;
            eventoViewModel.Endereco = localizacao.Endereco;
            eventoViewModel.Local = localizacao.Local;
            eventoViewModel.IdEvento = eventoLiterario.IdEvento;
            eventoViewModel.IdLocalizacao = eventoLiterario.IdLocalizacao;
            int usuario = Convert.ToInt32(Session["IdUsuario"].ToString());
            eventoViewModel.IdUsuario = usuario;
            return View(eventoViewModel);
        }

        // POST: EventoLiterario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IdEvento,Nome,Descricao,DataInicio,DataFim,IdUsuario,IdLocalizacao")] EventoLiterario eventoLiterario, [Bind(Include = "IdLocalizacao,Estado,Cidade,Endereco,Local")] Localizacao localizacao, EventoViewModel eventoViewModel)
        {
            var imageTypes = new string[]{
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
            if (eventoViewModel.ImagemUpload == null || eventoViewModel.ImagemUpload.ContentLength == 0)
            {
                eventoLiterario.Imagem = eventoViewModel.Imagem;
            }
            else if (eventoViewModel.ImagemUpload != null || eventoViewModel.ImagemUpload.ContentLength > 0)
            {
                if (!imageTypes.Contains(eventoViewModel.ImagemUpload.ContentType))
                {
                    ModelState.AddModelError("ImagemUpload", "Escolha uma imagem GIF, JPG ou PNG.");
                }
                //lemos a imagem e a seguir os bytes armazenados
                using (var binaryReader = new BinaryReader(eventoViewModel.ImagemUpload.InputStream))
                    eventoLiterario.Imagem = binaryReader.ReadBytes(eventoViewModel.ImagemUpload.ContentLength);
            }

            if (eventoViewModel.DataFim.Date <= DateTime.Now)
            {
                ModelState.AddModelError("DataFim", "É permitido somente o cadastro de evento que ainda não aconteceu.");
            }
            if (eventoViewModel.DataFim.Date < eventoViewModel.DataInicio.Date)
            {
                ModelState.AddModelError("DataFim", "A data de fim deve ser igual ou posterior a data de início.");
            }

            if (ModelState.IsValid)
            {
                int usuario = Convert.ToInt32(Session["IdUsuario"].ToString());
                eventoLiterario.IdUsuario = usuario;
                eventoLiterario.IdEvento = eventoViewModel.IdEvento;
                eventoLiterario.Nome = eventoViewModel.Nome;
                eventoLiterario.Descricao = eventoViewModel.Descricao;
                eventoLiterario.DataInicio = eventoViewModel.EditarDataInicio;
                eventoLiterario.DataFim = eventoViewModel.EditarDataFim;
                eventoLiterario.Link = eventoViewModel.Link;
                eventoLiterario.Status = "Ativado";
                localizacao.IdLocalizacao = eventoViewModel.IdLocalizacao;
                localizacao.Estado = eventoViewModel.Estado;
                localizacao.Cidade = eventoViewModel.Cidade;
                localizacao.Endereco = eventoViewModel.Endereco;
                localizacao.Local = eventoViewModel.Local;
                db.Entry(localizacao).State = EntityState.Modified;
                db.Entry(eventoLiterario).State = EntityState.Modified;
               
                db.SaveChanges();
                return RedirectToAction("MeusEventos");
            }
            return View(eventoViewModel);
        }

        // GET: EventoLiterario/C/5
        public ActionResult Excluir(int? id)
        {
            var usuarioLogado = HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }

            EventoLiterario eventoLiterario = db.EventoLiterario.Find(id);
            if (!GestaoElementos.VerificarElemento(eventoLiterario.Status, eventoLiterario.IdUsuario))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            if (id == null || eventoLiterario == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                List<EventoViewModel> eventoViewModel = new List<EventoViewModel>();
                var listaEventos = (from Evento in db.EventoLiterario
                                    join Local in db.Localizacao on Evento.IdLocalizacao equals Local.IdLocalizacao
                                    join User in db.Usuario on Evento.IdUsuario equals User.IdUsuario
                                    where Evento.Status == "Ativado"
                                    select new { Evento.IdEvento, Evento.Nome, Evento.Descricao, Evento.DataInicio, Evento.DataFim, Evento.Imagem, Evento.Link, User.IdUsuario, nomeUsuario = User.Nome, Local.IdLocalizacao, Local.Estado, Local.Cidade, Local.Endereco, Local.Local }).ToList();
                foreach (var item in listaEventos)
                {
                    EventoViewModel eventoVM = new EventoViewModel();
                    eventoVM.Nome = item.Nome;
                    eventoVM.Descricao = item.Descricao;
                    eventoVM.DataInicio = item.DataInicio;
                    eventoVM.DataFim = item.DataFim;
                    eventoVM.Imagem = item.Imagem;
                    eventoVM.Link = item.Link;
                    eventoVM.Usuario = item.nomeUsuario;
                    eventoVM.Estado = item.Estado;
                    eventoVM.Cidade = item.Cidade;
                    eventoVM.Endereco = item.Endereco;
                    eventoVM.Local = item.Local;
                    eventoVM.IdEvento = item.IdEvento;
                    eventoVM.IdLocalizacao = item.IdLocalizacao;
                    eventoVM.IdUsuario = item.IdUsuario;
                    eventoViewModel.Add(eventoVM);
                }
                return View(eventoViewModel.Find(x => x.IdEvento == id));
            }
        }

        // POST: EventoLiterario/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExclusao(int id)
        {
            EventoLiterario eventoLiterario = db.EventoLiterario.Find(id);
            Localizacao localizacao = db.Localizacao.Find(id);
            db.EventoLiterario.Remove(eventoLiterario);
            db.Localizacao.Remove(localizacao);
            db.SaveChanges();
            return RedirectToAction("MeusEventos");
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
