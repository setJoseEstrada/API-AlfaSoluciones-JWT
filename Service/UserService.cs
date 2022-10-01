using Alfa.Models;
using Alfa.Models.Common;
using Alfa.Models.Request;
using Alfa.Models.Response;
using Alfa.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace Alfa.Service
{
    public class UserService : IUserService
    {
        private readonly AppSetings _appSetings;

        public UserService(IOptions<AppSetings> appSetings)
        {

            _appSetings = appSetings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse response = new UserResponse();
            using (var db = new AlfaContext())
            {
                string scontrasena = Encrypt.GetSHA256(model.contrasena);

                var usuario = db.Usuarios.Where(d => d.Correo == model.correo &&
                d.Contrasena == scontrasena).FirstOrDefault();
                if (usuario==null)return null; 
                
                    response.correo = usuario.Correo;

                response.Token = GetToken(usuario);
             


            }
            return response; 
        }

        private string  GetToken(Usuario usuario)
        {
            var tokenHanldler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSetings.Secreto);
            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Correo)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = 
                new SigningCredentials(new SymmetricSecurityKey(llave),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHanldler.CreateToken(tokenDescripcion);
            return tokenHanldler.WriteToken(token);
        }
    }
}                                                        
