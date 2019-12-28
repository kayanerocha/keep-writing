using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KeepWriting.Helper;
using KeepWriting.Models;
using KeepWriting.Repositorios;
using KeepWriting.Utilities;
using KeepWriting.ViewModels;
using log4net;

namespace KeepWriting.Controllers
{
    public class UsuarioController : BaseController
    {
        private KeepWritingBDEntities db = new KeepWritingBDEntities();
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        // GET: Usuario
        public ActionResult Listar()
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            if(usuarioLogado.Status == "Ativado")
            {
                List<UsuarioViewModel> usuarioViewModel = new List<UsuarioViewModel>();
                var listaEventos = (from usuario in db.Usuario
                                    join nivel in db.Nivel on usuario.IdNivel equals nivel.IdNivel
                                    where usuario.IdUsuario == usuarioLogado.IdUsuario
                                    select new { usuario.IdUsuario, usuario.Nome, usuario.Descricao, usuario.DataNasc, usuario.Email, usuario.Foto, usuario.QtdHistorias, usuario.QtdPonto, usuario.Status, NomeNivel = nivel.Nome }).ToList();
                foreach (var item in listaEventos)
                {
                    UsuarioViewModel usuarioVM = new UsuarioViewModel();
                    usuarioVM.IdUsuario = item.IdUsuario;
                    usuarioVM.Nome = item.Nome;
                    usuarioVM.Descricao = item.Descricao;
                    usuarioVM.DataNasc = item.DataNasc;
                    usuarioVM.Email = item.Email;
                    usuarioVM.Foto = item.Foto;
                    usuarioVM.QtdHistorias = item.QtdHistorias.Value;
                    usuarioVM.QtdPonto = item.QtdPonto;
                    usuarioVM.Status = item.Status;
                    usuarioVM.Nivel = item.NomeNivel;
                    usuarioViewModel.Add(usuarioVM);
                }
                return View(usuarioViewModel);
            }
            else
            {
                return View("~/Views/Usuario/Status.cshtml");
            }            
        }

