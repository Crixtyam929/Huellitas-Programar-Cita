﻿using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Huellitas.Clases
{
    public static class TokenManager
    {
        public static string GenerateTokenJwt(string username , string rol = "Usuario")
        {
            // Leer configuraciones
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            // Crear credenciales de firma
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Crear claims del usuario
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, rol) 
            });

            // Crear token con audience e issuer
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials
            );

            return tokenHandler.WriteToken(jwtSecurityToken);
        }

        public static ClaimsPrincipal ValidateToken(string token)
        {
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,           // ❌ No validar el issuer
                ValidateAudience = false,         // ❌ No validar el audience
                ValidateIssuerSigningKey = true,  // ✅ Sí validar la firma del token
                IssuerSigningKey = securityKey,
                ValidateLifetime = true,          // ✅ Validar expiración
                ClockSkew = TimeSpan.Zero         // No permitir tolerancia en la expiración
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;

            try
            {
                // Validar el token con las opciones anteriores
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                return principal;
            }
            catch (Exception ex)
            {
                // Token inválido: devolver null o manejar el error
                Console.WriteLine("Token inválido: " + ex.Message);
                return null;
            }
        }
    }
}
