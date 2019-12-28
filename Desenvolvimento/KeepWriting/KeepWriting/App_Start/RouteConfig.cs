using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KeepWriting
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DetalhesHistoria",
                url: "Historia/Detalhes/{id}",
                defaults: new { controller = "Historia", action = "Detalhes" }
            );

            routes.MapRoute(
                name: "FavoritarHistoria",
                url: "Favorito/Favoritar/{idHistoria}",
                defaults: new { controller = "Favorito", action = "Favoritar" }
            );

            routes.MapRoute(
                name: "AlterarCapa",
                url: "Historia/AlterarCapa/{idHistoria}",
                defaults: new { controller = "Historia", action = "AlterarCapa" }
            );

            routes.MapRoute(
                name: "RetirarCapitulo",
                url: "Capitulo/Retirar/{idCapitulo}",
                defaults: new { controller = "Capitulo", action = "Retirar" }
            );

            routes.MapRoute(
                name: "PublicarCapitulo",
                url: "Capitulo/Publicar/{idCapitulo}",
                defaults: new { controller = "Capitulo", action = "Publicar" }
            );

            routes.MapRoute(
                name: "PublicarHistoria",
                url: "Historia/Publicar/{idHistoria}",
                defaults: new { controller = "Historia", action = "Publicar" }
            );

            routes.MapRoute(
                name: "RetirarHistoria",
                url: "Historia/Retirar/{idHistoria}",
                defaults: new { controller = "Historia", action = "Retirar" }
            );

            routes.MapRoute(
                name: "DetalhesCapitulo",
                url: "Capitulo/Detalhes/{id}",
                defaults: new { controller = "Capitulo", action = "Detalhes" }
            );

            routes.MapRoute(
                name: "ExcluirCapitulo",
                url: "Capitulo/Excluir/{id}",
                defaults: new { controller = "Capitulo", action = "Excluir" }
            );

            routes.MapRoute(
                name: "EditarCapitulo",
                url: "Capitulo/Editar/{id}",
                defaults: new { controller = "Capitulo", action = "Editar" }
            );

            routes.MapRoute(
                name: "MeusCapitulos",
                url: "Capitulo/{action}/{idHistoria}/{id}",
                defaults: new { controller = "Capitulo", action = "MeusCapitulos", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Capitulos",
                url: "Capitulo/{action}/{idHistoria}/{id}",
                defaults: new { controller = "Capitulo", action = "Listar", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ListarCapitulos",
                url: "Capitulo/{action}/{idHistoria}/{id}",
                defaults: new { controller = "Capitulo", action = "Listar", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CancelarInscricao",
                url: "HistoriaXCompeticao/Cancelar/{idCompeticao}/{idHistoria}",
                defaults: new { controller = "HistoriaXCompeticao", action = "Cancelar" }
            );

            routes.MapRoute(
                name: "EscolherHistoria",
                url: "HistoriaXCompeticao/{action}/{idCompeticao}/{idHistoria}/{id}",
                defaults: new { controller = "HistoriaXCompeticao", action = "InscreverHistoria", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "VotarCompeticao",
                url: "HistoriaXCompeticao/Votar/{idCompeticao}/{idHistoria}",
                defaults: new { controller = "HistoriaXCompeticao", action = "Votar" }
            );

            routes.MapRoute(
                name: "Inscrever",
                url: "HistoriaXCompeticao/Inscrever/{idCompeticao}/{id}",
                defaults: new { controller = "HistoriaXCompeticao", action = "Inscrever", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HistoriasInscritas",
                url: "HistoriaXCompeticao/{action}/{idCompeticao}/{id}",
                defaults: new { controller = "HistoriaXCompeticao", action = "HistoriasInscritas", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CriarCapitulo",
                url: "Capitulo/{action}/{idHistoria}/{id}",
                defaults: new {controller = "Capitulo", action = "Criar", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuario", action = "Autenticacao", id = UrlParameter.Optional }
            );
        }
    }
}