        // GET: Usuario/Details/5
        public ActionResult DetalhesPrivados(int? id)
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }

            if (usuarioLogado.Status == "Ativado")
            {
                var consultaUsuario = db.Usuario.Any(e => e.IdUsuario == id);
                if (id == null || consultaUsuario == false)
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
                else
                {
                    List<UsuarioViewModel> usuarioViewModel = new List<UsuarioViewModel>();
                    var infoUsuario = (from usuario in db.Usuario
                                        join nivel in db.Nivel on usuario.IdNivel equals nivel.IdNivel
                                        where usuario.IdUsuario == usuarioLogado.IdUsuario
                                        select new { usuario.IdUsuario, usuario.Nome, usuario.Descricao, usuario.DataNasc, usuario.Email, usuario.Foto, usuario.QtdHistorias, usuario.QtdPonto, usuario.Status, NomeNivel = nivel.Nome }).ToList();
                    foreach (var item in infoUsuario)
                    {
                        UsuarioViewModel usuarioVM = new UsuarioViewModel();
                        usuarioVM.IdUsuario = item.IdUsuario;
                        usuarioVM.Nome = item.Nome;
                        usuarioVM.Descricao = item.Descricao;
                        usuarioVM.DataNasc = item.DataNasc;
                        usuarioVM.Email = item.Email;
                        usuarioVM.Foto = item.Foto;
                        usuarioVM.QtdHistorias = item.QtdHistorias.Value;
                        usuarioVM.QtdPonto = item.QtdPonto;
                        usuarioVM.Status = item.Status;
                        usuarioVM.Nivel = item.NomeNivel;
                        usuarioViewModel.Add(usuarioVM);
                    }
                    return View(usuarioViewModel.Find(x => x.IdUsuario == id));
                }
            }
            else
            {
                return View("~/Views/Usuario/Status.cshtml");
            }
        }

        public bool VerificaEmail(string email)
        {
            var checar = db.Usuario.Where(e => e.Email == email).FirstOrDefault();
            return checar != null;
        }

        public void EnviarEmail(string email, string codigoAtivacao)
        {
            var GenarateUserVerificationLink = "/Usuario/VerificacaoUsuario/" + codigoAtivacao;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var meuEmail = new MailAddress("keepwritingpds@gmail.com", "Keep Writing"); // set your email  
            var minhaSenha = Environment.GetEnvironmentVariable("SENHA_EMAIL", EnvironmentVariableTarget.User); // Set your password   
            var paraEmail = new MailAddress(email);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(meuEmail.Address, minhaSenha);

            var Message = new MailMessage(meuEmail, paraEmail);
            Message.Subject = "Cadastro completo";
            Message.Body = "<br/> Seu cadastro foi completado com sucesso." +
                           "<br/> Por favor clique no link abaixo para verificação da conta." +
                           "<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }

        // GET: Usuario/Create
        public ActionResult Cadastro()
        {
            var usuarioViewModel = new UsuarioViewModel();
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro([Bind(Include = "IdUsuario,Nome,DataNasc,Descricao,Email,Senha,QtdPonto,NivelExper,QtdHistorias,Situacao,Foto")] Usuario usuario, UsuarioViewModel usuariosViewModel)
        {
            usuario.EmailVerificado = false;
            var emailVerif = VerificaEmail(usuariosViewModel.Email);
            if (emailVerif)
            {
                ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                return View(usuariosViewModel);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    usuario.CodigoAtivacao = Guid.NewGuid();
                    usuario.Senha = CriptografarSenha.criptografarSenha(usuario.Senha);
                    usuario.Nome = usuariosViewModel.Nome;
                    usuario.Email = usuariosViewModel.Email;
                    usuario.DataNasc = usuariosViewModel.DataNasc;
                    usuario.Descricao = usuariosViewModel.Descricao;
                    usuario.QtdHistorias = 0;
                    usuario.QtdPonto = 50;
                    usuario.IdNivel = 1;
                    usuario.Tipo = "Comum";
                    usuario.Status = "Ativado";
                    Log.Info("Um usuário foi cadastrado.");
                    db.Usuario.Add(usuario);
                    db.SaveChanges();

                    EnviarEmail(usuario.Email, usuario.CodigoAtivacao.ToString());
                    var Mensagem = "Cadastro completo. Por favor veja seu e-mail: " + usuario.Email;
                    ViewBag.Mensagem = Mensagem;

                    ViewBag.StatusCadastro = "Usuário cadastrado com sucesso.";
                    return View("Mensagem");
                }
                return View(usuariosViewModel);
            }

        }

        public ActionResult VerificacaoUsuario(string id)
        {
            bool Status = false;

            db.Configuration.ValidateOnSaveEnabled = false; // Ignor to password confirmation   
            var IsVerify = db.Usuario.Where(u => u.CodigoAtivacao == new Guid(id)).FirstOrDefault();

            if (IsVerify != null)
            {
                IsVerify.EmailVerificado = true;
                db.SaveChanges();
                ViewBag.Menssagem = "Verificação de e-mail completa.";
                Status = true;
            }
            else
            {
                ViewBag.Menssagem = "E-mail não verificado.";
                ViewBag.Status = false;
            }

            return View("VerificacaoUsuario");
        }

        public ActionResult Autenticacao()
        {
            ViewBag.Titulo = LocalResource.Resource.entrar;
            return View();
        }

        [HttpPost]
        public ActionResult Autenticacao(Login login)
        {
            Logger.Info("Teste de log de informações");
            Logger.Debug("Teste de log de depuração");
            Logger.Fatal("Teste de log fatal");
            if (ModelState.IsValid)
            {
                var senha = CriptografarSenha.criptografarSenha(login.Senha);
                bool Valida = db.Usuario.Any(x => x.Email == login.Email && x.Senha == senha);
                var consultaUsuario = db.Usuario.Where(u => u.Email == login.Email && u.Senha == senha).FirstOrDefault();
                
                if (Valida)
                {
                    //if (consultaUsuario.EmailVerificado == false)
                    //{
                    //    ModelState.AddModelError("", "Você ainda não confirmou seu e-mail. Por favor acesse o link enviado para ele.");
                    //}
                    //else
                    //{
                        int tempo = login.Lembreme ? 60 : 5; // Timeout in minutes, 60 = 1 hour.  
                        var ticket = new FormsAuthenticationTicket(login.Email, false, tempo);
                        string criptografada = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, criptografada);
                        cookie.Expires = System.DateTime.Now.AddMinutes(tempo);
                        cookie.HttpOnly = true;
                        cookie.Value = consultaUsuario.IdUsuario.ToString();
                        Response.Cookies.Add(cookie);
                        Session["IdUsuario"] = consultaUsuario.IdUsuario.ToString();
                        Session["Status"] = consultaUsuario.Status;
                        Session.Add("usuario", consultaUsuario);
                        var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
                        if (GestaoElementos.VerifStatusUsuario())
                        {
                            Log.Info("Um usuário foi autenticado.");
                            return RedirectToAction("Index", "Adm");
                        }
                        else
                        {
                            return View("Status");
                        }
                    //}
                }
                else
                {
                    ModelState.AddModelError("", "E-mail e/ou senha inválido, por favor tente novamente.");
                }
                return View(login);
            }
            return View(login);
        }

        public ActionResult Sair()
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            HttpContext.Session.Clear();
            FormsAuthentication.SignOut();
            Log.Info("Um usuário saiu da conta.");
            return RedirectToAction("Autenticacao", "Usuario");
        }

        public ActionResult RecuperarSenha()
        {
            return View();
        }

        public string GerarSenha()
        {
            string OtpTamanho = "4";
            string Otp = string.Empty;

            string Numeros = string.Empty;
            Numeros = "1,2,3,4,5,6,7,8,9,0";

            char[] seplitChar = { ',' };
            string[] arr = Numeros.Split(seplitChar);
            string NovoOtp = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OtpTamanho); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                NovoOtp += temp;
                Otp = NovoOtp;
            }
            return Otp;
        }

        public void EnviarEmailRecupeSenha(string email, string codigoAtivacao, string otp)
        {
            var GerarLinkVerificacao = "/Usuario/AlterarSenha/" + codigoAtivacao;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GerarLinkVerificacao);

            var meuEmail = new MailAddress("keepwritingpds@gmail.com", "Multiverso"); // set your email  
            var minhaSenha = Environment.GetEnvironmentVariable("SENHA_EMAIL"); // Set your password   
            var paraEmail = new MailAddress(email);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(meuEmail.Address, minhaSenha);

            var Message = new MailMessage(meuEmail, paraEmail);
            Message.Subject = "Recuperação de senha";
            Message.Body = "<br/> Por favor clique no link abaixo para alterar sua senha utilizando o código " + otp + "." +
                           "<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }

        [HttpPost]
        public ActionResult RecuperarSenha(RecuperarSenha recuperarSenha)
        {
            var verificaEmail = VerificaEmail(recuperarSenha.Email);
            if (!verificaEmail)
            {
                ModelState.AddModelError("EmailNaoExiste", "Este e-mail não é cadastrado.");
                return View();
            }
            var usuario = db.Usuario.Where(x => x.Email == recuperarSenha.Email).FirstOrDefault();

            // Genrate OTP   
            string Otp = GerarSenha();

            usuario.CodigoAtivacao = Guid.NewGuid();
            usuario.Otp = Otp;
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();

            EnviarEmailRecupeSenha(usuario.Email, usuario.CodigoAtivacao.ToString(), usuario.Otp);
            ViewBag.Menssagem = "Por favor verifique seu e-mail " + usuario.Email + " para a alteração de senha.";
            Log.Info("Um usuário solicitou um código para recuperar senha.");
            return View();
        }

        public bool VerificaOtp(string otp)
        {
            var verificacao = db.Usuario.Where(e => e.Otp == otp).FirstOrDefault();
            return verificacao != null;
        }

        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(AlteraSenha alterarSenha)
        {
            var otpVerif = VerificaOtp(alterarSenha.Otp);
            if (!otpVerif)
            {
                ModelState.AddModelError("Otp", "Código inválido.");
                Log.Info("Um usuário digitou o código de recuperação de senha errado.");
                return View(alterarSenha);
            }
            var usuario = db.Usuario.Where(u => u.Otp == alterarSenha.Otp).FirstOrDefault();
            if (ModelState.IsValid)
            {
                usuario.Senha = usuario.Senha = CriptografarSenha.criptografarSenha(alterarSenha.Senha);
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Menssagem = "Senha alterada com sucesso.";
                Log.Info("Um usuário alterou a sua senha.");
                return View();
            }
            return View(alterarSenha);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Editar(int? id)
        {
            var userLogado = (Usuario)HttpContext.Session["usuario"];
            if (userLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            var usuarioViewModel = new UsuarioViewModel();
            //usuarioViewModel.IdUsuario = usuario.IdUsuario;
            usuarioViewModel.Nome = usuario.Nome;
            usuarioViewModel.Descricao = usuario.Descricao;
            usuarioViewModel.EditarDataNasc = usuario.DataNasc;
            //usuarioViewModel.QtdHistorias = usuario.QtdHistorias.Value;
            //usuarioViewModel.IdNivel = usuario.IdNivel;
            int usuarioLogado = Convert.ToInt32(Session["IdUsuario"].ToString());
            usuarioViewModel.IdUsuario = usuarioLogado;
            return View(usuarioViewModel);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "IdHistoria,IdGeneroPortugues,IdUsuario,Titulo,Sinopse,Classificacao,QtdVotos,Capa,Situacao,IdGeneroIngles,IdIdioma")] Usuario usuario, UsuarioViewModel usuarioViewModel)
        {
            var imageTypes = new string[]{
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };
            if (usuarioViewModel.ImagemUpload == null || usuarioViewModel.ImagemUpload.ContentLength == 0)
            {
                usuario.Foto = null;
            }
            else if (usuarioViewModel.ImagemUpload != null || usuarioViewModel.ImagemUpload.ContentLength > 0)
            {
                if (!imageTypes.Contains(usuarioViewModel.ImagemUpload.ContentType))
                {
                    ModelState.AddModelError("ImagemUpload", "Escolha uma imagem GIF, JPG ou PNG.");
                }
                //lemos a imagem e a seguir os bytes armazenados
                using (var binaryReader = new BinaryReader(usuarioViewModel.ImagemUpload.InputStream))
                    usuario.Foto = binaryReader.ReadBytes(usuarioViewModel.ImagemUpload.ContentLength);
            }
            if (ModelState.IsValid)
            {
                int usuarioLogado = Convert.ToInt32(Session["IdUsuario"].ToString());
                usuario.IdUsuario = usuarioLogado;
                usuario.Nome = usuarioViewModel.Nome;
                usuario.Descricao = usuarioViewModel.Descricao;
                usuario.DataNasc = usuarioViewModel.DataNasc;
                //usuario.IdNivel = usuario.IdNivel;
                db.Entry(usuario).State = EntityState.Modified;
                db.Entry(usuario).Property(p => p.QtdHistorias).IsModified = false;
                db.Entry(usuario).Property(p => p.QtdPonto).IsModified = false;
                db.Entry(usuario).Property(p => p.IdNivel).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Listar");
            }
            return View(usuarioViewModel);
        }

        // GET: Usuario/Delete/5
        public ActionResult Excluir(int? id)
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            if (usuarioLogado == null)
            {
                return Redirect("/Usuario/Autenticacao");
            }

            if (usuarioLogado.Status == "Ativado")
            {
                var consultaUsuario = db.Usuario.Any(e => e.IdUsuario == id);
                if (id == null || consultaUsuario == false)
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
                else
                {
                    List<UsuarioViewModel> usuarioViewModel = new List<UsuarioViewModel>();
                    var infoUsuario = (from usuario in db.Usuario
                                       join nivel in db.Nivel on usuario.IdNivel equals nivel.IdNivel
                                       where usuario.IdUsuario == usuarioLogado.IdUsuario
                                       select new { usuario.IdUsuario, usuario.Nome, usuario.Descricao, usuario.DataNasc, usuario.Email, usuario.Foto, usuario.QtdHistorias, usuario.QtdPonto, usuario.Status, NomeNivel = nivel.Nome }).ToList();
                    foreach (var item in infoUsuario)
                    {
                        UsuarioViewModel usuarioVM = new UsuarioViewModel();
                        usuarioVM.IdUsuario = item.IdUsuario;
                        usuarioVM.Nome = item.Nome;
                        usuarioVM.Descricao = item.Descricao;
                        usuarioVM.DataNasc = item.DataNasc;
                        usuarioVM.Email = item.Email;
                        usuarioVM.Foto = item.Foto;
                        usuarioVM.QtdHistorias = item.QtdHistorias.Value;
                        usuarioVM.QtdPonto = item.QtdPonto;
                        usuarioVM.Status = item.Status;
                        usuarioVM.Nivel = item.NomeNivel;
                        usuarioViewModel.Add(usuarioVM);
                    }
                    return View(usuarioViewModel.Find(x => x.IdUsuario == id));
                }
            }
            else
            {
                return View("~/Views/Usuario/Status.cshtml");
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuarioLogado = (Usuario)HttpContext.Session["usuario"];
            Usuario usuario = db.Usuario.Find(id);
            EventoLiterario eventoLiterario = db.EventoLiterario.Find(id);
            db.EventoLiterario.Remove(eventoLiterario);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            HttpContext.Session.Clear();
            return Redirect("/Autenticacao/Cadastro");
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