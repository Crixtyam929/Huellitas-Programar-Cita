using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using Huellitas.Models;

public class JwtAuthorizeAttribute : AuthorizeAttribute
{
    public string RequiredRole { get; set; }
    DBHuellitasEntities db = new DBHuellitasEntities();
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        string token = ObtenerTokenDesdeHeaderOCookie(httpContext);

        if (string.IsNullOrEmpty(token))
            return false;
            
        try
        {
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            // Guardar el usuario validado
            httpContext.User = principal;
            var email = principal.FindFirst(ClaimTypes.Name)?.Value;
            Console.WriteLine($"Email: {email}");

            if (string.IsNullOrEmpty(email))
                return false;

            var usuario = db.Usuarios.FirstOrDefault(x => x.Email == email);
            if (usuario == null){
                var Empleado = db.Empleadoes.FirstOrDefault(x => x.Email == email);

                if (Empleado == null)
                    return false;

                httpContext.Items["EmpleadoActual"] = Empleado;
            }
            else
            {
                httpContext.Items["UsuarioActual"] = usuario;
            }

            // Si se requiere un rol específico, validarlo
            if (!string.IsNullOrEmpty(RequiredRole))
            {
                return principal.IsInRole(RequiredRole);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        // Devuelve 401 o redirige
        filterContext.Result = new RedirectResult("/Login");
    }

    private string ObtenerTokenDesdeHeaderOCookie(HttpContextBase context)
    {
        // Desde Header
        string authHeader = context.Request.Headers["Authorization"];
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            return authHeader.Substring("Bearer ".Length).Trim();
        }

        // Desde Cookie
        HttpCookie tokenCookie = context.Request.Cookies["token"];
        if (tokenCookie != null)
        {
            return tokenCookie.Value;
        }

        return null;
    }
}
