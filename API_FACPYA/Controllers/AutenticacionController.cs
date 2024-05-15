using Microsoft.AspNetCore.Mvc;
using BLL;
using Entity;
using Microsoft.Extensions.Configuration; // Agregamos la importación de IConfiguration
using System.Collections.Generic;
using DAL;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Diagnostics.Eventing.Reader;

namespace ApiClaseMiercoles.Controllers
{
    [Route("api/[controller]")] // Cambiamos [Controller] a [controller] para que coincida con el nombre de la clase.
    [ApiController]

    public class AutentificacionController : ControllerBase
    {
        private readonly string Cadena;
        private readonly string SecretKey;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public AutentificacionController(IConfiguration config)
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        {
#pragma warning disable CS8601 // Posible asignación de referencia nula
            SecretKey = config.GetSection("settings").GetSection("secretKey").ToString();
#pragma warning restore CS8601 // Posible asignación de referencia nula
            Cadena = config.GetConnectionString("PROD");
        }

        [HttpPost]
        [Route("Token")]

        public IActionResult Token([FromBody] DtoToken UsuarioT)
        {
            bool Validacion = BL_AUTENTICACION.ValidaUsuarioInterfaces(Cadena, UsuarioT.Usuario, UsuarioT.Contra);

            if (Validacion)
            {
                var KeyByte = Encoding.ASCII.GetBytes(SecretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, UsuarioT.Usuario));

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddSeconds(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyByte), SecurityAlgorithms.HmacSha256Signature)

                };

                var FechaExpira = tokenDescription.Expires;

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescription);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);

                return Ok(new { access_token = tokencreado, Expires_Date = FechaExpira });

            }
            else
            {
                return Unauthorized(new { access_token = "Credenciales incorrectas" });
            }

        }
    }
}
