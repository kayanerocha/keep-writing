using KeepWriting.Models;
using KeepWriting.Repositorios;
using KeepWriting.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeepWriting.Controllers
{
    public class HistoriaXCompeticaoController : Controller
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();
        private InscricaoRepositorio InscricaoRepositorio;

        // GET: HistoriaXCompeticao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inscrever(int idCompeticao)
        {
            ViewBag.Titulo = "Inscrever história";

            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            List<HistoriaXCompeticaoVM> historiaXCompeticao = new List<HistoriaXCompeticaoVM>();
            var historiasPublicadas = (from Hist in db.Historia
                                       where Hist.IdUsuario == usuarioLogado.IdUsuario && Hist.Situacao == true && Hist.Status == "Ativado"
                                       select new { Hist.IdHistoria, Hist.Titulo }).ToList();
            foreach (var item in historiasPublicadas)
            {
                HistoriaXCompeticaoVM historiaXCompeticaoVM = new HistoriaXCompeticaoVM();
                historiaXCompeticaoVM.IdHistoria = item.IdHistoria;
                historiaXCompeticaoVM.IdCompeticao = idCompeticao;
                historiaXCompeticaoVM.Titulo = item.Titulo;
                historiaXCompeticao.Add(historiaXCompeticaoVM);
            }
            return View(historiaXCompeticao);
        }

        
        public ActionResult InscreverHistoria(Inscricao inscricao, HistoriaXCompeticaoVM historiaXCompeticaoVM, int idCompeticao, int idHistoria)
        {
            var consultaInscricao = db.Inscricao.Where(h => h.IdCompeticao == idCompeticao && h.IdHistoria == idHistoria).FirstOrDefault();
            if (consultaInscricao != null)
            {
                ViewBag.MensagemErro = "Esta história selecionada já está inscrita na competição escolhida.";
                return RedirectToRoute(new { controller = "HistoriaXCompeticao", action = "Inscrever", historiaXCompeticaoVM.IdCompeticao });
            }
            else
                inscricao.IdCompeticao = idCompeticao;
            {
                if (ModelState.IsValid)
                {
                    inscricao.IdHistoria = idHistoria;
                    db.Inscricao.Add(inscricao);
                    db.SaveChanges();
                    return RedirectToRoute(new { controller = "HistoriaXCompeticao", action = "HistoriasInscritas", idCompeticao });
                }
            }
            ViewBag.MensagemErro = "Esta história selecionada já está inscrita na competição escolhida.";
            return RedirectToRoute(new { controller = "HistoriaXCompeticao", action = "Inscrever", historiaXCompeticaoVM.IdCompeticao, id = historiaXCompeticaoVM.IdHistoria });
        }

        public ActionResult HistoriasInscritas(int idCompeticao)
        {
            ViewBag.Titulo = "Histórias inscritas";
            var historiasInscritas = db.Inscricao.Include(i => i.Historia).Include(i => i.Competicao);
            return View(historiasInscritas.ToList().Where(h => h.IdCompeticao == idCompeticao));
        }

        public ActionResult Cancelar(int idCompeticao, int idHistoria)
        {
            Inscricao inscricao = db.Inscricao.Find(idCompeticao, idHistoria);
            Voto voto = db.Voto.Where(v => v.IdInscricaoComp == idCompeticao && v.IdInscricaoHist == idHistoria).FirstOrDefault();
            db.Inscricao.Remove(inscricao);
            db.SaveChanges();
            return RedirectToRoute(new { controller = "HistoriaXCompeticao", action = "HistoriasInscritas", idCompeticao });
        }

        public ActionResult Votar(Voto voto, int idCompeticao, int idHistoria)
        {
            Inscricao inscricao = db.Inscricao.Find(idCompeticao, idHistoria);
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (InscricaoRepositorio.VerificarVoto(idCompeticao, usuarioLogado.IdUsuario))
            {
                ModelState.AddModelError("", "Você já votou nesta competição");
                ViewBag.Mensagem = "Você já votou nesta competição";
                return RedirectToRoute(new { controller = "HistoriaXCompeticao", action = "HistoriasInscritas", idCompeticao });
            }
            else
            {
                voto.IdInscricaoComp = idCompeticao;
                voto.IdInscricaoHist = idHistoria;
                voto.IdUsuario = usuarioLogado.IdUsuario;
                inscricao.QtdVotos = inscricao.QtdVotos + 1;
                db.Voto.Add(voto);
                db.Entry(inscricao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToRoute(new { controller = "HistoriaXCompeticao", action = "HistoriasInscritas", idCompeticao });
            }
        }
    }
}