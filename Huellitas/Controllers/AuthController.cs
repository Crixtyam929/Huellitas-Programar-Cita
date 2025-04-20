using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Description;
using Huellitas.Clases;
using Huellitas.Models;

namespace Huellitas.Controllers
{
    /// <summary>
    /// Controlador para la gestión de autenticación y registro de usuarios
    /// </summary>
    [RoutePrefix("")]
    public class AuthController : Controller
    {
        private readonly DBHuellitasEntities _db;
        private readonly AuthCls _authService;

        public AuthController()
        {
            _db = new DBHuellitasEntities();
            _authService = new AuthCls();
        }

        /// <summary>
        /// Muestra la vista de inicio de sesión de los empleados y veterinarios
        /// </summary>
        /// <returns>Vista de login</returns>
        [Route("login/empleado")]
        public ActionResult EmployeeLoginView()
        {

            return View("loginEmployee");
        }

        /// <summary>
        /// Procesa el inicio de sesión del empleado
        /// </summary>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Redirección según resultado de autenticación</returns>
        [HttpPost]
        [Route("login/empleado")]
        public ActionResult EmployeeLogin(string password, string email)
        {
            Empleado usr = _authService.ValidarEmpleado(email, password);
            if (usr != null)
            {
                string token = TokenManager.GenerateTokenJwt(email,"Empleado");
                var cookie = new HttpCookie("token", token)
                {
                    HttpOnly = true,
                    Secure = Request.IsSecureConnection,
                    Expires = DateTime.UtcNow.AddHours(1),
                    Path = "/"
                };
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("EmployeeLoginView", "Auth");
        }

        /// <summary>
        /// Muestra la vista de inicio de sesión
        /// </summary>
        /// <returns>Vista de login</returns>
        [Route("login")]
        public ActionResult LoginView()
        {

            return View("login");
        }

        /// <summary>
        /// Muestra la vista de registro
        /// </summary>
        /// <returns>Vista de registro</returns>
        [Route("register")]
        public ActionResult RegisterView()
        {
            return View("register");
        }

        /// <summary>
        /// Procesa el inicio de sesión del usuario
        /// </summary>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Redirección según resultado de autenticación</returns>
        [HttpPost]
        [Route("login")]
        public ActionResult Login(string password, string email)
        {
           

            Usuario usr = _authService.ValidarUsuario(email, password);
            if (usr != null)
            {
                string token = TokenManager.GenerateTokenJwt(email);
                var cookie = new HttpCookie("token", token)
                {
                    HttpOnly = true,
                    Secure = Request.IsSecureConnection,
                    Expires = DateTime.UtcNow.AddHours(1),
                    Path = "/"
                };
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LoginView", "Auth");
        }

        /// <summary>
        /// Procesa el registro de un nuevo usuario
        /// </summary>
        /// <param name="usuarioData">Datos del usuario a registrar</param>
        /// <returns>Redirección según resultado del registro</returns>
        [HttpPost]
        [Route("register")]
        public ActionResult Register([Bind(Include = "Nombre,Email,Password")] UsuarioModel usuarioData)
        {
            Usuario usr = _authService.RegistrarUsuario(usuarioData);
            if (usr != null)
            {
                return RedirectToAction("LoginView", "Auth");
            }
            else
            {
                return RedirectToAction("RegisterView", "Auth");
            }
        }

        /// <summary>
        /// Cambia la contraseña de un usuario
        /// </summary>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <param name="password">Nueva contraseña</param>
        /// <returns>Resultado de la operación</returns>
        /// 
        // ----- ELIMINAR EN PROD --------
        // ----- ELIMINAR EN PROD --------
        // ----- ELIMINAR EN PROD --------
        // ----- ELIMINAR EN PROD --------
        [HttpPost]
        [Route("cambiarPass")]
        public ActionResult ChangePass(string password, string email)
        {
            Usuario usr = _authService.CambiarPassword(email, password);
            string mensaje = $"usr: {usr}, Email: {email}";
            return Content(mensaje);
        }

        /// <summary>
        /// Obtiene información del usuario actual autenticado
        /// </summary>
        /// <returns>Información del usuario</returns>
        [JwtAuthorize(RequiredRole = "Usuario")]
        [Route("whoami")]
        public ActionResult GetUser()
        {
            var usuario = HttpContext.Items["UsuarioActual"] as Usuario;
            var userId = ((ClaimsIdentity)User.Identity)
                .FindFirst(ClaimTypes.Name)?.Value;
            string mensaje = $"usr: {userId} {usuario.PasswordHash} ";
            return Content(mensaje);
        }

        [JwtAuthorize(RequiredRole = "Empleado")]
        [Route("whoami/Employee")]
        public ActionResult GetEmployee()
        {
            var usuario = HttpContext.Items["EmpleadoActual"] as Empleado;
            var userId = ((ClaimsIdentity)User.Identity)
                .FindFirst(ClaimTypes.Name)?.Value;
            string mensaje = $"usr: {userId} {usuario.PasswordHash} ";
            return Content(mensaje);
        }

        /// <summary>
        /// Cierra la sesión del usuario eliminando la cookie de token
        /// </summary>
        /// <returns>Redirección a la página de inicio de sesión</returns>
        [Route("logout")]
        public ActionResult Logout()
        {
            if (Request.Cookies["token"] != null)
            {
                var cookie = new HttpCookie("token")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Path = "/"
                };
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("LoginView");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Metodo para cambiar todas las contraseñas 
        // ----- ELIMINAR EN PROD --------
        // ----- ELIMINAR EN PROD --------
        // ----- ELIMINAR EN PROD --------
        // ----- ELIMINAR EN PROD --------
        [Route("fixpass")]
        public ActionResult ResetPasswords()
        {
            string password = "1234pass";


            foreach (Empleado emp in _db.Empleadoes.ToList())
            {
                emp.PasswordHash= AuthCls.GenerarSHA512ConSalt(password,AuthCls.GenerarSalt());

            }
            _db.SaveChanges();
            return Content("Done");
        }
    }
}