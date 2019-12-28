using KeepWriting.Models;
using KeepWriting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KeepWriting.Controllers
{
    public class PesquisaController : Controller
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();
        // GET: Pesquisa
        public ActionResult Buscar()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Buscar(ResultadoPesquisa resultadoPesquisa)
        //{
            //var historias = from h in db.Historia
            //                where h.Titulo.Contains(resultadoPesquisa.Titulo)
            //                select new ResultadoPesquisa
            //                {
            //                    Titulo = h.Titulo,
            //                    Capa = h.Capa,
            //                    Autor = h.Usuario.Nome
            //                };
            //if (historias != null)
            //    return View(historias.ToList());
            //else
            //{
            //    ViewBag.Mensagem = "Nenhum resultado encontrado.";
            //    return View();
            //}

        //    var historias = db.Historia.Where(h => h.Titulo.Contains(resultadoPesquisa.Titulo));
        //    return View(historias.ToList());
        //}
    }
}