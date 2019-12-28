using KeepWriting.LocalResource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace KeepWriting.Models
{
    public class Userdetails
    {
        //Geral

        [Display(Name = "editar", ResourceType = typeof(Resource))]
        public string editar { get; set; }

        [Display(Name = "detalhes", ResourceType = typeof(Resource))]
        public string detalhes { get; set; }

        [Display(Name = "excluir", ResourceType = typeof(Resource))]
        public string excluir { get; set; }

        //

        //História e evento

        [Display(Name = "publicado", ResourceType = typeof(Resource))]
        public string Publicado { get; set; }

        [Display(Name = "rascunho", ResourceType = typeof(Resource))]
        public string Rascunho { get; set; }

        [Display(Name = "situacaoCriar", ResourceType = typeof(Resource))]
        public string SituacaoCriar { get; set; }

        [Display(Name = "situacaoListar", ResourceType = typeof(Resource))]
        public string SituacaoListar { get; set; }

        //

        //Competição

        [Display(Name = "inicioInscriListar", ResourceType = typeof(Resource))]
        public string InicioInscriListar { get; set; }

        [Display(Name = "fimInscriListar", ResourceType = typeof(Resource))]
        public string FimInscriListar { get; set; }

        [Display(Name = "inicioVotaListar", ResourceType = typeof(Resource))]
        public string InicioVotaListar { get; set; }

        [Display(Name = "fimVotaListar", ResourceType = typeof(Resource))]
        public string FimVotaListar { get; set; }

        //

        //Historia, evento e competição

        [Display(Name = "statusValor", ResourceType = typeof(Resource))]
        public string StatusValor { get; set; }

        //

        [Display(Name = "nome", ResourceType = typeof(Resource))]
        public string nome { get; set; }

        [Display(Name = "digitarNome", ResourceType = typeof(Resource))]
        public string digitarNome { get; set; }

        [Display(Name = "senha", ResourceType = typeof(Resource))]
        public string senha { get; set; }

        [Display(Name = "digitarSenha", ResourceType = typeof(Resource))]
        public string digitarSenha { get; set; }

        [Display(Name = "confirmarSenha", ResourceType = typeof(Resource))]
        public string confirmarSenha { get; set; }

        [Display(Name = "email", ResourceType = typeof(Resource))]
        public string email { get; set; }

        [Display(Name = "digitarEmail", ResourceType = typeof(Resource))]
        public string digitarEmail { get; set; }

        [Display(Name = "entrar", ResourceType = typeof(Resource))]
        public string entrar { get; set; }

        [Display(Name = "cadastro", ResourceType = typeof(Resource))]
        public string cadastro { get; set; }

        [Display(Name = "descricao", ResourceType = typeof(Resource))]
        public string descricao { get; set; }

        [Display(Name = "dataNasc", ResourceType = typeof(Resource))]
        public string dataNasc { get; set; }

        [Display(Name = "criarHist", ResourceType = typeof(Resource))]
        public string criarHist { get; set; }

        [Display(Name = "titulo", ResourceType = typeof(Resource))]
        public string titulo { get; set; }

        [Display(Name = "sinopse", ResourceType = typeof(Resource))]
        public string sinopse { get; set; }

        [Display(Name = "genero", ResourceType = typeof(Resource))]
        public string genero { get; set; }

        [Display(Name = "classificacao", ResourceType = typeof(Resource))]
        public string classificacao { get; set; }

        [Display(Name = "livre", ResourceType = typeof(Resource))]
        public string livre { get; set; }

        [Display(Name = "publicado", ResourceType = typeof(Resource))]
        public string publicado { get; set; }

        [Display(Name = "rascunho", ResourceType = typeof(Resource))]
        public string rascunho { get; set; }

        //Evento literário

        [Display(Name = "meusEventos", ResourceType = typeof(Resource))]
        public string meusEventos { get; set; }

        [Display(Name = "criarEvento", ResourceType = typeof(Resource))]
        public string criarEvento { get; set; }
    }
}