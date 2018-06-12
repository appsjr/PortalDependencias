using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Pendencias.Models;
using System.Configuration;
using System.Data.Entity;

namespace Pendencias.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private DBPendenciasEntities gerenciadorDeLogin = new DBPendenciasEntities();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult NovoLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NovoLogin(Usuario model, string returnUrl)
        {
                if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                {
                    return View(model);
                }

                // Isso não conta falhas de login em relação ao bloqueio de conta
                // Para permitir que falhas de senha acionem o bloqueio da conta, altere para shouldLockout: true
                var result = this.gerenciadorDeLogin.LoginByUsernamePassword(model.Username, model.Password).ToList();
                if (result != null && result.Count() > 0)
                {
                    // Initialization.    
                    var logindetails = result.First();
                    // Login In.    
                    this.SignInUser(logindetails, false);
                    // Info.    
                    return this.RedirectToLocal(returnUrl);
                }
                else
                {
                    // Setting.    
                    ModelState.AddModelError(string.Empty, "Usuário ou senha incorretos.");
                }
            

            // If we got this far, something failed, redisplay form    
            return this.View(model);
        }

        private void SignInUser(LoginByUsernamePassword_Result login, bool isPersistent)
        {
            // Initialization.    
            var claims = new List<Claim>();
            try
            {
                // Setting    
                claims.Add(new Claim(ClaimTypes.Name, login.username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, login.id.ToString()));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign In.    
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                gerenciadorDeLogin.Database.ExecuteSqlCommand("INSERT INTO LOGIN (Username, Password, Descricao) VALUES ('"+model.UserName+ "', '" + model.Password + "', '" + model.Descricao + "')");
                return RedirectToAction("NovoLogin", "Account");
            }

            // Se chegamos até aqui e houver alguma falha, exiba novamente o formulário
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                // Setting.    
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                // Sign Out.    
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return RedirectToAction("NovoLogin", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Auxiliares
        // Usado para proteção XSRF ao adicionar logons externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Pendencias");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}