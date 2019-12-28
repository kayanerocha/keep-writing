using KeepWriting.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KeepWriting.Repositorios
{
    public class InscricaoRepositorio
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();

        public static bool VerificarVoto(int idCompeticao, int idUsuario)
        {
            try
            {
                KeepWritingBDEntities db = new KeepWritingBDEntities();
                var usuarioLogado = (Usuario)HttpContext.Current.Session["usuario"];
                Voto verifVoto = db.Voto.Where(v => v.IdInscricaoComp == idCompeticao && v.IdUsuario == idUsuario).First();
                if (verifVoto == null)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Vencedora(Competicao competicao)
        {
            var competicoes = db.Competicao.Where(c => c.DataFimVota < DateTime.Now); //competições que acabaram as votações
            var vencedora = db.Inscricao.Max(i => i.QtdVotos);
        }
    }
}